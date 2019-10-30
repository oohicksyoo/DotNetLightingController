using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using System;
using System.Collections.Generic;
using System.Device.Spi;
using System.Drawing;
using System.Text;

namespace netcorelighting.LightingController {
    public class ControllerManager {
        
        private SpiConnectionSettings settings;
        private SpiDevice spi;
        private Ws2812b device;
        //NOTE: 128x128 only uses the first row (i, 0)
        private BitmapImage image;
        private int totalLedCount = 0;
        private List<Matrix> matrices;

        public ControllerManager(List<Matrix> matrices) {

            this.matrices = matrices;
            matrices.ForEach(x => totalLedCount += x.TotalLedCount);

            //Create Settings
            settings = new SpiConnectionSettings(0, 0) {
                ClockFrequency = 2_400_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            //Create SPI interfacing Device
            spi = SpiDevice.Create(settings);
            device = new Ws2812b(spi, totalLedCount);

            image = device.Image;
            ClearLEDs();
        }

        public Matrix GetMatrixAt(int index) {
            if (index >= matrices.Count) {
                return null;
            }

            return matrices[index];
        }

        public void ClearLEDs() {
            matrices.ForEach(x => x.Clear());
            image.Clear();
            PushUpdate();
        }

        public void UpdateMatrices() {
            var currentCount = 0;
            foreach (var matrix in matrices) {
                var colArray = matrix.Colours;

                for (int i = 0; i < colArray.Length; i++) {
                    image.SetPixel(i + currentCount, 0, colArray[i]);
                }

                currentCount += matrix.TotalLedCount;
            }
            PushUpdate();
        }

        public void PushUpdate() {
            device.Update();
        }

        public void ColourAll(Color color) {

            Color c = Color.FromArgb(1, color.R, color.G, color.B);

            for (int i = 0; i < totalLedCount; i++) {
                image.SetPixel(i, 0, c);
            }
            PushUpdate();
        }
    }
}
