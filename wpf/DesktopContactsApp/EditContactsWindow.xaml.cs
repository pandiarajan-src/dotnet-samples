using ContactsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for EditContactsWindow.xaml
    /// </summary>
    public partial class EditContactsWindow : Window
    {
        private Contact _contact;
        private IContactsGateway _contactsGateway;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public EditContactsWindow(Contact contact, IContactsGateway contactsGateway)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();

            Owner = App.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if(contact is null)
            {
                this._contact = new Contact();
            }
            else
            {
                this._contact = contact;
            }
            _contactsGateway = contactsGateway;

            TxtFirstName.Text = _contact?.FirstName;
            TxtLastName.Text = _contact?.LastName;
            TxtEmail.Text = _contact?.Email;
            TxtMobile.Text = _contact?.Mobile;
        }

        private void UpdateContactButton_Click(object sender, RoutedEventArgs e)
        {
            //Contact? contact = new Contact
            //{
            //    FirstName = TxtFirstName.Text,
            //    LastName = TxtLastName.Text,
            //    Email = TxtEmail.Text,
            //    Mobile = TxtMobile.Text
            //};
            _contact.FirstName = TxtFirstName.Text;
            _contact.LastName = TxtLastName.Text;
            _contact.Email = TxtEmail.Text;
            _contact.Mobile = TxtMobile.Text;

            try
            {
                //_contact?.FirstName = (TxtFirstName.Text) ?? null;
                _contactsGateway.UpdateContact(_contact);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void DeleteContactButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _contactsGateway.DeleteContact(_contact);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }
}
