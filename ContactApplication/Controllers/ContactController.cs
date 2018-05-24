using DataAccessLayer;
using System.Collections.Generic;
using System.Web.Http;

namespace SampleWebApi.Controllers
{
    public class ContactController : ApiController
    {
        IContactRepository contactRepo;

        [Route("api/Contact/GetContacts")]
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            contactRepo = new ContactRepository(ModelFactory<ContactDBContext>.GetContext());
            return contactRepo.GetContacts();
        }

        [Route("api/Contact/GetContactById")]
        [HttpGet]
        public Contact GetContactById(int id)
        {
            contactRepo = new ContactRepository(ModelFactory<ContactDBContext>.GetContext());
            return contactRepo.GetContactById(id);
        }

        [Route("api/Contact/AddContact")]
        [HttpPost]
        public Contact AddContact(Contact contact)
        {
            contactRepo = new ContactRepository(ModelFactory<ContactDBContext>.GetContext());
            return contactRepo.AddContact(contact);
        }

        [Route("api/Contact/UpdateContact")]
        [HttpPut]
        public Contact UpdateContact([FromBody]Contact contact)
        {
            contactRepo = new ContactRepository(ModelFactory<ContactDBContext>.GetContext());
            return contactRepo.UpdateContact(contact);
        }

        [Route("api/Contact/DeleteContact")]
        [HttpDelete]
        public bool DeleteContact([FromBody]int id)
        {
            contactRepo = new ContactRepository(ModelFactory<ContactDBContext>.GetContext());
            return contactRepo.DeleteContact(id);
        }
    }
}
