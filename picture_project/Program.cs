// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO.Enumeration;
using SkiaSharp;
using System.Reflection;
using System.Collections.Generic;


namespace pictures
{
    class Program
    {
        static void Main(string[] args)
        {

            SelectColorMatrix(@"mountain.jpg", "sepia");
            
        }   
        static void SaveImage(SKData sKData, string filename)
        {
            using (FileStream fileStream = File.Create(filename))
            {
                sKData.AsStream().Seek(0, SeekOrigin.Begin);
                sKData.AsStream().CopyTo(fileStream);

                fileStream.Flush();
                fileStream.Close();
            }
        }

        static SKBitmap ReSize(
            string filename,
            float resizeFactor = 2.0f,
            bool changeSize = true)
        {
            SKBitmap skBitmap = SKBitmap.Decode(filename);

            
            if (!changeSize)
            {
                return skBitmap;
            }
            else
            {
                int width = (int)Math.Round(skBitmap.Width * resizeFactor);
                int height = (int)Math.Round(skBitmap.Height * resizeFactor);
                
                SKBitmap resizedBitmap = new SKBitmap(width, height, skBitmap.ColorType, skBitmap.AlphaType);
                SKCanvas canvas = new SKCanvas(resizedBitmap);
                canvas.SetMatrix(SKMatrix.CreateScale(resizeFactor, resizeFactor));
                canvas.DrawBitmap(skBitmap, new SKPoint());
                canvas.ResetMatrix();
                canvas.Flush();
                
                return resizedBitmap;
            }
        }

        static void SelectColorMatrix(string filename, string matrix_name)
        {
            float [] monoChrome = new float[]
            {
                0.21f, 0.72f, 0.07f, 0, 0,
                0.21f, 0.72f, 0.07f, 0, 0,
                0.21f, 0.72f, 0.07f, 0, 0,
                0,     0,     0,     1, 0
            };

            float [] pastel = new float[]
            {
                0.75f, 0.25f, 0.25f, 0, 0,
                0.25f, 0.75f, 0.25f, 0, 0,
                0.25f, 0.25f, 0.75f, 0, 0,
                0,     0,     0,     1, 0
            };

            float [] sepia = new float[]
            {
                0.393f, 0.769f, 0.189f, 0, 0,
                0.349f, 0.686f, 0.168f, 0, 0,
                0.272f, 0.534f, 0.131f, 0, 0,
                0,      0,      0,      1, 0
            };

            float [] colorMatrix1 = new float[] 
            {
                0, 1, 0, 0, 0,
                0, 0, 1, 0, 0,
                1, 0, 0, 0, 0,
                0, 0, 0, 1, 0
            };   

            float [] colorMatrix3 = new float[] 
            {
                -1, 1, 1, 0, 0,
                1, -1, 1, 0, 0,
                1, 1, -1, 0, 0,
                0, 0, 0, 1, 0
            };
                       
            float [] colorMatrix4 = new float[] 
            {
                0.0f, 0.5f, 0.5f, 0, 0,
                0.5f, 0.0f, 0.5f, 0, 0,
                0.5f, 0.5f, 0.0f, 0, 0,
                0.0f, 0.0f, 0.0f, 1, 0
            };

            float [] colorMatrix6 = new float[] {
                0, 0, 1, 0, 0,
                1, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 0, 1, 0
            };

            float [] colorMatrixElements = new float[] {
                3, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 1, 0, 0,
                .2f, .2f, .2f, 0, 1
            };
            float [] rgbBgr = new float[] {
                0, 0, 1, 0, 0,
                0, 1, 0, 0, 0,
                1, 0, 0, 0, 0,
                0, 0, 0, 1, 0
            };
            float [] polaroid = new float[] {
                1.438f, -0.062f, -0.062f, 0, 0,
                -0.122f, 1.378f, -0.122f, 0, 0,
                -0.016f, -0.016f, 1.483f, 0, 0,
                0, 0, 0, 1, 0
            };

            Dictionary<string, float[]> colorMatrixes = new Dictionary<string, float[]>()
            {
                {"monoChrome", monoChrome},
                {"pastel", pastel},
                {"sepia", sepia},
                {"colorMatrix1", colorMatrix1},
                {"colorMatrix3", colorMatrix3}, 
                {"colorMatrix4", colorMatrix4},
                {"colorMatrix6", colorMatrix6},
                {"colorMatrixElements", colorMatrixElements},
                {"rgbBgr", rgbBgr},
                {"polaroid", polaroid}
            };
            
            
            float [] selectedColorMatrix = new float []  //default to no change
            {
                1, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 1, 0, 0,
                0, 0, 0, 1, 0
            };

            if (colorMatrixes.ContainsKey(matrix_name))
            {
                selectedColorMatrix = colorMatrixes[matrix_name];
            }
            
            SKPaint paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateColorMatrix(selectedColorMatrix)
            };

            SKBitmap skBitmap = ReSize(filename);

            SKSurface sKSurface = SKSurface.Create(new SKImageInfo(skBitmap.Width, skBitmap.Height)); //
            SKCanvas sKCanvas = sKSurface.Canvas; //
            sKCanvas.DrawBitmap(skBitmap, new SKPoint(), paint);

            SKImage sKImage = sKSurface.Snapshot();                       
            SKData sKData = sKImage.Encode(SKEncodedImageFormat.Png, 100);

            SaveImage(sKData, @"image2.png");                
        }

        

    }
}    


            // string text = @"Doug is Funny";
            // SKSurface sKSurface_2 = SKSurface.Create(new SKImageInfo(450, 90));
            // SKCanvas sKCanvas_2 = sKSurface.Canvas;
            // // SKPaint paint = new SKPaint();
            
            // paint.Color = SKColors.Black;
            // paint.TextSize = 60;
            // paint.Typeface = SKTypeface.FromFamilyName("Arial");
            // paint.IsAntialias = true;

            // sKCanvas.DrawText(text, 10, 50, paint);
            // SKImage sKImage_2 = sKSurface.Snapshot();
            // SKBitmap sKBitmap_2 = SKBitmap.FromImage(sKImage);
            // SKData sKData_2 = sKBitmap_2.Encode(SKEncodedImageFormat.Png, 100);
        



   
    

    
    
    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //        if (File.Exists("learning.txt"))
    //        {
    //             string learnString =  File.ReadAllText(@"learning.txt");
    //             Console.WriteLine(learnString);
    //        }    
    //     }
    // }


    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         string data;
    //         StreamReader reader = null;
    //         StreamWriter writer = null;
    //         try
    //         {
    //             reader = new StreamReader("learning.txt");
    //             writer = new StreamWriter("learning.txt");
    //             data=reader.ReadLine();

    //             while (data != null)
    //             {
    //                 Console.WriteLine(data);
    //                 data = reader.ReadLine();
    //             }
    //             reader.Close();
    //             writer.WriteLine(@"vnm,jkujidfjkbkljadkmlnasflkjjklasdfljkml,nmvm,");
    //         }
    //         catch (Exception e)
    //         {
    //             Console.WriteLine(e.Message);
    //         }
    //         finally
    //         {
    //             writer.Close();
    //         }
    //     }
    // }

