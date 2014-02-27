using Aventyrliga.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (Session["UploadSuccessfull"] != null)
            {
                OutputPanel.Visible = true;
                HeaderOutputLiteral.Text = "Posten postades";
                OutputLiteral.Text = "Posten laddades upp, much success";
                Session.Remove("UploadSuccessfull");
            }

            if (Session["UpdateSuccessfull"] != null)
            {
                OutputPanel.Visible = true;
                HeaderOutputLiteral.Text = "Posten uppdaterades";
                OutputLiteral.Text = "Posten uppdaterades, much success";
                Session.Remove("UpdateSuccessfull");
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            try
            {
                return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
            }
            catch (Exception)
            {
                throw new ApplicationException("Felet är väldigt fel, vg åtgärda felet som är fel!");
            }
        }

        public void ContactListView_InsertItem(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.SaveContact(contact);
                    Session["UploadSuccessfull"] = true;
                    Response.RedirectToRoute("Start");
                }
            }
            catch (Exception e)
            {
                var validationResults = e.Data["ValidationResults"] as ICollection<ValidationResult>;
                if (validationResults != null)
                {
                    foreach (var item in validationResults)
                    {
                        ModelState.AddModelError(String.Empty, item.ErrorMessage);
                    }
                }
                ModelState.AddModelError(String.Empty, "Ett fel inträffade då posten skulle läggas till i tabellen.");
            }
        }

        public void ContactListView_UpdateItem(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.SaveContact(contact);
                    Session["UpdateSuccessfull"] = true;
                    Response.RedirectToRoute("Start"); //Förstör paginering
                }
            }
            catch (Exception e)
            {
                var validationResults = e.Data["ValidationResults"] as ICollection<ValidationResult>;
                if (validationResults != null)
                {
                    foreach (var item in validationResults)
                    {
                        ModelState.AddModelError(String.Empty, item.ErrorMessage);
                    }
                }
                ModelState.AddModelError(String.Empty, "Ett fel inträffade då tabellen skulle uppdateras.");
            }
        }

        public void ContactListView_DeleteItem(int contactID)
        {
            try
            {
                Service.DeleteContact(contactID);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett fel inträffade då posten skulle tas bort.");
            }
        }
    }
}