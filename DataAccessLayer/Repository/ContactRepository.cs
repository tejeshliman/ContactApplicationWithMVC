using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class ContactRepository:IContactRepository
    {
        private ContactDBContext _contactdbContext;
        public ContactRepository(ContactDBContext contactdbContext)
        {
            this._contactdbContext = contactdbContext;
        }
        public IEnumerable<Contact> GetContacts()
        {
            var contacts = _contactdbContext.Contacts.ToList();
            return contacts;
        }

        public Contact GetContactById(int id)
        {
            return _contactdbContext.Contacts.Where(c => c.ID == id).FirstOrDefault();
        }

        public Contact AddContact(Contact contact)
        {
            var addedContact = _contactdbContext.Contacts.Add(contact);
            _contactdbContext.SaveChanges();

            return addedContact;
        }

        public Contact UpdateContact(Contact contact)
        {
            var updatedContact = GetContactById(contact.ID);
            updatedContact.FirstName = contact.FirstName;
            updatedContact.LastName = contact.LastName;
            updatedContact.PrimaryEmail = contact.PrimaryEmail;
            updatedContact.PhoneNumber = contact.PhoneNumber;
            updatedContact.IsActive = contact.IsActive;
            _contactdbContext.SaveChanges();

            return updatedContact;
        }

        public bool DeleteContact(int id)
        {
            var contact = GetContactById(id);
            _contactdbContext.Contacts.Remove(contact);
            _contactdbContext.SaveChanges();
            return true;
        }
    }
}
