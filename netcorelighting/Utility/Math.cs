using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace netcorelighting.Utility {
    public static class Math {

        public static float Lerp(float a, float b, float t) {
            return (1 - t) * a + t * b;
        } 
    }
}
