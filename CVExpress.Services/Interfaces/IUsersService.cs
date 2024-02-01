using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    public interface IUsersService
    {
        Task<List<UsersEfo>> GetAllUsers();
        Task<UsersEfo> GetUserById(int id);
        Task<UsersEfo> SendUser(UsersEfo user);
        Task<UsersEfo> UpdateUser(int id, UsersEfo updateUser);
        Task DeleteUser(int id);
    }
}
