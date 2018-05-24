using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContactById(int id);

        Contact AddContact(Contact contact);

        Contact UpdateContact(Contact contact);

        bool DeleteContact(int id);

    }
}
