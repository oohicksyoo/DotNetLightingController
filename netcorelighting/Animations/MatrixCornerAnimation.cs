using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using netcorelighting.LightingController;

namespace netcorelighting.Animations {
    public class MatrixCornerAnimation : IAnimation {
        public void OnRun(ControllerManager controllerManager) {
            var firstEye = controllerManager.GetMatrixAt(0);
            var secondEye = controllerManager.GetMatrixAt(1);
            controllerManager.ClearLEDs();
            firstEye.SetColor(0, 0, Color.Orange);
            secondEye.SetColor(0, 0, Color.Orange);
            controllerManager.UpdateMatrices();

            System.Threading.Thread.Sleep(2000);
        }
    }
}
