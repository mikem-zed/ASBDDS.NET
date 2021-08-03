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
    [Route("api/admin/[controller]")]
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
        public async Task<ActionResult<ApiResponse<List<Switch>>>> GetSwitches()
        {
            var resp = new ApiResponse<List<Switch>>();
            try
            {
                resp.Data = await _context.Switches.Include(s => s.Ports).ToListAsync();
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
            }
            return resp;
        }

        // GET: api/Switches/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<Switch>> GetSwitch(Guid id)
        {
            var resp = new ApiResponse<Switch>();
            try
            {
                resp.Data = await _context.Switches.FindAsync(id);
            } 
            catch (Exception e)
            {
                resp.Status.Code = 1;
            }
            return resp;
        }

        // POST: api/Switches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ApiResponse<Switch>> PostSwitch(SwitchPostRequest @switchReq)
        {
            var resp = new ApiResponse<Switch>();
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
                resp.Data = _switch;
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
        public async Task<ApiResponse> DeleteSwitch(Guid id)
        {
            var resp = new ApiResponse();
            try
            {
                var @switch = await _context.Switches.FindAsync(id);
                if (@switch == null)
                {
                    resp.Status.Code = 2;
                    resp.Status.Message = "Not found";
                }
                else
                {
                    _context.Switches.Remove(@switch);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                resp.Status.Code = 1;
            }
            return resp;
        }

        private bool SwitchExists(Guid id)
        {
            return _context.Switches.Any(e => e.Id == id);
        }
    }
}
