using ASBDDS.Shared.Models.Database.DataDb;
using ASBDDS.Shared.Models.Requests;
using ASBDDS.Shared.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ASBDDS.API.Controllers
{
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly DataDbContext _context;

        public ProjectsController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/admin/projects/")]
        public async Task<ActionResult<ApiResponse<List<ProjectAdminResponse>>>> GetProjects()
        {
            var resp = new ApiResponse<List<ProjectAdminResponse>>();
            try
            {
                var projects = await _context.Projects.Include(p => p.ProjectDeviceLimits).ToListAsync();
                var _projects = new List<ProjectAdminResponse>();
                foreach (var project in projects)
                {
                    _projects.Add(new ProjectAdminResponse(project));
                }
                resp.Data = _projects;
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        [HttpGet("api/admin/projects/{id}")]
        public async Task<ActionResult<ApiResponse<ProjectAdminResponse>>> GetProject(Guid id)
        {
            var resp = new ApiResponse<ProjectAdminResponse>();
            try
            {
                var project = await _context.Projects.Where(p => p.Id == id).Include(p => p.ProjectDeviceLimits).FirstOrDefaultAsync();

                if (project == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "project not found";
                    return resp;
                }

                resp.Data = new ProjectAdminResponse(project);
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        [HttpPut("api/admin/projects/{id}")]
        public async Task<ActionResult<ApiResponse<ProjectAdminResponse>>> PutProject(Guid id, ProjectAdminPutRequest projectReq)
        {
            var resp = new ApiResponse<ProjectAdminResponse>();
            try
            {
                var project = await _context.Projects.Where(p => p.Id == id).Include(p => p.ProjectDeviceLimits).FirstOrDefaultAsync();
                var deviceLimits = project.ProjectDeviceLimits;

                if (project == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "project not found";
                    return resp;
                }

                project.Name = projectReq.Name;
                project.DefaultVlan = projectReq.DefaultVlan;
                project.AllowCustomBootloaders = projectReq.AllowCustomBootloaders;
                project.Disabled = projectReq.Disabled;

                project.ProjectDeviceLimits.Clear();
                foreach(var deviceLimit in projectReq.ProjectDeviceLimits)
                {
                    project.ProjectDeviceLimits.Add(new ProjectDeviceLimit(deviceLimit, project));
                }

                _context.Entry(project).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                resp.Data = new ProjectAdminResponse(project);
                
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        [HttpPost("api/admin/projects/")]
        public async Task<ApiResponse<ProjectAdminResponse>> PostProject(ProjectAdminPostRequest projectReq)
        {
            var resp = new ApiResponse<ProjectAdminResponse>();
            try
            {
                var project = new Project()
                {
                    Name = projectReq.Name,
                    DefaultVlan = projectReq.DefaultVlan,
                    AllowCustomBootloaders = projectReq.AllowCustomBootloaders,
                    ProjectDeviceLimits = new List<ProjectDeviceLimit>()
                };

                foreach(var deviceLimit in projectReq.ProjectDeviceLimits)
                {
                    project.ProjectDeviceLimits.Add(new ProjectDeviceLimit(deviceLimit, project));
                }

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                resp.Data = new ProjectAdminResponse(project);
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }

            return resp;
        }

        [HttpDelete("api/admin/projects/{id}")]
        public async Task<ActionResult<ApiResponse<ProjectAdminResponse>>> DeleteProject(Guid id)
        {
            var resp = new ApiResponse<ProjectAdminResponse>();
            try
            {
                var project = await _context.Projects.Where(d => d.Id == id).Include(d => d.ProjectDeviceLimits).FirstOrDefaultAsync();
                if (project == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "project not found";
                    return resp;
                }

                foreach (var deviceLimit in project.ProjectDeviceLimits)
                {
                    _context.ProjectDeviceLimits.Remove(deviceLimit);
                }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                resp.Data = new ProjectAdminResponse(project);
                
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }

            return resp;
        }

        [HttpGet("api/projects/")]
        public async Task<ActionResult<ApiResponse<List<ProjectUserResponse>>>> GetUserProjects()
        {
            var resp = new ApiResponse<List<ProjectUserResponse>>();
            try
            {
                var activeProjects = await _context.Projects.Where(p => !p.Disabled).ToListAsync();
                var _projects = new List<ProjectUserResponse>();
                foreach (var project in activeProjects)
                {
                    _projects.Add(new ProjectUserResponse(project));
                }
                resp.Data = _projects;
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        [HttpGet("api/projects/{id}")]
        public async Task<ActionResult<ApiResponse<ProjectUserResponse>>> GetUserProject(Guid id)
        {
            var resp = new ApiResponse<ProjectUserResponse>();
            try
            {
                var project = await _context.Projects.Where(p => p.Id == id).Include(p => p.ProjectDeviceLimits).FirstOrDefaultAsync();

                if (project == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "project not found";
                    return resp;
                }

                resp.Data = new ProjectUserResponse(project);
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        [HttpPut("api/projects/{id}")]
        public async Task<ActionResult<ApiResponse<ProjectUserResponse>>> PutUserProject(Guid id, ProjectUserPutRequest projectReq)
        {
            var resp = new ApiResponse<ProjectUserResponse>();
            try
            {
                var project = await _context.Projects.FindAsync(id);

                if (project == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "project not found";
                    return resp;
                }

                project.Name = projectReq.Name;

                _context.Entry(project).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                resp.Data = new ProjectUserResponse(project);

            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        [HttpPost("api/projects/")]
        public async Task<ApiResponse<ProjectUserResponse>> PostUserProject(ProjectUserPostRequest projectReq)
        {
            var resp = new ApiResponse<ProjectUserResponse>();
            try
            {
                var project = new Project()
                {
                    Name = projectReq.Name,
                };

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                resp.Data = new ProjectUserResponse(project);
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }

            return resp;
        }

        [HttpDelete("api/projects/{id}")]
        public async Task<ActionResult<ApiResponse<ProjectUserResponse>>> DeleteUserProject(Guid id)
        {
            var resp = new ApiResponse<ProjectUserResponse>();
            try
            {
                var project = await _context.Projects.Include(p => p.ProjectDeviceLimits).Where(p => p.Id == id).FirstOrDefaultAsync();
                if (project == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "project not found";
                    return resp;
                }

                foreach (var deviceLimit in project.ProjectDeviceLimits)
                {
                    _context.ProjectDeviceLimits.Remove(deviceLimit);
                }
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                resp.Data = new ProjectUserResponse(project);
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }

            return resp;
        }
    }
}
