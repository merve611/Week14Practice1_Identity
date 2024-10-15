using Microsoft.EntityFrameworkCore;
using Week14Practice1_Identity.Context;
using Week14Practice1_Identity.Dtos;
using Week14Practice1_Identity.Entities;
using Week14Practice1_Identity.Services;
using Week14Practice1_Identity.Types;

namespace Week14Practice1_Identity.Manager
{
    public class UserManager : IUserService
    {
        private readonly CustomIdentityDbContext _db;
        public UserManager(CustomIdentityDbContext db)
        {
            _db = db;
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var newUser = new UserEntity
            {
                Email = user.Email,
                Password = user.Password,
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return new ServiceMessage
            {
                IsSucced = true,
                Message = "Kayıt Başarı ile oluşturuldu"
            };
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                // Kullanıcı bulunamadığında hata veya null dönebilir
                return null;
            }
            return new UserDto { Email = user.Email, Password = user.Password };
        }
    }
}
