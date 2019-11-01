using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using netcorelighting.LightingController;
using netcorelighting.Utility;

namespace netcorelighting.Animations {
    public class StillEye : IAnimation {
        public void OnRun(ControllerManager controllerManager) {
            var firstEye = controllerManager.GetMatrixAt(0);
            var secondEye = controllerManager.GetMatrixAt(1);


            PushGrid(controllerManager, EyeSamples.CenterEye, firstEye, secondEye);
            System.Threading.Thread.Sleep(1500);
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
