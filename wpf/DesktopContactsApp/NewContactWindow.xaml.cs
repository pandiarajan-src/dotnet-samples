using ContactsLib;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        private IContactsGateway contactsGateway;
        public NewContactWindow(IContactsGateway gateway)
        {
            contactsGateway = gateway;
            InitializeComponent();
            Owner = App.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void ClearContactButton_Click(object sender, RoutedEventArgs e)
        {
           TxtFirstName.Text = TxtLastName.Text = TxtEmail.Text = TxtMobile.Text = "";
        }

        private void AddNewContactButton_Click(object sender, RoutedEventArgs e)
        {
            Contact? contact = new Contact
            {
                FirstName = TxtFirstName.Text,
                LastName = TxtLastName.Text,
                Email = TxtEmail.Text,
                Mobile = TxtMobile.Text
            };
            try
            {
                contactsGateway.CreateNewContact(contact);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Close();
        }
    }
}
