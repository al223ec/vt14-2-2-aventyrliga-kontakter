using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aventyrliga.Model.DAL
{
    public class ContactDAL : DALBase
    {
        public IEnumerable<Contact> GetContacts()
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
                var contacts = new List<Contact>(100);
                var cmd = new SqlCommand("Person.uspGetContacts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var contactIndex = reader.GetOrdinal("ContactID");
                    var firstNameIndex = reader.GetOrdinal("FirstName");
                    var lastNameIndex = reader.GetOrdinal("LastName");
                    var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            ContactID = reader.GetInt32(contactIndex),
                            FirstName = reader.GetString(firstNameIndex),
                            LastName = reader.GetString(lastNameIndex),
                            EmailAddress = reader.GetString(emailAddressIndex)
                        });
                    }
                }
                //var desc = from s in contacts
                //           orderby s descending
                //           select s;

                contacts.TrimExcess();
                return contacts;
            }
        }
        public IEnumerable<Contact> GetContacts(int maximumRows, int startRowIndex, out int totalRowCount)
        {

            using (var conn = CreateConnection())
            {
                var contacts = new List<Contact>(maximumRows);
                var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex + 1;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                cmd.Parameters.Add("@Recordcount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var contactIndex = reader.GetOrdinal("ContactID");
                    var firstNameIndex = reader.GetOrdinal("FirstName");
                    var lastNameIndex = reader.GetOrdinal("LastName");
                    var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            ContactID = reader.GetInt32(contactIndex),
                            FirstName = reader.GetString(firstNameIndex),
                            LastName = reader.GetString(lastNameIndex),
                            EmailAddress = reader.GetString(emailAddressIndex)
                        });
                    }
                }
                totalRowCount = (int)cmd.Parameters["@RecordCount"].Value / maximumRows;
                contacts.TrimExcess();
                return contacts;
            }
        }


        public Contact GetContact(int contactID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("Person.uspGetContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@ContactID", contactID);// Långsamt
                cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactID;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) //Hittat en post
                    {
                        var contactIndex = reader.GetOrdinal("ContactID"); //Hitta respektive index
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        return new Contact
                        {
                            ContactID = reader.GetInt32(contactIndex),
                            FirstName = reader.GetString(firstNameIndex),
                            LastName = reader.GetString(lastNameIndex),
                            EmailAddress = reader.GetString(emailAddressIndex)
                        };
                    }
                }
                return null;
            }
        }

        public void InsertCustomer(Contact contact)
        {
            using (SqlConnection conn = CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("Person.uspAddContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                conn.Open();

                cmd.ExecuteNonQuery();

                contact.ContactID = (int)cmd.Parameters["@ContactID"].Value;
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (SqlConnection conn = CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("Person.uspUpdateContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactID;
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteContact(int contactID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("Person.uspRemoveContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactID;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}