using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASBDDS.Shared.Models.Database.DataDb;
using ASBDDS.Shared.Models.Responses;
using ASBDDS.Shared.Models.Requests;

namespace ASBDDS.API.Controllers
{
    [Route("api/admin/switches")]
    [ApiController]
    public class SwitchesController : ControllerBase
    {
        private readonly DataDbContext _context;

        public SwitchesController(DataDbContext context)
        {
            _context = context;
        }

        // GET: api/Switches
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<SwitchAdminResponse>>>> GetSwitches()
        {
            var resp = new ApiResponse<List<SwitchAdminResponse>>();
            try
            {
                var switches = await _context.Switches.Include(s => s.Ports).ToListAsync();
                var _switches = new List<SwitchAdminResponse>();
                foreach (Switch _switch in switches)
                {
                    _switches.Add(new SwitchAdminResponse(_switch));
                }
                resp.Data = _switches;
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
            }
            return resp;
        }

        // GET: api/Switches/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<SwitchAdminResponse>> GetSwitch(Guid id)
        {
            var resp = new ApiResponse<SwitchAdminResponse>();
            try
            {
                var _switch = await _context.Switches.FindAsync(id);
                if(_switch == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "Switch not found";
                }
                else
                {
                    resp.Data = new SwitchAdminResponse(_switch);
                }
            } 
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        // PUT: api/Switches/id
        [HttpPut("{id}")]
        public async Task<ApiResponse<SwitchAdminResponse>> PutSwitch(Guid id, SwitchAdminPutRequest @switchReq)
        {
            var resp = new ApiResponse<SwitchAdminResponse>();
            try
            {
                var _switch = _context.Switches.Include(s => s.Ports).Where(s => s.Id == id).FirstOrDefault();

                if(_switch == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "Switch not found";
                }
                else
                {
                    _switch.Name = switchReq.Name;
                    _switch.Serial = switchReq.Serial;
                    _switch.Ports = new List<SwitchPort>();
                    foreach (var port in switchReq.Ports)
                        _switch.Ports.Add(new SwitchPort() { Number = port.Number, Type = port.Type, Switch = _switch });

                    _context.Entry(_switch).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    resp.Data = new SwitchAdminResponse(_switch);
                }
            }
            catch(Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }

            return resp;
        }

        // POST: api/Switches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ApiResponse<SwitchAdminResponse>> PostSwitch(SwitchAdminPostRequest @switchReq)
        {
            var resp = new ApiResponse<SwitchAdminResponse>();
            try
            {
                var _switch = new Switch()
                {
                    Serial = @switchReq.Serial,
                    Name = @switchReq.Name,
                };
                _switch.Ports = new List<SwitchPort>();
                foreach (var port in @switchReq.Ports)
                    _switch.Ports.Add(new SwitchPort() { Number = port.Number, Type = port.Type, Switch = _switch });
                _context.Switches.Add(_switch);
                await _context.SaveChangesAsync();
                resp.Data = new SwitchAdminResponse(_switch);
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }

            return resp;
        }

        // DELETE: api/Switches/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse<SwitchAdminResponse>> DeleteSwitch(Guid id)
        {
            var resp = new ApiResponse<SwitchAdminResponse>();
            try
            {
                var _switch = await _context.Switches.FindAsync(id);
                if (_switch == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "Switch not found";
                }
                else
                {
                    _context.Switches.Remove(_switch);
                    await _context.SaveChangesAsync();

                    resp.Data = new SwitchAdminResponse(_switch);
                }
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        private bool SwitchExists(Guid id)
        {
            return _context.Switches.Any(e => e.Id == id);
        }
    }
}
