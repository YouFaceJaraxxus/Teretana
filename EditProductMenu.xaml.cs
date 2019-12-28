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
    public sealed partial class EditProductMenu : Page
    {
        int id;
        Product product;
        byte[] currentImageData;
        public EditProductMenu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.id = Int32.Parse((string)e.Parameter);
            using (var db = new AppDBContext())
            {
                product = db.Products.Where(p => p.Id == this.id).FirstOrDefault();
                nameBox.Text = product.name;
                priceBox.Text = product.price.ToString("#.00");
                if (product.type)
                {
                    equipmentButton.IsChecked = true;
                }
                else supplementButton.IsChecked = true;
                loadImage();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(SalesView));
        }

        private async void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var data = await Util.GetBytesAsync(file);



                IRandomAccessStream stream = await Util.ConvertTo(data);
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();



                SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied);

                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

                currentImageData = data;

                productImage.Source = bitmapSource;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(SalesView));
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
                    if(!name.Equals(product.name))
                    {
                        Product oldProduct = null;
                        oldProduct = db.Products.Where(mt => mt.name == name).FirstOrDefault();
                        if (oldProduct != null)
                        {
                            MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["There is already a product with the same name"] as string), (Application.Current.Resources["Error"] as string));
                            messageDialog.ShowAsync();
                            return;
                        }
                    }
                    using (var trx = db.Database.BeginTransaction())
                    {
                        product.name = name;
                        product.price = price;
                        product.productImage = currentImageData;
                        product.type = (bool)equipmentButton.IsChecked;
                        db.Products.Update(product);
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


        public async void loadImage()
        {

            var data = product.productImage;

            IRandomAccessStream stream = await Util.ConvertTo(data);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();



            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
            BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Premultiplied);

            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                bitmapSource.SetBitmapAsync(softwareBitmapBGR8);
                productImage.Source = bitmapSource;
                currentImageData = data;
            });
        }
    }
}
