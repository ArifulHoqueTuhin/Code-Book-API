using CodeBookAPL.Models.DTO;
using CodeBookAPL.Models;
using System.Linq.Expressions;

namespace CodeBookAPL.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueEmail(string email);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<User> Registration(RegistrationRequestDTO registrationRequestDTO);

        Task<User> GetAsync(Expression<Func<User, bool>>? filter = null, bool tracked = true);

    }
}
