using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Teretana
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private void loginButtonClick(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDBContext())
            {
                string username = usernameBox.Text;
                string password = passwordBox.Password;
                if (username == null || username.Equals(""))
                {
                    MessageDialog dialog = new MessageDialog((Application.Current.Resources["The username field can't be empty"] as string), (Application.Current.Resources["Error"] as string));
                    dialog.ShowAsync();
                    return;
                }
                if (password == null || password.Equals(""))
                {
                    MessageDialog dialog = new MessageDialog((Application.Current.Resources["The password field can't be empty"] as string), (Application.Current.Resources["Error"] as string));
                    dialog.ShowAsync();
                    return;
                }
                List<User> users = db.Users.ToList();
                if (users.Any())
                {
                    User user = db.Users.Where(u => u.username.Equals(username)).FirstOrDefault();
                    if(user==null)
                    {
                        MessageDialog dialog = new MessageDialog((Application.Current.Resources["Invalid credentials"] as string), (Application.Current.Resources["Error"] as string));
                        dialog.ShowAsync();
                        return;
                    }
                    else
                    {
                        string passwordHash = Util.GetHash(password);
                        if (passwordHash.Equals(user.password))
                        {
                            if(user.active)
                            {
                                if (user.type) Frame.Navigate(typeof(TrainerMenu), user.id);
                                else Frame.Navigate(typeof(AdminMenu), user.id);
                            }
                            else
                            {
                                MessageDialog dialog = new MessageDialog((Application.Current.Resources["The user account has been disabled"] as string), (Application.Current.Resources["Error"] as string));
                                dialog.ShowAsync();
                                return;
                            }
                            
                        }
                        else
                        {
                            MessageDialog dialog = new MessageDialog((Application.Current.Resources["Invalid credentials"] as string), (Application.Current.Resources["Error"] as string));
                            dialog.ShowAsync();
                            return;
                        }
                    }
                    
                }
                else
                {
                    MembershipType mt = new MembershipType
                    {
                        active = false,
                        name = "none",
                        price = 0.0
                    };
                    using (var trx = db.Database.BeginTransaction())
                    {
                        db.MembershipTypes.Add(mt);
                        db.SaveChanges();
                        trx.Commit();
                    }
                    using (var trx = db.Database.BeginTransaction())
                    {
                        MessageDialog dialog = new MessageDialog((Application.Current.Resources["This will be your starting user data"] as string + "\nusername: " + username + "\npassword: " + password), (Application.Current.Resources["Notification"] as string));
                        dialog.ShowAsync();
                        User user = new User()
                        {
                            username = usernameBox.Text,
                            password = Util.GetHash(passwordBox.Password),
                            master = true,
                            logged = true,
                            active = true,
                            type = false
                        };
                        db.Users.Add(user);
                        db.SaveChanges();
                        trx.Commit();
                        Frame.Navigate(typeof(AdminMenu), user.id);
                    }
                }
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

    }
}
