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
    public sealed partial class ProductDetailView : Page
    {
        int id;
        Product product;
        byte[] currentImageData;

        public ProductDetailView()
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
                idValueBlock.Text = id.ToString();
                nameValueBlock.Text = product.name;
                priceValueBlock.Text = product.price.ToString("#.00");
                typeValueBlock.Text = product.typeAsString;
                stateValueBlock.Text = product.state.ToString();
                loadImage();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(EditProductMenu), id.ToString());
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(SalesView));
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
                memberImage.Source = bitmapSource;
                currentImageData = data;
            });
        }
    }
}
