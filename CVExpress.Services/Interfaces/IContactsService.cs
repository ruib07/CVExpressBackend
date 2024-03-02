using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    #region Contacts Interface

    public interface IContactsService
    {
        #region Contacts Interface Tasks

        Task<List<ContactsEfo>> GetAllContacts();
        Task<ContactsEfo> GetContactById(int id);
        Task<ContactsEfo> SendContact(ContactsEfo contact);
        Task DeleteContact(int id);

        #endregion
    }

    #endregion
}
