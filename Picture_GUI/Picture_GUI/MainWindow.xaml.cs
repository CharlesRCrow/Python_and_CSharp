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

                    btnSaveFile.IsEnabled = true;
                    btnSaveFile.Visibility = Visibility.Visible;

                    colorSelection.Visibility = Visibility.Visible;
                    colorLabel.Visibility = Visibility.Visible;

                    resetButton.IsEnabled = true;
                    resetButton.Visibility = Visibility.Visible;

                    RotateButton.IsEnabled = true;
                    RotateButton.Visibility = Visibility.Visible;

                    BrightnessLabel.Visibility = Visibility.Visible;
                    ContrastLabel.Visibility = Visibility.Visible;

                    BrightnessSlider.Visibility = Visibility.Visible;
                    ContrastSlider.Visibility = Visibility.Visible;
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
                saveFileDialog.Filter = "Image files (*.JPG;)|*.JPG;|All files (*.*) | *.*";
                saveFileDialog.DefaultExt = "jpg";

                if (saveFileDialog.ShowDialog() != true)
                {
                    return;
                }
                var encoder = new JpegBitmapEncoder();
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
            if (changedBitmap == null)
            {
                return;
            }
            try
            {
                string key = ((KeyValuePair<string, float[]>)colorSelection.SelectedItem).Key;
                changedBitmap = SelectColorMatrix(changedBitmap, key);
                SKImage image = GenerateImage(changedBitmap);
                myImage.Source = WPFExtensions.ToWriteableBitmap(image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateImage(object sender, EventArgs e)
        {
            changedBitmap = originaBitmap;
            if (IsLoaded)
            {
                float brightness = (float)BrightnessSlider.Value;
                float contrast = (float)ContrastSlider.Value;

            if (ContrastSlider.Value < 1)
            {
                contrast = (float)(ContrastSlider.Value / 2) + 0.5f;
                changedBitmap = ChangeContrast(changedBitmap, contrast);
                SKImage ContrastImage = GenerateImage(changedBitmap);
                myImage.Source = WPFExtensions.ToWriteableBitmap(ContrastImage);  //
            }

            if (sender is Button btn)
            
                {
                if (btn.Name == "RotateButton")
                {
                     changedBitmap = Rotate(changedBitmap);
                     SKImage RotateImage = GenerateImage(changedBitmap);
                     myImage.Source = WPFExtensions.ToWriteableBitmap(RotateImage);
                }
            }

            changedBitmap = ChangeLight(changedBitmap, brightness);
            SKImage image = GenerateImage(changedBitmap);
            myImage.Source = WPFExtensions.ToWriteableBitmap(image);
            }
        }


        private void Reset(object sender, EventArgs e)
        {
            if (originaBitmap == null)
            {
                return;
            }
            try
            {
                changedBitmap = originaBitmap;
                SKImage image = GenerateImage(changedBitmap);
                myImage.Source = WPFExtensions.ToWriteableBitmap(image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        static SKBitmap Rotate(SKBitmap bitmap, double angle = 90.0f)
        {
            double radians = Math.PI * angle / 180;
            float sine = (float)Math.Abs(Math.Sin(radians));
            float cosine = (float)Math.Abs(Math.Cos(radians));
            int originalWidth = bitmap.Width;
            int originalHeight = bitmap.Height;
            int rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
            int rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);

            var rotatedBitmap = new SKBitmap(rotatedWidth, rotatedHeight);

            using (var surface = new SKCanvas(rotatedBitmap))
            {
                surface.Clear();
                surface.Translate(rotatedWidth / 2, rotatedHeight / 2);
                surface.RotateDegrees((float)angle);
                surface.Translate(-originalWidth / 2, -originalHeight / 2);
                surface.DrawBitmap(bitmap, new SKPoint());
            }
            return rotatedBitmap;
        }

        static SKBitmap ChangeContrast(SKBitmap sKBitmap, float contrast)
        {
            SKPaint paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateHighContrast(false, SKHighContrastConfigInvertStyle.NoInvert, contrast)
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
        static SKBitmap ChangeLight(SKBitmap sKBitmap, float brightness)
        {
            float[] bright = new float[]  //default to no change
                {
                    (1+brightness), 0, 0, 0, 0,
                    0, (1+brightness), 0, 0, 0,
                    0, 0, (1+brightness), 0, 0,
                    0, 0, 0, 1, 0
                };
            SKPaint paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateColorMatrix(bright)
            };

            SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height));
            using (SKCanvas sKCanvas = sKSurface.Canvas)
            {
                sKCanvas.DrawBitmap(sKBitmap, new SKPoint(), paint);

                SKImage sKImage = sKSurface.Snapshot();
                sKBitmap = SKBitmap.FromImage(sKImage);

                sKCanvas.ResetMatrix();
                sKCanvas.Flush();
            }

            return sKBitmap;
        }
    }
    
}


