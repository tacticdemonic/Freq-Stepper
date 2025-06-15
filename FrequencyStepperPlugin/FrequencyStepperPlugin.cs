using System;
using System.Windows.Forms;
using SDRSharp.Common;

namespace FrequencyStepperPlugin
{
    public class FrequencyStepperPlugin : ISharpPlugin
    {
        private ISharpControl _control;
        private FrequencyStepperControl _guiControl;

        public string DisplayName => "Frequency Stepper";

        public bool HasGui => true;

        public UserControl GuiControl => _guiControl;

        public void Initialize(ISharpControl control)
        {
            _control = control;
            _guiControl = new FrequencyStepperControl(_control);
        }

        public void Close()
        {
            _guiControl?.Dispose();
        }
    }
}