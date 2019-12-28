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
using Windows.Media.Capture;
using Windows.Storage;
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
    public sealed partial class MemberDetailView : Page
    {
        public byte[] currentImageData;
        public Member member;
        int id;
        public MemberDetailView()
        {
            this.InitializeComponent();
        }

        public MemberDetailView(string id)
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.id = Int32.Parse((string)e.Parameter);
            using (var db = new AppDBContext())
            {
                member = db.Members.Include(m => m.membershipType).Where(m => m.Id == this.id).FirstOrDefault();
                idValueBlock.Text = id.ToString();
                nameValueBlock.Text = member.name;
                lastNameValueBlock.Text = member.lastName;
                if(member.gender)
                {
                    genderValueBlock.Text = Application.Current.Resources["Male"] as string;
                }
                else
                {
                    genderValueBlock.Text = Application.Current.Resources["Female"] as string;
                }
                JMBValueBlock.Text = member.JMB;
                string birthDateString = member.birthDateAsString;
                DateTime birthDateTime = member.birthDate;
                TimeSpan ageSpan = DateTime.Now - birthDateTime;
                int yearSpan = (int)(ageSpan.TotalDays / 365.25);
                birthDateString += ("  (" + yearSpan + ")");
                BirthDateValueBlock.Text = birthDateString;
                JoinedDateValueBlock.Text = member.joinDateAsString;
                UntilDateValueBlock.Text = member.untilDateAsString;
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


        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(EditMemberMenu), id.ToString());
        }

        

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(MembersView));
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
                currentImageData = data;
            });
        }

        private string dateString(DateTime dateTime)
        {
            return dateTime.Day + "." + dateTime.Month + "." + dateTime.Year;
        }
    }
}
