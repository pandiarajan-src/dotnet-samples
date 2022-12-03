using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class Contact
    {
        public Contact() {
            FirstName = "";
            LastName = "";
            Email = "";
            Mobile = "";
        }
        public Contact(string firstName, string lastName, string email, string mobile)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Mobile = mobile;
        }

        [PrimaryKey, AutoIncrement]
        public int ContactID { get; set; }

        [SQLite.MaxLength(50)]
        public string FirstName { get; set; }

        [SQLite.MaxLength(50)]
        public string LastName { get; set; }

        [Ignore]
        public string FullName { get { return $"{LastName}, {FirstName}"; } }

        [SQLite.NotNull]
        public string Email { get; set; }

        public string Mobile { get; set; }

        public override string ToString()
        {
            return $"{FirstName}, {LastName}, {Email}, {Mobile}";
        }
    }

    public interface IContactsGateway
    {
        public bool CreateNewContact(Contact contact);
        public bool DeleteContact(Contact contact );
        public bool UpdateContact(Contact contact );
        public List<Contact>? ListContacts();
        public Contact? GetContact(int contact_id );
    }
}
