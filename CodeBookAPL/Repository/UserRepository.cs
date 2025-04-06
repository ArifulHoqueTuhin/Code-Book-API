using CodeBookAPL.Models.DTO;
using CodeBookAPL.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeBookAPL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace CodeBookAPL.Repository
{
    public class UserRepository : IUserRepository
    {


        private readonly CodeBookContext _dbData;
        private string SecretKey;

        public UserRepository(CodeBookContext dbData, IConfiguration configuration)
        {
            _dbData = dbData;
            SecretKey = configuration.GetValue<string>("ApiSettings:Secrets");
        }

        public bool IsUniqueEmail(string email)
        {
            var User = _dbData.Users.FirstOrDefault(u => u.Email == email);

            if (User == null)
            {
                return true;
            }

            return false;
        }


        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _dbData.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email && u.Password == loginRequestDTO.Password);

            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    user = null
                };

            }

            // if user was found generate JWT Token

            var TokenHandler = new JwtSecurityTokenHandler();

            var Key = Encoding.ASCII.GetBytes(SecretKey);


            var TokenDescriptor = new SecurityTokenDescriptor

            {
                Subject = new ClaimsIdentity(new Claim[]

                {
                            new Claim(ClaimTypes.Name, user.Id.ToString()),
                            
                            new Claim(ClaimTypes.Email, user.Email)
                }),

                Expires = DateTime.UtcNow.AddDays(1),

                SigningCredentials = new(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)




            };


            var Token = TokenHandler.CreateToken(TokenDescriptor);

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()

            {
                Token = TokenHandler.WriteToken(Token),
                user = user,
            };

            return loginResponseDTO;
        }



        public async Task<User> Registration(RegistrationRequestDTO registrationRequestDTO)

        {
            User NewUser = new()
            {
                Name = registrationRequestDTO.Name,

                Email = registrationRequestDTO.Email,

                Password = registrationRequestDTO.Password,


            };

            _dbData.Users.Add(NewUser);

            await _dbData.SaveChangesAsync();

            //NewUser.Email = "";

            //NewUser.Password = "";

            return NewUser;



        }


        public async Task<User> GetAsync(Expression<Func<User, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<User> query = _dbData.Users;

            if (!tracked)

            {
                query = query.AsNoTracking();
            }


            if (filter != null)
            {
                query = query.Where(filter);
            }

            //return await query.FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync() ?? default!;



        }

    }
    

    
}
