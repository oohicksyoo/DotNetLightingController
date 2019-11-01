using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using netcorelighting.LightingController;
using netcorelighting.Utility;

namespace netcorelighting.Animations {
    public class InterpolateAnimation : IAnimation {

        private int[] firstFrame;
        private int[] lastFrame;
        private int nFrames;
        private int sleepTime;
        private bool startingSleep;
        private bool endSleep;


        public InterpolateAnimation(int[] firstFrame, int[] lastFrame, int sleepTime = 50, bool startingSleep = true, bool endSleep = true) {
            this.firstFrame = firstFrame;
            this.lastFrame = lastFrame;
            this.sleepTime = sleepTime;
            this.startingSleep = startingSleep;
            this.endSleep = endSleep;
            nFrames = 10;
        }

        public void OnRun(ControllerManager controllerManager) {
            var firstEye = controllerManager.GetMatrixAt(0);
            var secondEye = controllerManager.GetMatrixAt(1);

            SetPixels(controllerManager, firstEye, secondEye, 0);
            if (startingSleep) {
                System.Threading.Thread.Sleep(2000);
            }

            float delta = 1/((float)nFrames-1);

            for (int i = 0; i < nFrames; i++) { 
                float t = (float)i * delta;

                SetPixels(controllerManager, firstEye, secondEye, t);
                System.Threading.Thread.Sleep(sleepTime);
            }

            if (startingSleep) {
                System.Threading.Thread.Sleep(250);
            }
        }

        private void SetPixels(ControllerManager controllerManager, Matrix firstEye, Matrix secondEye, float time) {
            //will interpolate the frame
            for (int j = 0; j < firstFrame.Length; j++) {
                int firstFramePixel = firstFrame[j];
                int lastFramePixel = lastFrame[j];
                float brightness = Utility.Math.Lerp((float)firstFramePixel, (float)lastFramePixel, time);

                var c = Color.White.SetBrightness(brightness);
                int x = j % firstEye.Width;
                int y = j / firstEye.Width;
                firstEye.SetColor(x, y, c);
                secondEye.SetColor(x, y, c);
            }

            controllerManager.UpdateMatrices();
        }
    }
}
