using CVExpress.Entities.Efos;
using CVExpress.EntityFramework;
using CVExpress.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CVExpress.Services.Services
{
    #region Contacts Services

    public class ContactsService : IContactsService
    {
        private readonly CVExpressDbContext _context;

        #region Contacts Service Constructors

        public ContactsService(CVExpressDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Contacts GET Services

        public async Task<List<ContactsEfo>> GetAllContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<ContactsEfo> GetContactById(int id)
        {
            ContactsEfo? contact = await _context.Contacts.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                throw new Exception("Contacto não encontrado!");
            }

            return contact;
        }

        #endregion

        #region Contacts POST Services

        public async Task<ContactsEfo> SendContact(ContactsEfo contact)
        {
            try
            {
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();

                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a enviar contacto: {ex.Message}");
            }
        }

        #endregion

        #region Contacts DELETE Services

        public async Task DeleteContact(int id)
        {
            ContactsEfo? contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                throw new Exception("Contacto não encontrado!");
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        #endregion
    }

    #endregion
}
