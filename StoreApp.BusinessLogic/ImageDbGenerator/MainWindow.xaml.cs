using StoreApp.ProductData;
using StoreApp.ProductData.EF;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ImageDbGenerator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = fileDialog.FileName;
                    Filepath.Text = file;
                    Filepath.ToolTip = file;

                    SaveProductImage(file);

                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    Filepath.Text = null;
                    Filepath.ToolTip = null;
                    break;
            }
        }

        private void SaveProductImage(string file)
        {
            var splitedName = file.Split('.');

            var extension = splitedName[splitedName.Length - 1];

            var stream = File.OpenRead(file);

            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, (int)stream.Length);

            using(ProductDataContext db = new ProductDataContext("StoreAppB"))
            {
                var product = db.Set<Product>().Find(int.Parse(ProductIDTxt.Text));

                ProductImage pi = new ProductImage
                {
                    ImageData = buffer,
                    ImageMimeType = extension,
                    SequenceNumber = short.Parse(SequenceNumber.Text),
                    Product = product
                };

                db.Set<ProductImage>().Add(pi);
                db.SaveChanges();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
