using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using netcorelighting.LightingController;
using netcorelighting.Utility;

namespace netcorelighting.Animations {
    public class Wink : IAnimation {
        public void OnRun(ControllerManager controllerManager) {
            var firstEye = controllerManager.GetMatrixAt(0);
            var secondEye = controllerManager.GetMatrixAt(1);

            PushGrid(controllerManager, EyeSamples.CenterEye, secondEye);


            PushGrid(controllerManager, EyeSamples.CenterEye, firstEye);
            System.Threading.Thread.Sleep(1000);
            PushGrid(controllerManager, EyeSamples.Blink1, firstEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink2, firstEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink3, firstEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink4, firstEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink5, firstEye);
            System.Threading.Thread.Sleep(1250);
            PushGrid(controllerManager, EyeSamples.Blink4, firstEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink3, firstEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink2, firstEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink1, firstEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.CenterEye, firstEye);
        }

        private void PushGrid(ControllerManager controllerManager, int[] grid, Matrix eye) {
            for (int j = 0; j < grid.Length; j++) {
                var c = Color.White;

                if (grid[j] == 0) {
                    c = Color.Black;
                } else if (grid[j] == 2) {
                    c = Color.Orange;
                }


                int x = j % eye.Width;
                int y = j / eye.Width;
                eye.SetColor(x, y, c);
            }

            controllerManager.UpdateMatrices();
        }
    }
}
