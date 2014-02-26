using Aventyrliga.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aventyrliga
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;
        private Service Service { get { return _service ?? (_service = new Service()); } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Contact> ContactListView_GetData()
        {
            return Service.GetContacts(); 
        }

        public void ContactListView_InsertItem(Contact contact)
        {
            Service.SaveContact(contact);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ContactListView_UpdateItem(int contactID)
        {
            var contact = Service.GetContact(contactID);

            if (contact == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", contactID));
                return;
            }
            if (TryUpdateModel(contact) && ModelState.IsValid)
            {
                Service.SaveContact(contact);
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ContactListView_DeleteItem(int contactID)
        {
            Service.DeleteContact(contactID);
        }
    }
}