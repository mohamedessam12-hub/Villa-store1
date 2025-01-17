using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MagicVila_VillaAPi.Migrations;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace MagicVila_VillaAPi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationdbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemaneger;
        private readonly IMapper mapper;
        private string _Secretkey;
        public UserRepository(ApplicationdbContext applicationdbContext,IConfiguration configuration,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> rolemaneger,IMapper mapper)
        {
            _db = applicationdbContext;
            _userManager = userManager;
            _rolemaneger = rolemaneger;
            this.mapper = mapper;
            _Secretkey = configuration.GetValue<string>("ApiSetting:SecretKey");
        }
        public bool isUniqUser(string username)
        {
            var User = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);

            if (User == null) 
            {
                return true;
            }
            return false;

        }

        public async Task<LoginResponceDTO> Login(LoginRequestDTO loginRequest)
        {
           
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.UserName.ToLower());
            bool isvalid = await _userManager.CheckPasswordAsync(user,loginRequest.Password);
            if (user == null || isvalid == false)
            {
                return new LoginResponceDTO()
                {
                    Token = "",
                    User = null
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_Secretkey);
            var Role = await _userManager.GetRolesAsync(user);
            var tokendescriber = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name,user.UserName.ToString()),
                    new(ClaimTypes.Role,Role.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokendescriber);
            LoginResponceDTO loginResponce = new LoginResponceDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = mapper.Map<UserDTO>(user),
            };
            return loginResponce;
        }

        public async Task<UserDTO> Register(RegisterationRequestDTO registerationRequest)
        {
            ApplicationUser user = new()
            {
                UserName = registerationRequest.UserName,
                Email = registerationRequest.Email,
                NormalizedEmail = registerationRequest.UserName.ToUpper(),
                Name = registerationRequest.Name,
                PhoneNumber = registerationRequest.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequest.Password);
                if (result.Succeeded)
                {
                    if (!await _rolemaneger.RoleExistsAsync("admin"))
                    {
                        await _rolemaneger.CreateAsync(new IdentityRole("admin"));
                        await _rolemaneger.CreateAsync(new IdentityRole("customer"));
                    }
                    await _userManager.AddToRoleAsync(user, "admin");
                    var userToReturn = _db.ApplicationUsers
                        .FirstOrDefault(u => u.UserName == registerationRequest.UserName);
                    return mapper.Map<UserDTO>(userToReturn);
                }
                else
                {
                    // إذا لم تنجح عملية إنشاء المستخدم، اجمع الأخطاء وألقها كاستثناء
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    throw new Exception(string.Join("; ", errors));
                }
            }
            catch (Exception e)
            {
                // قم بتسجيل الخطأ لتسهيل التحليل لاحقًا
                Console.WriteLine($"Error during registration: {e.Message}");
                throw new Exception($"Registration failed: {e.Message}");
            }


        }
        public async Task<bool> ConfirmEmailAsync(ConfirmEmailDTO userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Name);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // تحقق من وجود البريد الإلكتروني في الداتا بيز
            var emailExists = await _userManager.FindByEmailAsync(userDto.Email); // أو استخدم userDto.Email لو كنت تبعت الإيميل
            if (emailExists == null)
            {
                throw new Exception("Email not found in the database.");
            }

            // تحديث حالة التأكيد للبريد الإلكتروني
            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> ConfirmPhoneAsync(ConfirmPhoneDTO userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Name);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // تحقق من وجود الرقم في الداتا بيز
            if (user.PhoneNumber != userDto.PhoneNumber)
            {
                throw new Exception("Phone number does not match.");
            }

            // تحديث حالة التأكيد لرقم الهاتف
            user.PhoneNumberConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }




    }
}
