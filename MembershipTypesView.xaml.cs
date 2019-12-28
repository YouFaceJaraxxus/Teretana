using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MembershipTypesView : Page
    {
        public List<MembershipType> allTypes = null;
        private User user;
        private int id;
        public MembershipTypesView()
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

        private void addMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu.adminMenuFrame.Navigate(typeof(AddMembershipTypeMenu), this.id);
        }

        private void editMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            MembershipType type = (MembershipType)membershipTypesList.SelectedItem;
            if (type != null)
            {
                AdminMenu.adminMenuFrame.Navigate(typeof(EditMembershipTypeMenu), new List<int> { user.id, type.id });
            }
        }

        private void activateMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            MembershipType type = (MembershipType)membershipTypesList.SelectedItem;
            if(type!=null)
            {
                type.active = true;
                using (var db = new AppDBContext())
                {
                    using (var trx = db.Database.BeginTransaction())
                    {
                        db.MembershipTypes.Update(type);
                        db.SaveChanges();
                        trx.Commit();
                    }
                }
                refreshList();
            }
        }

        private void deactivateMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            MembershipType type = (MembershipType)membershipTypesList.SelectedItem;
            if(type!=null)
            {
                type.active = false;
                using (var db = new AppDBContext())
                {
                    using (var trx = db.Database.BeginTransaction())
                    {
                        db.MembershipTypes.Update(type);
                        db.SaveChanges();
                        trx.Commit();
                    }
                }
                refreshList();
            }
        }

        private void refreshList()
        {
            using (var db = new AppDBContext())
            {
                membershipTypesList.ItemsSource = db.MembershipTypes;
                allTypes = db.MembershipTypes.ToList();
            }
        }
    }
}
