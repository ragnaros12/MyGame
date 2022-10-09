using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int _n = 5;
        private Field _field;
        private Image[,] _images;

        public MainWindow()
        {
            InitializeComponent();
            Height = _n * 100 + 100;
            Width = _n * 100 + 100;

            Field.Height = _n * 100;
            Field.Width = _n * 100;

            _images = new Image[_n,_n];
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    Image image = new();
                    image.Width = 100;
                    image.Height = 100;
                    image.Margin = new Thickness(i * 100,j * 100,0,0);
                    
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"./image.png", UriKind.Relative);
                    bi.EndInit();
                    image.Source = bi;
                    
                    _images[i, j] = image;
                    Field.Children.Add(image);
                    image.MouseDown += ImageClick;
                }
            }
            
            _field = new Field(_n , (row, column, index) =>
            {
                Image image = _images[row, column];
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"./image.png", UriKind.Relative);
                if (index.Status == Status.Rotate)
                    bi.Rotation = Rotation.Rotate180;
                else
                    bi.Rotation = Rotation.Rotate0;
                bi.EndInit();
                image.Source = bi;
            });
        }

        private void ImageClick(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if (sender.Equals(_images[i,j]))
                    {
                        _field.Rotate(i, j);
                        if (_field.Check())
                        {
                            Success success = new Success();
                            Close();
                            success.Show();
                        }
                        break;
                    }
                }
            }
        }
    }
}