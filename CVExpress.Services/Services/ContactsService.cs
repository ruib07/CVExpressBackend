using CVExpress.Entities.Efos;
using CVExpress.EntityFramework;
using CVExpress.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CVExpress.Services.Services
{
    public class ContactsService : IContactsService
    {
        private readonly CVExpressDbContext _context;

        public ContactsService(CVExpressDbContext context)
        {
            _context = context;
        }

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
    }
}
