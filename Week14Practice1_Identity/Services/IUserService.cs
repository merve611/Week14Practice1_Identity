using Week14Practice1_Identity.Dtos;
using Week14Practice1_Identity.Types;

namespace Week14Practice1_Identity.Services
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);
        Task<UserDto> GetUserByEmail(string email);
    }
}
