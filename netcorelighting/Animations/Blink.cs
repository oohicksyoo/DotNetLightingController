using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using netcorelighting.LightingController;
using netcorelighting.Utility;

namespace netcorelighting.Animations {
    public class Blink : IAnimation {
        public void OnRun(ControllerManager controllerManager) {
            var firstEye = controllerManager.GetMatrixAt(0);
            var secondEye = controllerManager.GetMatrixAt(1);


            PushGrid(controllerManager, EyeSamples.CenterEye, firstEye, secondEye);
            System.Threading.Thread.Sleep(1000);
            PushGrid(controllerManager, EyeSamples.Blink1, firstEye, secondEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink2, firstEye, secondEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink3, firstEye, secondEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink4, firstEye, secondEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink5, firstEye, secondEye);
            System.Threading.Thread.Sleep(500);
            PushGrid(controllerManager, EyeSamples.Blink4, firstEye, secondEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink3, firstEye, secondEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink2, firstEye, secondEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.Blink1, firstEye, secondEye);
            System.Threading.Thread.Sleep(50);
            PushGrid(controllerManager, EyeSamples.CenterEye, firstEye, secondEye);
        }

        private void PushGrid(ControllerManager controllerManager, int[] grid, Matrix firstEye, Matrix secondEye) {
            for (int j = 0; j < grid.Length; j++) {
                var c = Color.White;

                if (grid[j] == 0) {
                    c = Color.Black;
                } else if (grid[j] == 2) {
                    c = Color.Orange;
                }


                int x = j % firstEye.Width;
                int y = j / firstEye.Width;
                firstEye.SetColor(x, y, c);
                secondEye.SetColor(x, y, c);
            }

            controllerManager.UpdateMatrices();
        }
    }
}
