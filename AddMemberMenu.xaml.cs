using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
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
    public sealed partial class AddMemberMenu : Page
    {
        public byte[] currentImageData;
        public AddMemberMenu()
        {
            this.InitializeComponent();
            Task.Run(() => this.loadDefaultImage()).Wait();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(MembersView));
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameBox.Text;
            string lastName = lastNameBox.Text;
            string jmb = JMBBox.Text;
            if (name.Length == 0)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The name field can't be empty"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            if (lastName.Length == 0)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The last name field can't be empty"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            if (jmb.Length != 13)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The JMB field must have 13 characters"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            if (BirthDateCalendar.SelectedDates != null && BirthDateCalendar.SelectedDates.Count() != 1)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Please pick a date"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            DateTimeOffset dateTimeOffset = BirthDateCalendar.SelectedDates[0];
            DateTime birthDateTime = dateTimeOffset.LocalDateTime;
            TimeSpan ageSpan = DateTime.Now - birthDateTime;
            double yearSpan = ageSpan.TotalDays / 365.25;
            if (yearSpan < 12)
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Members must be at least 12 years old"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }

            using (var db = new AppDBContext())
            {
                Member oldMember = null;
                oldMember = db.Members.Where(g => g.JMB == jmb).FirstOrDefault();
                if (oldMember != null)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["There is already a member with the same personal ID"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                using (var trx = db.Database.BeginTransaction())
                {
                    Member member = new Member()
                    {
                        name = name,
                        lastName = lastName,
                        JMB = jmb,
                        birthDate = birthDateTime,
                        joinDate = DateTime.Now,
                        imgBytes = currentImageData,
                        gender = (bool)maleButton.IsChecked,
                        membershipType = db.MembershipTypes.Where(mt => mt.name.ToLower().Equals("none")).FirstOrDefault()
                    };
                    db.Members.Add(member);
                    db.SaveChanges();
                    trx.Commit();
                    okButton.IsEnabled = false;
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Operation successful"] as string), (Application.Current.Resources["Notification"] as string));
                    messageDialog.ShowAsync();
                }
            }
        }

        private void nameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string name = nameBox.Text;
            if (name != string.Empty)
            {
                name = char.ToUpper(name[0]) + name.Substring(1);
                nameBox.Text = name;
            }
        }

        private void lastNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string lastName = lastNameBox.Text;
            if (lastName != string.Empty)
            {
                lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
                lastNameBox.Text = lastName;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(MembersView));
        }

        private async void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }

            var data = await Util.GetBytesAsync(photo);



            IRandomAccessStream stream = await Util.ConvertTo(data);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();



            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
            BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Premultiplied);

            SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

            currentImageData = data;

            memberImage.Source = bitmapSource;

        }

        public async void loadDefaultImage()
        {
            string fname = @"Assets\bodybuilder_icon.png";
            StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile photo = await InstallationFolder.GetFileAsync(fname);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }

            var data = await Util.GetBytesAsync(photo);
            currentImageData = data;


            /*IRandomAccessStream stream = await Util.ConvertTo(data);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();



            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
            BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Premultiplied);

            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                bitmapSource.SetBitmapAsync(softwareBitmapBGR8);
                currentImageData = data;
            });*/
        }


    }
}
