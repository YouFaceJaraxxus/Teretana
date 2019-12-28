using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class AdminMenu : Page
    {
        public AdminMenu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.id = (int)e.Parameter;
            using (var db = new AppDBContext())
            {
                user = db.Users.Where(m => m.id == this.id).FirstOrDefault();
                using (var trx = db.Database.BeginTransaction())
                {
                    this.user.logged = true;
                    db.Users.Update(user);
                    db.SaveChanges();
                    trx.Commit();
                }
                this.user = user;
                adminMenuFrame = currentFrame;
            }
        }

        public static Frame adminMenuFrame;
        private User user;
        private int id;
        private void PivotTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PivotTab.SelectedIndex == 0)
            {
                currentFrame.Navigate(typeof(MembershipTypesView), this.id);
            }
            else if (PivotTab.SelectedIndex == 1)
            {
                currentFrame.Navigate(typeof(UsersView), this.id);
            }
        }

        private void DarkThemeOption_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["theme"] = "dark";
            MessageDialog dialog = new MessageDialog((Application.Current.Resources["Please restart app to make changes."] as string), (Application.Current.Resources["Notification"] as string));
            dialog.ShowAsync();
        }

        private void LightThemeOption_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["theme"] = "light";
            MessageDialog dialog = new MessageDialog((Application.Current.Resources["Please restart app to make changes."] as string), (Application.Current.Resources["Notification"] as string));
            dialog.ShowAsync();
        }

        private void BlueThemeOption_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["theme"] = "blue";
            MessageDialog dialog = new MessageDialog((Application.Current.Resources["Please restart app to make changes."] as string), (Application.Current.Resources["Notification"] as string));
            dialog.ShowAsync();
        }

        private void englishButtonClick(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["language"] = "english";
            MessageDialog dialog = new MessageDialog((Application.Current.Resources["Please restart app to make changes."] as string), (Application.Current.Resources["Notification"] as string));
            dialog.ShowAsync();
        }

        private void serbianButtonClick(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["language"] = "serbian";
            MessageDialog dialog = new MessageDialog((Application.Current.Resources["Please restart app to make changes."] as string), (Application.Current.Resources["Notification"] as string));
            dialog.ShowAsync();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDBContext())
            {
                using (var trx = db.Database.BeginTransaction())
                {
                    this.user.logged = false;
                    db.Users.Update(user);
                    db.SaveChanges();
                    trx.Commit();
                    Frame.Navigate(typeof(MainPage));
                }
            }
        }
    }
}

