using ContactApplicationViewLayer.Models;
using DataAccessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly INotifier notifier;
        string ContactApiurl = "http://localhost/ContactAPI/";

        public ContactController(INotifier notifier)
        {
            this.notifier = notifier;
        }

        // GET: Contact
        public async Task<ActionResult> Index()
        {
            IEnumerable<Contact> Contactslist = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(ContactApiurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("api/Contact/GetContacts");

                if (Response.IsSuccessStatusCode)
                {
                    var ResultSet = Response.Content.ReadAsStringAsync().Result;
                    Contactslist = JsonConvert.DeserializeObject<List<Contact>>(ResultSet);
                }
                if (Contactslist == null)
                {
                    return HttpNotFound();
                }
                return View(Contactslist);
            }
        }

        // GET: Contact/Details
        public async Task<ActionResult> Details(int id)
        {
            Contact contact = await GetContactByIDAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,PrimaryEmail,PhoneNumber,IsActive")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(ContactApiurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage request = CreateRequest(HttpMethod.Post, new System.Uri(ContactApiurl + "api/Contact/AddContact"), contact);
                    string content = JsonConvert.SerializeObject(contact);
                    request.Content = new StringContent(content);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Response = await client.SendAsync(request);

                    if (Response.IsSuccessStatusCode)
                    {
                        var ResultSet = Response.Content.ReadAsStringAsync().Result;
                        contact = JsonConvert.DeserializeObject<Contact>(ResultSet);
                        notifier.Success("Contact Saved Sucessfully..");
                    }
                    else
                    { 
                    notifier.Error("There was a problem while Contact Saving..");
                    }
                    return View(contact);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Contact/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Contact contact = await GetContactByIDAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,PrimaryEmail,PhoneNumber,IsActive")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(ContactApiurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpRequestMessage request = CreateRequest(HttpMethod.Put, new System.Uri(ContactApiurl + "api/Contact/UpdateContact"), contact);
                    string content = JsonConvert.SerializeObject(contact);
                    request.Content = new StringContent(content);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Response = await client.SendAsync(request);

                    if (Response.IsSuccessStatusCode)
                    {
                        var ResultSet = Response.Content.ReadAsStringAsync().Result;
                        contact = JsonConvert.DeserializeObject<Contact>(ResultSet);
                        notifier.Success("Contact updated Sucessfully..");
                    }
                    else
                        notifier.Error("There was a problem while Contact update..");
                    if (contact == null)
                    {
                        return HttpNotFound();
                    }
                    return View(contact);
                }
            }
            return View(contact);
        }


        // GET: Contact/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Contact contact = await GetContactByIDAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(FormCollection fcNotUsed, int id = 0)
        {
            bool contactDeleted = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(ContactApiurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.DeleteAsync("api/Contact/DeleteContact?id=" + id);

                if (Response.IsSuccessStatusCode)
                {
                    var ResultSet = Response.Content.ReadAsStringAsync().Result;
                    contactDeleted = JsonConvert.DeserializeObject<bool>(ResultSet);
                    notifier.Success("Contact deleted Sucessfully..");
                }
                else
                    notifier.Error("There was a problem while Contact deletion..");
                if (contactDeleted == false)
                {
                    return HttpNotFound();
                }
            }
            return RedirectToAction("Index");
        }

        private async Task<Contact> GetContactByIDAsync(int contactId)
        {
            Contact contact = new Contact();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(ContactApiurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("api/Contact/GetContactById?Id=" + contactId);

                if (Response.IsSuccessStatusCode)
                {
                    var ResultSet = Response.Content.ReadAsStringAsync().Result;
                    contact = JsonConvert.DeserializeObject<Contact>(ResultSet);
                }
                return contact;
            }
        }

        internal static HttpRequestMessage CreateRequest<TRequest>(HttpMethod verb, Uri uri, TRequest requestContent)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = uri,
                Method = verb
            };

            string content = JsonConvert.SerializeObject(requestContent);
            request.Content = new StringContent(content);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
