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

            //Create Controller Manager
            var controllerManager = new ControllerManager(new List<Matrix>() { 
                new Matrix(8, 8, MatrixType.SnakeEven),
                new Matrix(8, 8, MatrixType.SnakeEven)
            });
            controllerManager.UpdateMatrices();

            //Create Animation List
            List<IAnimation> animations = new List<IAnimation>() {
                new MatrixCornerAnimation(),
                new EyeFadeInAnimation(),
                new EyeFadeOutAnimation(),
                new MatrixCornerAnimation()
            };

            foreach (var animation in animations) {
                animation.OnRun(controllerManager);
            }                      

            while (!Console.KeyAvailable) {}

            controllerManager.ClearLEDs();
            Console.WriteLine("End lighting");
        }
    }
}
