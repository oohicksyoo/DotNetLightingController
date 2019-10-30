using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace netcorelighting.Utility {
    public static class ColorExtensions {

        public static Color SetBrightness(this Color color, float brightness) {
            return Color.FromArgb(color.A, 
                (int)(color.R * brightness), 
                (int)(color.G * brightness), 
                (int)(color.B * brightness)
                );
        } 

    }
}
