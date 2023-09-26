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
            static float[] colorMatrixElements = new float[] {
                3, 0, 0, 0, 0,
                0, 1, 0, 0, 0,
                0, 0, 1, 0, 0,
                .2f, .2f, .2f, 0, 1
            };
            static float[] rgbBgr = new float[] {
                0, 0, 1, 0, 0,
                0, 1, 0, 0, 0,
                1, 0, 0, 0, 0,
                0, 0, 0, 1, 0
            };
            static float[] polaroid = new float[] {
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
                {"no change", noChange},
                {"monoChrome", monoChrome},
                {"pastel", pastel},
                {"sepia", sepia},
                {"colorMatrix1", colorMatrix1},
                {"colorMatrix3", colorMatrix3},
                {"colorMatrix4", colorMatrix4},
                {"colorMatrix6", colorMatrix6},
                {"colorMatrixElements", colorMatrixElements},
                {"rgbBgr", rgbBgr},
                {"polaroid", polaroid},
                {"inverter", inverter}
            };
     }
}