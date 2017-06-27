using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SellYourCar.DBContext_Related;
using SellYourCar.Entities;
using SellYourCar.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SellYourCar.Controllers
{
    public class AuthenticationController : Controller
    {
        MyContext _ctx;
        SignInManager<CarAdderUser> _sgnInMgr; 
        UserManager<CarAdderUser> _userMgr;
        IPasswordHasher<CarAdderUser> _passHasher;
        IConfigurationRoot _cfg;
        public AuthenticationController(MyContext ctx, SignInManager<CarAdderUser> sgnMgr, UserManager<CarAdderUser> userMgr, 
        IConfigurationRoot cfg , IPasswordHasher<CarAdderUser> passweod)
        {
            _ctx = ctx;
            _cfg = cfg;
            _passHasher = passweod;
            _sgnInMgr = sgnMgr;
            _userMgr = userMgr;
        }
      
       [HttpPost("api/authentication/login")]
       public async Task<IActionResult> Login([FromBody]CredentialModel vm)
       {
           try
           {
               var user =await _userMgr.FindByNameAsync(vm.UserName);
               if(user!=null)
               {
                 if(_passHasher.VerifyHashedPassword(user , user.PasswordHash ,vm.Password )==PasswordVerificationResult.Success)
                 {
                     var userClaims = await _userMgr.GetClaimsAsync(user);
                     var someClaims = new []
                     {
                        new Claim(JwtRegisteredClaimNames.Sub , user.UserName) ,
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email)
                     }.Union(userClaims);
                     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Tokens:Key"]));
                     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                         issuer: _cfg["Tokens:Issuer"],
                         audience: _cfg["Tokens:Audience"],
                         claims: someClaims,
                         expires: DateTime.UtcNow.AddMinutes(10),
                         signingCredentials: creds
                           );
                     return Ok(new { token= new JwtSecurityTokenHandler().WriteToken(token), 
                              expiration= token.ValidTo
                           
                           });
                 }

               }

           }
           catch{}
           return BadRequest("Cant Login server error");
           
       }
       [HttpPost("api/authentication/register")]
       public async Task<IActionResult> Register([FromBody]CredentialModel vm)
       {
             if(!ModelState.IsValid)
             {
                    return BadRequest(ModelState + "username and password are required ");
             }
             var user =await  _userMgr.FindByNameAsync(vm.UserName);
             if(user==null)
              {
             user = new CarAdderUser{ UserName = vm.UserName };
             var userResulted = await _userMgr.CreateAsync(user , vm.Password);
             if (userResulted==null)
            {
                return BadRequest("User can not be created");
            }
               return Ok(); 
            //return Created();
              }
              return BadRequest("user already exists");
           }    
          }


    }
}
