using Aventyrliga.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            throw new NotImplementedException("Använd inte denna"); //return ContactDAL.GetContacts();
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public Contact GetContact(int contactID)
        {
            return ContactDAL.GetContact(contactID);
        }

        public void SaveContact(Contact contact)
        {
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults))
            {
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            if (contact.ContactID == 0) // Ny contact
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }

        }

        public void DeleteContact(int contactID)
        {
            ContactDAL.DeleteContact(contactID);
        }
    }
}