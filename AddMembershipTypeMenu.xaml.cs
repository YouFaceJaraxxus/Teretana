using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teretana
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddMembershipTypeMenu : Page
    {
        public AddMembershipTypeMenu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.id = (int)e.Parameter;
            using (var db = new AppDBContext())
            {
                user = db.Users.Where(m => m.id == this.id).FirstOrDefault();
            }
        }

        private User user;
        private int id;

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu.adminMenuFrame.Navigate(typeof(MembershipTypesView), this.id);
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu.adminMenuFrame.Navigate(typeof(MembershipTypesView), this.id);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameBox.Text;
            string priceString = priceBox.Text;

            if (name.Length == 0)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The name field can't be empty"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            if (priceString.Length == 0)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The price field can't be empty"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            try
            {
                double price = Double.Parse(priceString);
                price = Math.Round(price, 2);
                if (price <= 0) throw new FormatException();
                using (var db = new AppDBContext())
                {
                    MembershipType oldMembershipType = null;
                    oldMembershipType = db.MembershipTypes.Where(mt => mt.name == name).FirstOrDefault();
                    if (oldMembershipType != null)
                    {
                        MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["There is already a membership type with the same name"] as string), (Application.Current.Resources["Error"] as string));
                        messageDialog.ShowAsync();
                        return;
                    }
                    using (var trx = db.Database.BeginTransaction())
                    {
                        MembershipType type = new MembershipType()
                        {
                            name = name,
                            active = true,
                            price = price
                        };
                        db.MembershipTypes.Add(type);
                        db.SaveChanges();
                        trx.Commit();
                    }
                    MessageDialog messageDialog2 = new MessageDialog((Application.Current.Resources["Operation successful"] as string), (Application.Current.Resources["Notification"] as string));
                    messageDialog2.ShowAsync();
                }
            }
            catch (FormatException ex)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Invalid number input"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
        }
    }
}
