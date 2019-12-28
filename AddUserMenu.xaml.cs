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
    public sealed partial class AddUserMenu : Page
    {
        public AddUserMenu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.id = (int)e.Parameter;
            using (var db = new AppDBContext())
            {
                user = db.Users.Where(m => m.id == this.id).FirstOrDefault();
                userTypeBox.Items.Add(Application.Current.Resources["Trainer"] as string);
                if (user.master) userTypeBox.Items.Add(Application.Current.Resources["Admin"] as string);
                userTypeBox.SelectedIndex = 0;
            }
        }

        private User user;
        private int id;

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu.adminMenuFrame.Navigate(typeof(UsersView), this.id);
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu.adminMenuFrame.Navigate(typeof(UsersView), this.id);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Text;
            
            if (username.Length == 0)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The username field can't be empty"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            if (password.Length == 0)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The password field can't be empty"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            string passwordHash = Util.GetHash(password);

            using (var db = new AppDBContext())
            {
                User oldUser = null;
                oldUser = db.Users.Where(g => g.username == username).FirstOrDefault();
                if (oldUser != null)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["There is already a user with the same username"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                using (var trx = db.Database.BeginTransaction())
                {
                    User user = new User()
                    {
                        username = username,
                        password = passwordHash,
                        active = true,
                        logged = false,
                        master = false,
                        type = userTypeBox.SelectedIndex==0 ? true : false
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    trx.Commit();
                    okButton.IsEnabled = false;
                }
                MessageDialog messageDialog2 = new MessageDialog((Application.Current.Resources["Operation successful"] as string), (Application.Current.Resources["Notification"] as string));
                messageDialog2.ShowAsync();
            }
        }
    }
}
