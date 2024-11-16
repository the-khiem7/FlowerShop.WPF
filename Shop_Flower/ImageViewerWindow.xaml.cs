using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Shop_Flower
{
    public partial class ImageViewerWindow : Window
    {
        // Constructor nhận vào string path (file local)
        public ImageViewerWindow(string imagePath)
        {
            InitializeComponent();
            ImageView.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
        }

        // Constructor mới nhận vào Uri (dùng cho URL)
        public ImageViewerWindow(Uri imageUri)
        {
            InitializeComponent();
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = imageUri;
            bitmap.EndInit();
            ImageView.Source = bitmap;
        }
    }
}
