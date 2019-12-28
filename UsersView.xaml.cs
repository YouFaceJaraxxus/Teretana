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
    public sealed partial class UsersView : Page
    {
        public List<User> allUsers = null;
        private User user;
        private int id;
        public UsersView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.id = (int)e.Parameter;
            using (var db = new AppDBContext())
            {
                user = db.Users.Where(m => m.id == this.id).FirstOrDefault();
                refreshList();
            }
        }

        

        private void editUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)usersList.SelectedItem;
            if (user != null)
            {
                AdminMenu.adminMenuFrame.Navigate(typeof(EditUserMenu), new List<int> { this.user.id, user.id });
            }
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu.adminMenuFrame.Navigate(typeof(AddUserMenu), this.id);
        }

        private void activateUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)usersList.SelectedItem;
            if(user!=null)
            {
                if (user.master)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The master user cannot be activated or deactivated"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                else
                {
                    user.active = true;
                    using (var db = new AppDBContext())
                    {
                        using (var trx = db.Database.BeginTransaction())
                        {
                            db.Users.Update(user);
                            db.SaveChanges();
                            trx.Commit();
                        }
                    }
                }
                refreshList();
            }
        }

        private void deactivateUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)usersList.SelectedItem;
            if(user!=null)
            {
                if (user.master)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The master user cannot be activated or deactivated"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                else
                {
                    user.active = false;
                    using (var db = new AppDBContext())
                    {
                        using (var trx = db.Database.BeginTransaction())
                        {
                            db.Users.Update(user);
                            db.SaveChanges();
                            trx.Commit();
                        }
                    }
                }
                refreshList();
            }
        }

        private void refreshList()
        {
            using (var db = new AppDBContext())
            {
                if (user.master)
                {
                    usersList.ItemsSource = db.Users;
                    allUsers = db.Users.ToList();
                }
                else
                {
                    usersList.ItemsSource = db.Users.Where(u => u.type == true && !u.master);
                    allUsers = db.Users.Where(u => u.type == true && !u.master).ToList();
                }
            }
        }
    }
}
