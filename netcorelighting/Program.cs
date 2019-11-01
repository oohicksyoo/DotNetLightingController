using System;
using System.Collections.Generic;
using System.Device.Spi;
using System.Drawing;
using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using netcorelighting.Animations;
using netcorelighting.LightingController;
using netcorelighting.Utility;

namespace netcorelighting {
    class Program {

        static void Main(string[] args) {
            Console.WriteLine("Starting lighting");

            Random rand = new Random(1234);

            //Create Controller Manager
            var controllerManager = new ControllerManager(new List<Matrix>() { 
                new Matrix(8, 8, MatrixType.SnakeEven) { Brightness = 0.1f },
                new Matrix(8, 8, MatrixType.SnakeEven) { Brightness = 0.1f },
                new Matrix(5, 1, MatrixType.Normal)
            });
            controllerManager.UpdateMatrices();

            var strip = controllerManager.GetMatrixAt(2);
            strip.ColourAll(Color.Orange);

            //Create Animation List
            List<IAnimation> animations = new List<IAnimation>();

            List<IAnimation> rightLeftBlink = new List<IAnimation>() {
                new InterpolateAnimation(EyeSamples.CenterEye, EyeSamples.RightEye),
                new InterpolateAnimation(EyeSamples.RightEye, EyeSamples.CenterEye),
                new InterpolateAnimation(EyeSamples.CenterEye, EyeSamples.LeftEye),
                new InterpolateAnimation(EyeSamples.LeftEye, EyeSamples.CenterEye),
                new Blink()
            };

            List<IAnimation> eyeRollBlink = new List<IAnimation>() {
                new InterpolateAnimation(EyeSamples.CenterEye, EyeSamples.RightEye),
                new InterpolateAnimation(EyeSamples.RightEye, EyeSamples.TopRightEye, 5, false, false),
                new InterpolateAnimation(EyeSamples.TopRightEye, EyeSamples.NorthEye, 5, false, false),
                new InterpolateAnimation(EyeSamples.NorthEye, EyeSamples.TopLeftEye, 5, false, false),
                new InterpolateAnimation(EyeSamples.TopLeftEye, EyeSamples.LeftEye, 5, false, false),
                new InterpolateAnimation(EyeSamples.LeftEye, EyeSamples.CenterEye),
                new StillEye(),
                new Blink()
            };

            List<IAnimation> blinkBlink = new List<IAnimation>() {
                new StillEye(),
                new Blink(),
                new Blink()
            };

            List<IAnimation> wink = new List<IAnimation>() {
                new StillEye(),
                new Wink()
            };

            List<IAnimation> stillEye = new List<IAnimation>() {
                new StillEye(),
            };

            int stillEyeCounter = 0;
            bool firstRun = true;
            while (true) {
                int value = rand.Next(0, 5);

                if (stillEyeCounter < 5) {
                    value = 4;
                } else {
                    stillEyeCounter = 0;
                }

                if (firstRun) {
                    firstRun = false;
                    value = 1;
                }

                if (value == 0) {
                    animations = rightLeftBlink;
                    Console.WriteLine("Playing Right Left Blink");
                } else if (value == 1) {
                    animations = blinkBlink;
                    Console.WriteLine("Playing Blink Blink");
                } else if (value == 2) {
                    animations = eyeRollBlink;
                    Console.WriteLine("Eye Roll");
                } else if (value == 3) {
                    animations = wink;
                    Console.WriteLine("Wink");
                } else {
                    animations = stillEye;
                }

                foreach (var animation in animations) {
                    animation.OnRun(controllerManager);
                }

                animations = new List<IAnimation>();
                stillEyeCounter++;
            }

            controllerManager.ClearLEDs();
            Console.WriteLine("End lighting");
        }
    }
}
