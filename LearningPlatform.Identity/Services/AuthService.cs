using LearningPlatform.Application.Contracts.Identity;
using LearningPlatform.Application.Models.Identity;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entity;
using LearningPlatform.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearningPlatform.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IStudentRepository studentRepository;
        private readonly ITeacherRepository teacherRepository;

        //private readonly EmailService _emailService;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext applicationDbContext, IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            _dbContext = applicationDbContext;
            this.studentRepository = studentRepository;
            this.teacherRepository = teacherRepository;
        }
        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return (0, "Email already exists");

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
  
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.Firstname,
                LastName = model.Lastname,
                UserName = model.Email

            };

            Student student = Student.Create(model.Firstname, model.Lastname, model.Email).Value;
        
            this.studentRepository.AddAsync(student);

            await this._dbContext.SaveChangesAsync();



          
            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(role))
                await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }

        public async Task<(int, string)> RegisterationStudent(RegistrationModel model, string role)
        {  
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return (0, "Email already exists");

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,

                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.Firstname,
                LastName = model.Lastname,
                UserName = model.Email

            };

            Student student = Student.Create(model.Firstname, model.Lastname, model.Email).Value;

            this.studentRepository.AddAsync(student);

            await this._dbContext.SaveChangesAsync();


            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(role))
                await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }


        public async Task<(int, string)> RegisterationTeacher(RegistrationModel model, string role)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return (0, "Email already exists");

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,

                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.Firstname,
                LastName = model.Lastname,
                UserName = model.Email

            };

            Teacher teacher = Teacher.Create(model.Firstname, model.Lastname, model.Email).Value;

            this.teacherRepository.AddAsync(teacher);

            await this._dbContext.SaveChangesAsync();


            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(role))
                await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }



        public async Task<(int, string)> Login(LoginModel model)
        {
            var username = model.Email;
            var user = await userManager.FindByEmailAsync(username);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password!))
                return (0, "Date invalide");

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            string token = GenerateToken(authClaims);
            return (1, token);
        }


        public async Task<(int, string)> Logout()
        {
            await signInManager.SignOutAsync();
            return (1, "User logged out successfully!");
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"]!,
                Audience = configuration["JWT:ValidAudience"]!,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
