using netcorelighting.LightingController;
using System;
using System.Collections.Generic;
using System.Text;

namespace netcorelighting.Animations {
    public interface IAnimation {
        void OnRun(ControllerManager controllerManager);
    }
}
