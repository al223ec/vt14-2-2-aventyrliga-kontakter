using Aventyrliga.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aventyrliga.Model
{
    public class Service
    {
        private ContactDAL _contactDAL;
        public ContactDAL ContactDAL { get { return _contactDAL ?? (_contactDAL = new ContactDAL()); } }

        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        public Contact GetContact(int contactID)
        {
            return ContactDAL.GetContact(contactID); 
        }

        internal static void SaveContact(Contact contact)
        {
            //Validera data
            if (contact.ContactID == 0) // Ny contact
            {
                ContactDAL.InsertCustomer(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }
        }

        internal static void DeleteContact(int contactID)
        {
            throw new NotImplementedException();
        }
    }
}