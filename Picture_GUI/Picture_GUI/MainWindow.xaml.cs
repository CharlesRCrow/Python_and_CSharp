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
<<<<<<< HEAD
        //public int Angle { get; set; }
=======
>>>>>>> c360b403 (Update colorMatrixes)

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
<<<<<<< HEAD
                    originalBitmap = SKBitmap.Decode(openFileDialog.FileName);
                    //Angle = 0;
                    //changedBitmap = SKBitmap.Decode(openFileDialog.FileName); //change to clone later

                    ContrastSlider.Value = 0;
                    BrightnessSlider.Value = 0;
                    colorSelection.SelectedIndex = 0;
                    changedBitmap = null;
                    filterBitmap = null;


=======
                    originaBitmap = SKBitmap.Decode(openFileDialog.FileName);
                    changedBitmap = SKBitmap.Decode(openFileDialog.FileName); //change to clone later
                    
                    
>>>>>>> c360b403 (Update colorMatrixes)
                    btnSaveFile.IsEnabled = true;
                    btnSaveFile.Visibility = Visibility.Visible;

                    colorSelection.Visibility = Visibility.Visible;
                    colorLabel.Visibility = Visibility.Visible;

                    mirrorButton.IsEnabled = true;
                    mirrorButton.Visibility = Visibility.Visible;

                    //rotateButton.IsEnabled = true;
                    //rotateButton.Visibility = Visibility.Visible;

                    mirrorButton.IsEnabled = true;
                    mirrorButton.IsEnabled = Visibility.Visible;

                    rotateButton.IsEnabled = true;
                    rotateButton.IsEnabled = Visibility.Visible;
                    
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
            if (originalBitmap == null)
            {
                return;
<<<<<<< HEAD
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
=======
            }            
            try               
            { 
                string key = ((KeyValuePair<string, float[]>)colorSelection.SelectedItem).Key;
                filterBitmap = SelectColorMatrix(changedBitmap, key);
>>>>>>> c360b403 (Update colorMatrixes)
                SKImage image = GenerateImage(filterBitmap);
                myImage.Source = WPFExtensions.ToWriteableBitmap(image);
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
<<<<<<< HEAD
                float brightness = (float)BrightnessSlider.Value;
                //float contrast = (float)ContrastSlider.Value;

                //if (ContrastSlider.Value < 1)
                //{
                    //float contrast = (float)(ContrastSlider.Value / 2) + 0.5f;
                    //changedBitmap = ChangeContrast(changedBitmap, (float)(ContrastSlider.Value / 2) + 0.5f);
                //}
=======
                if (filterBitmap != null)
                {
                    changedBitmap = filterBitmap;
                    filterBitmap = null;
                }
                float brightness = (float) BrightnessSlider.Value;
                float contrast = (float) ContrastSlider.Value;

                if (ContrastSlider.Value < 1)
                {
                    contrast = (float) (ContastSlider.Value / 2) + 0.5f;
                    changedBitmap = ChangeContrast(changedBitmap, contrast);
                    //SKImage image = GenerateImage(changedBitmap);
                    //myImage.Source = WPFExtensions.ToWriteableBitmap(image);  //
                }
>>>>>>> c360b403 (Update colorMatrixes)

                if (sender is Button btn)
                {
                    if (btn.Name == "rotateButton")
                    {
                        changedBitmap = Rotate(changedBitmap);
<<<<<<< HEAD
=======
                        //SKImage image = GenerateImage(changedBitmap);
                        //myImage.Source = WPFExtensions.ToWriteableBitmap(image);
>>>>>>> c360b403 (Update colorMatrixes)
                    }
                    if (btn.Name == "mirrorButton")
                    {
                        changedBitmap = Mirror(changedBitmap);
                    }
                }
<<<<<<< HEAD
                //changedBitmap = ChangeContrast(changedBitmap, (float)(ContrastSlider.Value / 2) + 0.5f);
                changedBitmap = ChangeContrast(changedBitmap, (float)ContrastSlider.Value);
                //Rotate(changedBitmap, (double) Angle);
                if (brightness != 0.0f)
                {
                    changedBitmap = ChangeLight(changedBitmap, brightness);
                }
                using (SKImage image = GenerateImage(changedBitmap))
                {
                    myImage.Source = WPFExtensions.ToWriteableBitmap(image);
                }
=======
                if (brightness != 0.0f)
                {
                    changedBitmap = ChangeLight(changedBitmap brightness);
                }
                SKImage image = GenerateImage(changedBitmap);
                myImage.Source = WPFExtensions.ToWriteableBitmap(image);
>>>>>>> c360b403 (Update colorMatrixes)
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
<<<<<<< HEAD
                using (SKImage image = GenerateImage(originalBitmap))
                {
                    myImage.Source = WPFExtensions.ToWriteableBitmap(image);
                }

                ContrastSlider.Value = 0;
                BrightnessSlider.Value = 0;
                colorSelection.SelectedIndex = 0;
                changedBitmap = null;
                filterBitmap = null;
=======
                changedBitmap = originalBitmap;
                SKImage image = GenerateImage(changedBitmap);
                myImage.Source = WPFExtensions.ToWriteableBitmap(image);
>>>>>>> c360b403 (Update colorMatrixes)
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
                SKCanvas sKCanvas = sKSurface.Canvas;
                sKCanvas.DrawBitmap(sKBitmap, new SKPoint());
                sKCanvas.ResetMatrix();
                sKCanvas.Flush();
                SKImage sKImage = sKSurface.Snapshot();
                return sKImage;
            }
        }

        static SKBitmap SelectColorMatrix(SKBitmap sKBitmap, string matrix_name)
        {
            float[] selectedColorMatrix = colorMatrixes[matrix_name];

            SKPaint paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateColorMatrix(selectedColorMatrix)
            };

            using (SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height)))
            {
                SKCanvas sKCanvas = sKSurface.Canvas;
                sKCanvas.DrawBitmap(sKBitmap, new SKPoint(), paint);

                SKImage sKImage = sKSurface.Snapshot();
                SKBitmap colorBitmap = SKBitmap.FromImage(sKImage);

                sKCanvas.ResetMatrix();
                sKCanvas.Flush();

                return colorBitmap;
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
<<<<<<< HEAD

        static SKBitmap ChangeContrast(SKBitmap sKBitmap, float contrast)
=======
        
        static SKBitmap ChangeContrast(SKBitmap sKbitmap, float contrast)
>>>>>>> c360b403 (Update colorMatrixes)
        {
            SKPaint paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateHighContrast(false, SKHighContrastConfigInvertStyle.NoInvert, contrast)
            };

<<<<<<< HEAD
            using (SKSurface sKSurface = SKSurface.Create(new SKImageInfo(sKBitmap.Width, sKBitmap.Height)))
=======
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
            float[][] light = new float[][] 
>>>>>>> c360b403 (Update colorMatrixes)
            {
                SKCanvas sKCanvas = sKSurface.Canvas;
                sKCanvas.DrawBitmap(sKBitmap, new SKPoint(), paint);

                SKImage sKImage = sKSurface.Snapshot();
                SKBitmap colorBitmap = SKBitmap.FromImage(sKImage);

                sKCanvas.ResetMatrix();
                sKCanvas.Flush();

                return colorBitmap;
            }
        }
        static SKBitmap ChangeLight(SKBitmap sKBitmap, float brightness)
        {
            float[] bright = new float[]  //default to no change
                {
<<<<<<< HEAD
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
=======
                    g.DrawImage(brightBitmap, rc, 0, 0, brightBitmap.Width, brightBitmap.Height, GraphicsUnit.Pixel, imgattr);
                } 

            sKBitmap = ToSKBitmap(brightBitmap);        
            return sKBitmap;    
>>>>>>> c360b403 (Update colorMatrixes)
        }

        static SKBitmap Mirror(SKBitmap sKBitmap)
        {
            SKCanvas sKCanvas = new SKCanvas(sKBitmap);
<<<<<<< HEAD
            using (new SKAutoCanvasRestore(sKCanvas, true))
=======
            using(new SKAutoCanvasRestore(sKCanvas, true))
>>>>>>> c360b403 (Update colorMatrixes)
            {
                sKCanvas.Scale(-1, 1, sKBitmap.Width / 2.0f, 0);
                sKCanvas.DrawBitmap(sKBitmap, 0, 0);
            }
<<<<<<< HEAD
            return sKBitmap;
        }
=======
            return sKBitmap;            
        }    
>>>>>>> c360b403 (Update colorMatrixes)
    }
}


