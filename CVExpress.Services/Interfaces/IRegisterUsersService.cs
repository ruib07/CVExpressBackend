using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    #region Register Users Interface

    public interface IRegisterUsersService
    {
        #region Register Users Interface Tasks

        Task<List<RegisterUsersEfo>> GetAllRegisterUsers();
        Task<RegisterUsersEfo> GetRegisterUserById(int id);
        Task<RegisterUsersEfo> GetRegisterUserByEmail(string email);
        Task<RegisterUsersEfo> SendRegisterUser(RegisterUsersEfo registerUser);
        Task<RegisterUsersEfo> UpdatePassword(string email, string newPassword, string confirmNewPassword);
        Task<RegisterUsersEfo> UpdateRegisterUser(int id, RegisterUsersEfo updateRegisterUser);
        Task DeleteRegisterUser(int id);

        #endregion
    }

    #endregion
}
