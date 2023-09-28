using System.Collections.Generic;

namespace Picture_GUI
{
    internal class ColorMatrixes
    {
        static float[] noChange = new float[]  //default to no change
            {
                1, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 1, 0, 0,
                0, 0, 0, 1, 0
            };
        static float[] monoChrome = new float[]
        {
                0.21f, 0.72f, 0.07f, 0, 0,
                0.21f, 0.72f, 0.07f, 0, 0,
                0.21f, 0.72f, 0.07f, 0, 0,
                0,     0,     0,     1, 0
        };
        static float[] pastel = new float[]
        {
                0.75f, 0.25f, 0.25f, 0, 0,
                0.25f, 0.75f, 0.25f, 0, 0,
                0.25f, 0.25f, 0.75f, 0, 0,
                0,     0,     0,     1, 0
        };
        static float[] sepia = new float[]
        {
                0.393f, 0.769f, 0.189f, 0, 0,
                0.349f, 0.686f, 0.168f, 0, 0,
                0.272f, 0.534f, 0.131f, 0, 0,
                0,      0,      0,      1, 0
        };
        static float[] colorMatrix1 = new float[]
        {
                0, 1, 0, 0, 0,
                0, 0, 1, 0, 0,
                1, 0, 0, 0, 0,
                0, 0, 0, 1, 0
        };
        static float[] colorMatrix3 = new float[]
        {
                -1, 1, 1, 0, 0,
                1, -1, 1, 0, 0,
                1, 1, -1, 0, 0,
                0, 0, 0, 1, 0
        };
        static float[] colorMatrix4 = new float[]
        {
                0.0f, 0.5f, 0.5f, 0, 0,
                0.5f, 0.0f, 0.5f, 0, 0,
                0.5f, 0.5f, 0.0f, 0, 0,
                0.0f, 0.0f, 0.0f, 1, 0
        };
        static float[] colorMatrix6 = new float[] {
                0, 0, 1, 0, 0,
                1, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 0, 1, 0
            };
<<<<<<< HEAD
        static float[] MatrixElementsRed = new float[] {
=======
            static float[] MatrixElementsRed = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                3, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 1, 0, 0,
                .2f, .2f, .2f, 0, 1
            };
<<<<<<< HEAD
        static float[] MatrixElementsGreen = new float[] {
=======
            static float[] MatrixElementsGreen = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                1, 0, 0, 0, 0,
                0, 3, 0, 0, 0,
                0, 0, 1, 0, 0,
                .2f, .2f, .2f, 0, 1
            };
<<<<<<< HEAD
        static float[] MatrixElementsBlue = new float[] {
=======
            static float[] MatrixElementsBlue = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                1, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 3, 0, 0,
                .2f, .2f, .2f, 0, 1
<<<<<<< HEAD
            };
        static float[] rgbBgr = new float[] {
=======
            };                        
            static float[] rgbBgr = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                0, 0, 1, 0, 0,
                0, 1, 0, 0, 0,
                1, 0, 0, 0, 0,
                0, 0, 0, 1, 0
            };
<<<<<<< HEAD
        static float[] rgbGrb = new float[] {
=======
            static float[] rgbGrb = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                0, 1, 0, 0, 0,
                1, 0, 0, 0, 0,
                0, 0, 1, 0, 0,
                0, 0, 0, 1, 0
            };
<<<<<<< HEAD
        static float[] rgbBrg = new float[] {
=======
            static float[] rgbBrg = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                0, 0, 1, 0, 0,
                1, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 0, 1, 0
            };
<<<<<<< HEAD
        static float[] rgbGbr = new float[] {
=======
            static float[] rgbGbr = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                0, 1, 0, 0, 0,
                0, 0, 1, 0, 0,
                1, 0, 0, 0, 0,
                0, 0, 0, 1, 0
            };
<<<<<<< HEAD
        static float[] rgbRbg = new float[] {
=======
            static float[] rgbRbg = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                1, 0, 0, 0, 0,
                0, 0, 1, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 0, 1, 0
<<<<<<< HEAD
            };
        static float[] polaroid = new float[] {
=======
            };                        
            static float[] polaroid = new float[] {
>>>>>>> c360b403 (Update colorMatrixes)
                1.438f, -0.062f, -0.062f, 0, 0,
                -0.122f, 1.378f, -0.122f, 0, 0,
                -0.016f, -0.016f, 1.483f, 0, 0,
                0, 0, 0, 1, 0
            };
        static float[] inverter = new float[] {
                -1f,  0f,  0f, 0f, 1f,
                0f, -1f,  0f, 0f, 1f,
                0f,  0f, -1f, 0f, 1f,
                0f,  0f,  0f, 1f, 0f
            };
        public static Dictionary<string, float[]> colorMatrixes = new Dictionary<string, float[]>()
            {
                {"No_change", noChange},
                {"MonoChrome", monoChrome},
                {"Pastel", pastel},
                {"Sepia", sepia},
                {"Polaroid", polaroid},
                {"Inverter", inverter},
                {"ColorMatrix1", colorMatrix1},
                {"ColorMatrix3", colorMatrix3},
                {"ColorMatrix4", colorMatrix4},
                {"ColorMatrix6", colorMatrix6},
                {"MatrixElementsRed", MatrixElementsRed},
                {"MatrixElementsGreen", MatrixElementsGreen},
                {"MatrixElementsBlue", MatrixElementsBlue},
                {"Blue|Green|Red", rgbBgr},
<<<<<<< HEAD
                {"Blue|Red|Green", rgbBrg},
=======
                {"Blue|Red|Green", rgbBrg},                
>>>>>>> c360b403 (Update colorMatrixes)
                {"Green|Red|Blue", rgbGrb},
                {"Green|Blue|Red", rgbGbr},
                {"Red|Blue|Green", rgbRbg}
            };
    }
}