using SQLite;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;

namespace ContactsLib
{

    public class ContactsGateway : IContactsGateway, IDisposable
    {
        private SQLiteConnection? connection = null;
        public ContactsGateway()
        {
            connection = new SQLiteConnection("C:\\temp\\contacts.db");
            connection.CreateTable<Contact>();
        }

        public void Dispose()
        {
            connection?.Close(); 
            connection?.Dispose();
        }

        public bool CreateNewContact(Contact contact)
        {
            bool status = false;
            try
            {
                if (connection?.Insert(contact) > 0)
                {
                    status = true;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                status = false;
            }
            finally
            {
            }
            return status;
        }

        public bool DeleteContact(Contact contact)
        {
            bool status = false;
            try
            {
                if( connection?.Delete(contact) > 0)
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                status = false;
            }
            finally
            {
            }
            return status;
        }



        public Contact? GetContact(int contact_id)
        {

            Contact contact = new Contact();
            try
            {
                if(connection is not null)
                {
                    contact = connection.Get<Contact>(contact_id);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            return contact;
        }

        public List<Contact>? ListContacts() => connection?.Table<Contact>().ToList();

        public bool UpdateContact(Contact contact)
        {
            bool status = false;
            try
            {
                if(connection?.Update(contact) > 0)
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                status = false;
            }
            finally
            {
            }
            return status;
        }
    }
}