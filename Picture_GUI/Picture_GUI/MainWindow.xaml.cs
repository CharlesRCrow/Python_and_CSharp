using System;
using System.IO;
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
using Microsoft.Win32;
using SkiaSharp.Views.WPF;
using SkiaSharp;
using static System.Net.Mime.MediaTypeNames;
using static Picture_GUI.ColorMatrixes;

namespace Picture_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window

    {
        public SKBitmap originaBitmap { get; set; }
        public SKBitmap changedBitmap { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            colorSelection.ItemsSource = colorMatrixes;
            colorSelection.DisplayMemberPath = "Key";
            colorSelection.SelectedValuePath = "Value";
            colorSelection.SelectedIndex = 0;

        }
        public void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.PNG; *.JPG)|*.PNG;*.JPG|All files (*.*) | *.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    BitmapImage bitmapImage = new BitmapImage(fileUri);
                    myImage.Source = bitmapImage;
                    originaBitmap = SKBitmap.Decode(openFileDialog.FileName);
                    changedBitmap = SKBitmap.Decode(openFileDialog.FileName); //change to clone later

                    //SKBitmap sKBitmap = SKBitmap.Decode(openFileDialog.FileName);
                    //SKBitmap sKBitmapColor = SelectColorMatrix(sKBitmap, "sepia", true);
                    //SKImage image = GenerateImage(sKBitmapColor);
                    //myImage.Source = WPFExtensions.ToWriteableBitmap(image);
                    btnSaveFile.IsEnabled = true;
                    btnSaveFile.Visibility = Visibility.Visible;
                    colorSelection.Visibility= Visibility.Visible;
                    colorLabel.Visibility= Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }
        public void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.Filter = "Image files (*.PNG; *.JPG)|*.PNG;*.JPG|All files (*.*) | *.*";
                saveFileDialog.Filter = "Image files (*.PNG;)|*.PNG;|All files (*.*) | *.*";
                saveFileDialog.DefaultExt = "png";

                if (saveFileDialog.ShowDialog() != true)
                {
                    return;
                }
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)myImage.Source));
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changedBitmap = originaBitmap;
            try
               
            { //string colorChange = colorSelection.SelectedKey as string;
                string key = ((KeyValuePair<string, float[]>)colorSelection.SelectedItem).Key;
                changedBitmap = SelectColorMatrix(changedBitmap, key);
                SKImage image = GenerateImage(changedBitmap);
                myImage.Source = WPFExtensions.ToWriteableBitmap(image);
            }
            catch
            {

            }
        }

        SKImage GenerateImage(SKBitmap sKBitmap)
        {
            SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height));
            SKCanvas sKCanvas = sKSurface.Canvas;
            sKCanvas.DrawBitmap(sKBitmap, new SKPoint());
            sKCanvas.ResetMatrix();
            sKCanvas.Flush();
            SKImage sKImage = sKSurface.Snapshot();
            return sKImage;
        }
        SKBitmap SelectColorMatrix(SKBitmap sKBitmap, string matrix_name)
        {

           float[] selectedColorMatrix = colorMatrixes[matrix_name];
    

            SKPaint paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateColorMatrix(selectedColorMatrix)
            };

            SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height));
            SKCanvas sKCanvas = sKSurface.Canvas;
            sKCanvas.DrawBitmap(sKBitmap, new SKPoint(), paint);

            SKImage sKImage = sKSurface.Snapshot();
            SKBitmap colorBitmap = SKBitmap.FromImage(sKImage);

            sKCanvas.ResetMatrix();
            sKCanvas.Flush();

            return colorBitmap;
        }

    }

}
   

