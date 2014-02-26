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
        //Validera data, 
        //Fixa pagingen
        //Sort, är kanske inte nödvändigt
        private ContactDAL _contactDAL;
        public ContactDAL ContactDAL { get { return _contactDAL ?? (_contactDAL = new ContactDAL()); } }

        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            totalRowCount = ContactDAL.GetContacts().Count();
            return ContactDAL.GetContacts().Skip(startRowIndex).Take(maximumRows);
            //return ContactDAL.GetContacts(maximumRows, startRowIndex, out totalRowCount);
        }

        public Contact GetContact(int contactID)
        {
            return ContactDAL.GetContact(contactID); 
        }

        public void SaveContact(Contact contact)
        {
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults)) // Använder "extension method" för valideringen!
            {                                             
                
                // ...kastas ett undantag med ett allmänt felmeddelande samt en referens 
                // till samlingen med resultat av valideringen.
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }
            
            if (contact.ContactID == 0) // Ny contact
            {
                ContactDAL.InsertCustomer(contact);
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