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

namespace DesktopContactsApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {


        public Contact Contact
        {
            get { return (Contact)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), new PropertyMetadata(new Contact { FirstName="First", LastName="Last", Email="example@domain.com", Mobile="000-000-0000"}, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl contactControl = (ContactControl)d;
            if(contactControl != null)
            {
                contactControl.ContactName.Text = ((Contact)e.NewValue).FullName;
                contactControl.ContactEmail.Text = ((Contact)e.NewValue).Email;
                contactControl.ContactMobile.Text = ((Contact)e.NewValue).Mobile;

            }
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
