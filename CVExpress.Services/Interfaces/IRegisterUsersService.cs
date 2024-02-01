using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    public interface IRegisterUsersService
    {
        Task<List<RegisterUsersEfo>> GetAllRegisterUsers();
        Task<RegisterUsersEfo> GetRegisterUserById(int id);
        Task<RegisterUsersEfo> SendRegisterUser(RegisterUsersEfo registerUser);
        Task<RegisterUsersEfo> UpdatePassword(string email, string newPassword, string confirmNewPassword);
        Task<RegisterUsersEfo> UpdateRegisterUser(int id, RegisterUsersEfo updateRegisterUser);
        Task DeleteRegisterUser(int id);
    }
}
