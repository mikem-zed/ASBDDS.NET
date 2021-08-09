using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ASBDDS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASBDDS.Shared.Models.Database.DataDb;
using ASBDDS.Shared.Models.Requests;
using ASBDDS.Shared.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ASBDDS.API.Controllers 
{
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly DataDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(DataDbContext context, UserManager<ApplicationUser> userManager, 
                                    SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: api/ApplicationUsers
        [HttpGet("api/admin/users/")]
        public async Task<ApiResponse<List<UserAdminResponse>>> GetUsers()
        {
            var resp = new ApiResponse<List<UserAdminResponse>>();
            try
            {
                var dbUsers = await _context.Users.ToListAsync();
                resp.Data = new List<UserAdminResponse>();
                foreach (var dbUser in  dbUsers)
                {
                    resp.Data.Add(new UserAdminResponse(dbUser));
                }
            }
            catch(Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        // GET: api/ApplicationUsers/5
        [HttpGet("api/admin/users/{id}")]
        public async Task<ApiResponse<UserAdminResponse>> GetUser(Guid id)
        {
            var resp = new ApiResponse<UserAdminResponse>();
            try
            {
                // TODO: Only admin can read all users. Default user can read - only his account.
                var dbUser = await _context.Users.FindAsync(id);
                if (dbUser == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "not found";
                    return resp;
                }
                resp.Data = new UserAdminResponse(dbUser);
            }
            catch(Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        // PUT: api/ApplicationUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/admin/users/{id}")]
        public async Task<ApiResponse<UserAdminResponse>> PutUser(Guid id, UserAdminPutRequest reqUser)
        {
            var resp = new ApiResponse<UserAdminResponse>();
            try
            {
                if (id != reqUser.Id)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "bad user id";
                    return resp;
                }
                // TODO: Only admin can read all users. Default user can read - only his account.
                var dbUser = await _context.Users.FindAsync(id);
                if (dbUser == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "not found";
                    return resp;
                }

                updateUserAdmin(dbUser, reqUser);
                _context.Entry(dbUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                resp.Data = new UserAdminResponse(dbUser);
            }
            catch(Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        // POST: api/ApplicationUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/admin/users/")]
        public async  Task<ApiResponse<UserAdminResponse>> PostUser(UserAdminPostRequest reqUser)
        {
            var resp = new ApiResponse<UserAdminResponse>();
            try
            {
                //TODO: Save user that this user
                var user = new ApplicationUser(reqUser)
                {
                    
                };
                var result = await _userManager.CreateAsync(user, reqUser.Password);
                if (result.Succeeded)
                {
                    var userDefaultProject = new Project()
                    {
                        Creator = user,
                        Name = user.Name + " " + user.LastName + "'s Project",
                    };
                    _context.Projects.Add(userDefaultProject);
                    await _context.SaveChangesAsync();
                    resp.Data = new UserAdminResponse(user);
                }
                else
                {
                    resp.Status.Code = 1;
                    foreach (var error in result.Errors)
                        resp.Status.Message += error.Code + " \n";
                }
            }
            catch(Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        // DELETE: api/ApplicationUsers/5
        [HttpDelete("api/admin/users/{id}")]
        public async Task<ApiResponse> DisableUser(Guid id)
        {
            var resp = new ApiResponse();
            try
            {
                var dbUser = await _context.Users.FindAsync(id);
                if (dbUser == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "not found";
                    return resp;
                }

                dbUser.Disabled = true;
                _context.Entry(dbUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        private void updateUserAdmin(ApplicationUser dbUser, UserAdminPutRequest reqUser)
        {
            dbUser.Disabled = reqUser.Disabled;
            dbUser.Name = reqUser.Name;
            dbUser.LastName = reqUser.LastName;
            dbUser.Updated = DateTime.UtcNow;
            dbUser.Email = reqUser.Email;
            //TODO: write Editor user.
        }

        [AllowAnonymous]
        [HttpPost("api/users/jwt")]
        public async Task<ActionResult<ApiResponse<TokenResponse>>> GenerateToken(LoginPostRequest request)
        {
            var resp = new ApiResponse<TokenResponse>();
            try
            {
                var dbuser = await _userManager.FindByNameAsync(request.UserName);
                if (dbuser == null)
                {
                    resp.Status.Code = 1;
                    resp.Status.Message = "user not found";
                    return resp;
                }

                var result = await _signInManager.CheckPasswordSignInAsync(dbuser, request.Password, false);
                if (result.Succeeded)
                {
                    var userClaims = await GetClaimsIdentity(dbuser);
                    var now = DateTime.UtcNow;
                    var expires = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));
                    // создаем JWT-токен
                    var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: userClaims.Claims,
                        expires: expires,
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    resp.Data = new TokenResponse()
                    {
                        AccessToken = encodedJwt,
                        UserName = userClaims.Name,
                        Expires = jwt.ValidTo
                    };
                }
            }
            catch(Exception e)
            {
                resp.Status.Code = 1;
                resp.Status.Message = e.Message;
            }
            return resp;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}