using CVExpress.Entities.Efos;
using CVExpress.EntityFramework;
using CVExpress.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CVExpress.Services.Services
{
    public class RegisterUsersService : IRegisterUsersService
    {
        private readonly CVExpressDbContext _context;

        public RegisterUsersService(CVExpressDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegisterUsersEfo>> GetAllRegisterUsers()
        {
            return await _context.RegisterUsers.ToListAsync();
        }

        public async Task<RegisterUsersEfo> GetRegisterUserById(int id)
        {
            RegisterUsersEfo? registerUser = await _context.RegisterUsers.AsNoTracking()
                .FirstOrDefaultAsync(ru => ru.Id == id);

            if (registerUser == null)
            {
                throw new Exception("Registo de utilizador não encontrado!");
            }

            return registerUser;
        }

        public async Task<RegisterUsersEfo> GetRegisterUserByEmail(string email)
        {
            RegisterUsersEfo? registerUser = await _context.RegisterUsers
                .FirstOrDefaultAsync(ru => ru.Email == email);

            if (registerUser == null)
            {
                throw new Exception("Registo de utilizador não encontrado!");
            }

            return registerUser;
        }

        public async Task<RegisterUsersEfo> SendRegisterUser(RegisterUsersEfo registerUser)
        {
            try
            {
                RegisterUsersEfo? existingEmail = await _context.RegisterUsers
                    .FirstOrDefaultAsync(ee => ee.Email == registerUser.Email);

                if (existingEmail != null)
                {
                    throw new Exception("O email já está em uso.");
                }


                registerUser.Password = HashPassword(registerUser.Password);

                await _context.RegisterUsers.AddAsync(registerUser);
                await _context.SaveChangesAsync();

                return registerUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao enviar registo de utilizador: {ex.Message}");
            }
        }

        public async Task<RegisterUsersEfo> UpdatePassword(string email, string newPassword, string confirmNewPassword)
        {
            try
            {
                RegisterUsersEfo? registerUser = await _context.RegisterUsers
                    .FirstOrDefaultAsync(ru => ru.Email == email);

                if (registerUser == null)
                {
                    throw new Exception("Registo de utilizador não encontrado");
                }

                if (newPassword != confirmNewPassword)
                {
                    throw new Exception("As passwords devem ser iguais");
                }

                registerUser.Password = HashPassword(newPassword);

                await _context.SaveChangesAsync();

                return registerUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar password: {ex.Message}");
            }
        }

        public async Task<RegisterUsersEfo> UpdateRegisterUser(int id, RegisterUsersEfo updateRegisterUser)
        {
            try
            {
                RegisterUsersEfo? newRegisterUser = await _context.RegisterUsers
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (newRegisterUser == null)
                {
                    throw new Exception("Registo de utilizador não encontrado");
                }

                newRegisterUser.FullName = updateRegisterUser.FullName;
                newRegisterUser.Email = updateRegisterUser.Email;
                newRegisterUser.Password = HashPassword(updateRegisterUser.Password);
                newRegisterUser.BirtDate = updateRegisterUser.BirtDate;
                newRegisterUser.Location = updateRegisterUser.Location;
                newRegisterUser.Country = updateRegisterUser.Country;
                newRegisterUser.Nationality = updateRegisterUser.Nationality;
                newRegisterUser.PhoneNumber = updateRegisterUser.PhoneNumber;

                await _context.SaveChangesAsync();

                return newRegisterUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar dados do registo do utilizador: {ex.Message}");
            }
        }

        public async Task DeleteRegisterUser(int id)
        {
            RegisterUsersEfo? registerUser = await _context.RegisterUsers
                .FirstOrDefaultAsync(ru => ru.Id == id);

            if (registerUser == null)
            {
                throw new Exception("Registo de utilizador não encontrado!");
            }

            _context.RegisterUsers.Remove(registerUser);
            await _context.SaveChangesAsync();
        }

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
    }
}
