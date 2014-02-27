using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aventyrliga.Model
{
    public class Contact : IComparable<Contact>
    {
        public int ContactID { get; set; }

        [Required(ErrorMessage="Ett förnamn måste anges")]
        [StringLength(50, ErrorMessage = "Förnamnet kan som mest bestå av 50 tecken.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ett efternamn måste anges")]
        [StringLength(50, ErrorMessage = "Efternamnet kan som mest bestå av 50 tecken.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "En emailadress måste anges")]
        [DataType(DataType.EmailAddress)] //Förutsätter att denna bör räcka? 
        [EmailAddress(ErrorMessage="Emailadressen verkar inte vara giltig")]
        public string EmailAddress { get; set; }
        //[RegularExpression(@"^", ErrorMessage = "Mailadressen verkar inte vara i rätt format.")]

        public int CompareTo(Contact other)
        {
            return other.LastName.CompareTo(LastName) * -1;
        }
    }
}