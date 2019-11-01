using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace netcorelighting.LightingController {

    public enum MatrixType {
        Normal = 0,
        SnakeEven,
        SnakeOdd
    }

    public class Matrix {

        public float Brightness {
            get {
                return brightness;
            }
            set {
                brightness = value;
            }
        }

        public int Width {
            get;
            private set;
        }

        public int Height {
            get;
            private set;
        }

        public int TotalLedCount {
            get {
                return leds.Length;
            }
        }

        public Color[] Colours {
            get {
                return leds;
            }
        }

        private Color[] leds;
        private MatrixType matrixType;
        private float brightness = 1;

        public Matrix(int width, int height, MatrixType type = MatrixType.Normal) {
            Width = width;
            Height = height;
            matrixType = type;
            leds = new Color[width * height];
            DefaultColours();
        }

        public void Clear() {
            leds = new Color[Width * Height];
        }

        public void Replace(Color[] newLeds) {
            if (newLeds.Length != leds.Length) {
                return;
            }

            leds = newLeds;
        }

        public void SetColor(int x, int y, Color color) {
            int index = GetIndex(x, y);

            if (matrixType == MatrixType.SnakeEven && y % 2 == 0) {
                index = GetIndex(Width - 1 - x, y);
            } else if (matrixType == MatrixType.SnakeOdd && y % 2 != 0) {
                index = GetIndex(Width - 1 - x, y);
            }

            leds[index] = color;
        }

        private int GetIndex(int x, int y) {
            return x + (int)Width * y;
        }

        public void ColourAll(Color color) {
            for (int i = 0; i < leds.Length; i++) {
                leds[i] = color;
            }
        }

        private void DefaultColours() {
            ColourAll(Color.Black);
        }
    }
}
