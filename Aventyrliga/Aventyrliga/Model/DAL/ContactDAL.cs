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
        /// <summary>
        /// Hämtar alla poster i databasen
        /// </summary>
        /// <returns>IEnumberable med samtilga poster som contactobject</returns>
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
        /// <summary>
        /// Hämtar poster "sidovis" från databasen. 
        /// </summary>
        /// <param name="maximumRows">Antalet poster per rad</param>
        /// <param name="startRowIndex">Start indexet, den rad poster börjar hämtas ifrån</param>
        /// <param name="totalRowCount"></param>
        /// <returns></returns>
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(maximumRows);

                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
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
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;
                    contacts.TrimExcess();

                    //var desc = from s in contacts
                    //       orderby s descending
                    //       select s;
                    //contacts.Sort();

                    return contacts;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Något gick fel med mssql servern");
                }
            }
        }

        /// <summary>
        /// Hämtar en enskild kontakt från databasen
        /// </summary>
        /// <param name="contactID">Contactens ID</param>
        /// <returns>Null om det misslyckas annars ett Contact obj</returns>
        public Contact GetContact(int contactID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ContactID", contactID);// Långsamt
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactID;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
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
                catch (Exception)
                {
                    throw new ApplicationException("Något gick fel med mssql servern");
                }
            }
        }
        /// <summary>
        /// Lägger till en ny contact till tabellen
        /// </summary>
        /// <param name="contact"></param>
        public void InsertContact(Contact contact)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
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
                catch (Exception)
                {

                    throw new ApplicationException("Något gick fel med mssql servern");
                }
            }
        }
        /// <summary>
        /// Uppdaterar en existerande contact
        /// </summary>
        /// <param name="contact">contact obj</param>
        public void UpdateContact(Contact contact)
        {

            using (SqlConnection conn = CreateConnection())
            {
                try
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
                catch (Exception)
                {

                    throw new ApplicationException("Något gick fel med mssql servern");
                }
            }

        }
        /// <summary>
        /// Tar bort en existerande contact
        /// </summary>
        /// <param name="contactID">Contactens contactID</param>
        public void DeleteContact(int contactID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new ApplicationException("Något gick fel med mssql servern");
                }

            }
        }
    }
}