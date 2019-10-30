using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using netcorelighting.LightingController;
using netcorelighting.Utility;

namespace netcorelighting.Animations {
    public class EyeFadeInAnimation : IAnimation {
        public void OnRun(ControllerManager controllerManager) {
            var firstEye = controllerManager.GetMatrixAt(0);
            var secondEye = controllerManager.GetMatrixAt(1);
            var eye = EyeSamples.SampleEye;
            for (float brightness = 0; brightness < 1; brightness += 0.1f) {
                for (int i = 0; i < eye.Length; i++) {
                    var c = (eye[i] == 0) ? Color.Black : Color.White.SetBrightness(brightness);
                    int x = i % firstEye.Width;
                    int y = i / firstEye.Width;
                    firstEye.SetColor(x, y, c);
                    secondEye.SetColor(x, y, c);
                }
                controllerManager.UpdateMatrices();
                System.Threading.Thread.Sleep(40);
            }

            System.Threading.Thread.Sleep(5000);
        }
    }
}
