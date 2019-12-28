using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teretana
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProlongMembershipMenu : Page
    {
        public Member member;
        int id;
        List<MembershipType> membershipTypes;
        DateTime untilDate;
        public ProlongMembershipMenu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.id = (int)e.Parameter;
            using (var db = new AppDBContext())
            {
                member = db.Members.Include(m => m.membershipType).Where(m => m.Id == this.id).FirstOrDefault();
                membershipTypes = db.MembershipTypes.Where(mt => !mt.name.ToLower().Equals("none") && mt.active).ToList();
                if(membershipTypes!=null)
                {
                    foreach(MembershipType type in membershipTypes)
                    {
                        membershipTypeChoiceBox.Items.Add(type.name);
                    }
                }
                for (int i = 1; i <= 12; i++) numberOfMonthsBox.Items.Add(i.ToString());
                idValueBlock.Text = id.ToString();
                nameValueBlock.Text = member.name;
                lastNameValueBlock.Text = member.lastName;
                untilValueBlock.Text = member.untilDateAsString;
                untilDate = member.untilDate;
                if (member.gender)
                {
                    genderValueBlock.Text = Application.Current.Resources["Male"] as string;
                }
                else
                {
                    genderValueBlock.Text = Application.Current.Resources["Female"] as string;
                }
                JoinedDateValueBlock.Text = member.joinDateAsString;
                string membershipTypeString = member.membershipTypeAsString;
                if (membershipTypeString == null || membershipTypeString.Equals("none"))
                {
                    membershipTypeValueBlock.Text = Application.Current.Resources["None"] as string;
                }
                else membershipTypeValueBlock.Text = membershipTypeString;
                if (member.active)
                {
                    activeValueBlock.Text = Application.Current.Resources["Yes"] as string;
                }
                else
                {
                    activeValueBlock.Text = Application.Current.Resources["No"] as string;
                    activeValueBlock.Foreground = new SolidColorBrush(Colors.Red);
                }
                loadImage();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(MembersView));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(MembersView));
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if(okButton.IsEnabled)
            {
                string selectedMembership = (string)membershipTypeChoiceBox.SelectedItem;
                string selectedMonth = (string)numberOfMonthsBox.SelectedItem;
                if (selectedMembership == null)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["You must select a type of membership"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                if (selectedMonth == null)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["You must select a number of months"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                int numberOfMonths = Int32.Parse(selectedMonth);
                membershipTypeValueBlock.Text = selectedMembership;
                DateTime lastDateTime = member.untilDate;
                TimeSpan ts = TimeSpan.Zero;
                if (DateTime.Compare(lastDateTime, DateTime.Now) > 0)
                {
                    ts = lastDateTime - DateTime.Now;
                }
                lastDateTime = DateTime.Now;
                lastDateTime = lastDateTime.AddMonths(numberOfMonths);
                lastDateTime = lastDateTime.AddDays(ts.TotalDays);
                using (var db = new AppDBContext())
                {
                    member.untilDate = lastDateTime;
                    member.membershipType = db.MembershipTypes.Where(mt => mt.name.Equals(selectedMembership)).FirstOrDefault();
                    member.active = true;
                    activeValueBlock.Text = Application.Current.Resources["Yes"] as string;
                    activeValueBlock.Foreground = new SolidColorBrush(Colors.LightGreen);
                    double price = calculatePrice(member.membershipType.price, numberOfMonths);
                    double realPrice = member.membershipType.price * numberOfMonths;
                    double discount = realPrice - price;
                    MembershipFee fee = new MembershipFee()
                    {
                        price = price,
                        realPrice = realPrice,
                        discount = discount,
                        member = member,
                        membershipType = member.membershipType,
                        date = DateTime.Now,
                        numberOfMonths = numberOfMonths
                    };
                    using (var trx = db.Database.BeginTransaction())
                    {
                        db.Members.Update(member);
                        db.SaveChanges();
                        trx.Commit();
                    }
                    using (var trx = db.Database.BeginTransaction())
                    {
                        db.MembershipFees.Add(fee);
                        db.SaveChanges();
                        trx.Commit();
                    }
                    okButton.IsEnabled = false;
                    membershipTypeBlock.Text = selectedMembership;
                    untilValueBlock.Text = dateString(lastDateTime);
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Operation successful"] as string), (Application.Current.Resources["Notification"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["You cannot prolong the membership any longer"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            
        }

        public async void loadImage()
        {

            var data = member.imgBytes;

            IRandomAccessStream stream = await Util.ConvertTo(data);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();



            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
            BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Premultiplied);

            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                bitmapSource.SetBitmapAsync(softwareBitmapBGR8);
                memberImage.Source = bitmapSource;
            });
        }

        private string dateString(DateTime dateTime)
        {
            return dateTime.Day + "." + dateTime.Month + "." + dateTime.Year;
        }

        private double calculatePrice(double pricePerMonth, int numberOfMonths)
        {
            if (numberOfMonths == 12) return (pricePerMonth * 10);
            else if (numberOfMonths >= 6) return (pricePerMonth * 5);
            else if (numberOfMonths >= 3) return (pricePerMonth * 2.5);
            else return pricePerMonth*numberOfMonths;
        }
    }
}
