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
    public sealed partial class EditMembershipTypeMenu : Page
    {
        public EditMembershipTypeMenu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<int> parameters = (List<int>)e.Parameter;
            this.id = parameters[0];
            this.membershipTypeId = parameters[1];
            using (var db = new AppDBContext())
            {
                user = db.Users.Where(u => u.id == this.id).FirstOrDefault();
                membershipType = db.MembershipTypes.Where(mt => mt.id == this.membershipTypeId).FirstOrDefault();
                nameBox.Text = membershipType.name;
                priceBox.Text = membershipType.price.ToString("#.00");
            }
        }

        private User user;
        private int id;

        private MembershipType membershipType;
        private int membershipTypeId;

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
                using (var db = new AppDBContext())
                {
                    MessageDialog messageDialog;
                    if (!name.Equals(membershipType.name))
                    {
                        MembershipType oldMembershipType = null;
                        oldMembershipType = db.MembershipTypes.Where(mt => mt.name == name).FirstOrDefault();
                        if (oldMembershipType != null)
                        {
                            messageDialog = new MessageDialog((Application.Current.Resources["There is already a membership type with the same name"] as string), (Application.Current.Resources["Error"] as string));
                            messageDialog.ShowAsync();
                            return;
                        }
                    }
                    using (var trx = db.Database.BeginTransaction())
                    {
                        membershipType.name = name;
                        membershipType.price = price;
                        db.MembershipTypes.Update(membershipType);
                        db.SaveChanges();
                        trx.Commit();
                    }
                    messageDialog = new MessageDialog((Application.Current.Resources["Operation successful"] as string), (Application.Current.Resources["Notification"] as string));
                    messageDialog.ShowAsync();
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
