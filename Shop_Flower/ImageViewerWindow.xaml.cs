using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Shop_Flower
{
    public partial class ImageViewerWindow : Window
    {
        public ImageViewerWindow(string imagePath)
        {
            InitializeComponent();
            ImageView.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
        }
    }
}
