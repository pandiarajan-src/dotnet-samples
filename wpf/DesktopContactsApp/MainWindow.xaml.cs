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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IContactsGateway contactsGateway;
        public MainWindow()
        {
            contactsGateway = new ContactsGateway();
            InitializeComponent();

            UpdateContactsListView();
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow(contactsGateway);
            newContactWindow.ShowDialog();
            UpdateContactsListView();
        }

        private void ContactsSearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search_text = ((TextBox)sender).Text;
            List<Contact>? contacts = contactsGateway.ListContacts()?.OrderBy(c => c.FullName).ToList();
            ContactsListView.ItemsSource = contacts?.Where<Contact>(c => c.ToString().ToLower().Contains(search_text.ToLower())).ToList();
        }

        private void ContactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactsListView.SelectedItem != null)
            {
                Contact contact = (Contact)ContactsListView.SelectedItem;
                if (contact is not null)
                {
                    EditContactsWindow editWnd = new EditContactsWindow(contact, contactsGateway);
                    editWnd.ShowDialog();
                    UpdateContactsListView();
                }
            }
        }

        private void UpdateContactsListView()
        {
            ContactsListView.ItemsSource = contactsGateway.ListContacts()?.OrderBy(c => c.FullName).ToList();
        }
    }
}
