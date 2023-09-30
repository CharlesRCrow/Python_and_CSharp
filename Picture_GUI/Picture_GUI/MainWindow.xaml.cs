using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SkiaSharp.Views.WPF;
using SkiaSharp;
using static Picture_GUI.ColorMatrixes;
using System.Windows.Media;

namespace Picture_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public SKBitmap originalBitmap { get; set; }
        public SKBitmap changedBitmap { get; set; }
        public SKBitmap filterBitmap { get; set; }
        //public int Angle { get; set; }
       

        public MainWindow()
        {
            InitializeComponent();

            colorSelection.ItemsSource = cMatrix;
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
                    ContrastSlider.Value = 0;
                    BrightnessSlider.Value = 0;
                    colorSelection.SelectedIndex = 0;
                    changedBitmap = null;
                    filterBitmap = null;
                    

                    //Uri fileUri = new Uri(openFileDialog.FileName);
                    //BitmapImage bitmapImage = new BitmapImage(fileUri);
                    //myImage.Source = bitmapImage;
                    originalBitmap = SKBitmap.Decode(openFileDialog.FileName);

                    using (SKImage image = GenerateImage(originalBitmap))
                    {
                        myImage.Source = WPFExtensions.ToWriteableBitmap(image);
                    }

                    btnSaveFile.IsEnabled = true;
                    btnSaveFile.Visibility = Visibility.Visible;

                    colorSelection.Visibility = Visibility.Visible;
                    colorLabel.Visibility = Visibility.Visible;

                    mirrorButton.IsEnabled = true;
                    mirrorButton.Visibility = Visibility.Visible;

                    // vFlipButton.IsEnabled = true;
                    //vFlipButton.Visibility = Visibility.Visible;

                    mirrorButton.IsEnabled = true;
                    mirrorButton.Visibility = Visibility.Visible;

                    //rotateButton.IsEnabled = true;
                    //rotateButton.IsEnabled = Visibility.Visible;

                    resetButton.IsEnabled = true;
                    resetButton.Visibility = Visibility.Visible;

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
            if (originalBitmap == null)
            {
                return;
            }
            try
            {
                if (changedBitmap == null)
                {
                    filterBitmap = originalBitmap;
                }
                else
                {
                    filterBitmap = changedBitmap;
                }

                string key = ((KeyValuePair<string, float[]>)colorSelection.SelectedItem).Key;
                filterBitmap = SelectColorMatrix(filterBitmap, key);
                using (SKImage image = GenerateImage(filterBitmap))
                {
                    myImage.Source = WPFExtensions.ToWriteableBitmap(image);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateImage(object sender, EventArgs e)
        {
            if (filterBitmap == null)
            {
                changedBitmap = originalBitmap;
            }
            else
            {
                changedBitmap = filterBitmap;
            }

            if (IsLoaded)
            {
                float brightness = (float)BrightnessSlider.Value;

                if (sender is Button btn)
                {
                    if (btn.Name == "vFlipButton")
                    {
                        changedBitmap = VFlip(changedBitmap);
                    }
                    if (btn.Name == "mirrorButton")
                    {
                        changedBitmap = Mirror(changedBitmap);
                    }
                }

                changedBitmap = ChangeContrast(changedBitmap, (float)ContrastSlider.Value);

                if (brightness != 0.0f)
                {
                    changedBitmap = ChangeLight(changedBitmap, brightness);
                }
                using (SKImage image = GenerateImage(changedBitmap))
                {
                    myImage.Source = WPFExtensions.ToWriteableBitmap(image);
                }
            }
        }

        private void Reset(object sender, EventArgs e)
        {
            if (originalBitmap == null)
            {
                return;
            }
            try
            {
                using (SKImage image = GenerateImage(originalBitmap))
                {
                    myImage.Source = WPFExtensions.ToWriteableBitmap(image);
                }

                ContrastSlider.Value = 0;
                BrightnessSlider.Value = 0;
                colorSelection.SelectedIndex = 0;
                changedBitmap = null;
                filterBitmap = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        SKImage GenerateImage(SKBitmap sKBitmap)
        {
            using (SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height)))
            {
                using (SKCanvas sKCanvas = sKSurface.Canvas)
                {
                    sKCanvas.DrawBitmap(sKBitmap, new SKPoint());
                    sKCanvas.ResetMatrix();
                    sKCanvas.Flush();
                }

                SKImage sKImage = sKSurface.Snapshot();
                return sKImage;
            }
        }

        static SKBitmap SelectColorMatrix(SKBitmap sKBitmap, string matrix_name)
        {
            float[] selectedColorMatrix = cMatrix[matrix_name];

            SKPaint paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateColorMatrix(selectedColorMatrix)
            };

            using (SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height)))
            {
                SKCanvas sKCanvas = sKSurface.Canvas;
                sKCanvas.DrawBitmap(sKBitmap, new SKPoint(), paint);

                using (SKImage sKImage = sKSurface.Snapshot())
                {
                    SKBitmap colorBitmap = SKBitmap.FromImage(sKImage);

                    sKCanvas.ResetMatrix();
                    sKCanvas.Flush();

                    return colorBitmap;
                }
            }
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

            using (SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height)))
            {
                using (SKCanvas sKCanvas = sKSurface.Canvas)
                {
                    sKCanvas.DrawBitmap(sKBitmap, new SKPoint(), paint);

                    SKImage sKImage = sKSurface.Snapshot();
                    SKBitmap colorBitmap = SKBitmap.FromImage(sKImage);

                    sKCanvas.ResetMatrix();
                    sKCanvas.Flush();

                    return colorBitmap;
                }
            }
        }
        static SKBitmap ChangeLight(SKBitmap sKBitmap, float brightness)
        {
            float[] bright = new float[]
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

            using (SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height)))
            {
                using (SKCanvas sKCanvas = sKSurface.Canvas)
                {
                    sKCanvas.DrawBitmap(sKBitmap, new SKPoint(), paint);

                    SKImage sKImage = sKSurface.Snapshot();
                    sKBitmap = SKBitmap.FromImage(sKImage);

                    sKCanvas.ResetMatrix();
                    sKCanvas.Flush();
                }
            }

            return sKBitmap;
        }

        static SKBitmap Mirror(SKBitmap sKBitmap)
        {
            SKCanvas sKCanvas = new SKCanvas(sKBitmap);
            using (new SKAutoCanvasRestore(sKCanvas, true))
            {
                sKCanvas.Scale(-1, 1, sKBitmap.Width / 2.0f, 0);
                sKCanvas.DrawBitmap(sKBitmap, 0, 0);
            }
            //mirrored ^= true;
            return sKBitmap;
        }
        static SKBitmap VFlip(SKBitmap sKBitmap)
        {
            SKCanvas sKCanvas = new SKCanvas(sKBitmap);
            using (new SKAutoCanvasRestore(sKCanvas, true))
            {
                sKCanvas.Scale(1, -1, 0, sKBitmap.Height);
                sKCanvas.DrawBitmap(sKBitmap, 0, 0);
            }
            return sKBitmap;
        }
    }
}


