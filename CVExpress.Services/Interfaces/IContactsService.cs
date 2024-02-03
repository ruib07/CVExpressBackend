using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    public interface IContactsService
    {
        Task<List<ContactsEfo>> GetAllContacts();
        Task<ContactsEfo> GetContactById(int id);
        Task<ContactsEfo> SendContact(ContactsEfo contact);
        Task DeleteContact(int id);
    }
}
