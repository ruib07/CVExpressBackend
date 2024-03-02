using CVExpress.Entities.Efos;
using CVExpress.EntityFramework;
using CVExpress.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CVExpress.Services.Services
{
    #region Users Service

    public class UsersService : IUsersService
    {
        private readonly CVExpressDbContext _context;

        #region Users Service Constructors

        public UsersService(CVExpressDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Users GET Services

        public async Task<List<UsersEfo>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UsersEfo> GetUserById(int id)
        {
            UsersEfo? user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("Utilizador não encontrado!");
            }

            return user;
        }

        #endregion

        #region Users POST Service

        public async Task<UsersEfo> SendUser(UsersEfo user)
        {
            try
            {
                user.Password = HashPassword(user.Password);

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a enviar utilizador: {ex.Message}");
            }
        }

        #endregion

        #region Users PUT Service

        public async Task<UsersEfo> UpdateUser(int id, UsersEfo updateUser)
        {
            try
            {
                UsersEfo? newUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (newUser == null)
                {
                    throw new Exception("Utilizador não encontrado!");
                }

                newUser.FullName = updateUser.FullName;
                newUser.BirtDate = updateUser.BirtDate;
                newUser.Location = updateUser.Location;
                newUser.Country = updateUser.Country;
                newUser.Nationality = updateUser.Nationality;
                newUser.Email = updateUser.Email;
                newUser.PhoneNumber = updateUser.PhoneNumber;
                newUser.Password = HashPassword(updateUser.Password);

                await _context.SaveChangesAsync();

                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar utilizador: {ex.Message}");
            }
        }

        #endregion

        #region Users DELETE Service

        public async Task DeleteUser(int id)
        {
            UsersEfo? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("Utilizador não encontrado!");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Hash Users Password 

        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 32);

            byte[] hashBytes = new byte[16 + 32];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);
            return Convert.ToBase64String(hashBytes);
        }

        #endregion
    }

    #endregion
}
