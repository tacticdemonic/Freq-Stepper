using System;
using System.Windows.Forms;
using SDRSharp.Common;

namespace FrequencyStepperPlugin
{
    public class FrequencyStepperLogic : IDisposable
    {
        private readonly ISharpControl _control;
        private readonly Timer _stepTimer;
        private FrequencyStepperConfig _config;
        private decimal _currentFrequencyMHz;
        private bool _isRunning;

        public event EventHandler<FrequencyChangedEventArgs> FrequencyChanged;
        public event EventHandler SteppingCompleted;
        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        public FrequencyStepperLogic(ISharpControl control)
        {
            _control = control;
            _stepTimer = new Timer();
            _stepTimer.Tick += StepTimer_Tick;
        }

        public void Start(FrequencyStepperConfig config)
        {
            if (_isRunning)
                return;

            _config = config;
            _currentFrequencyMHz = config.StartFrequencyMHz;
            _isRunning = true;

            // Set initial frequency
            SetFrequency(_currentFrequencyMHz);

            // Configure and start timer
            _stepTimer.Interval = config.StepIntervalMs;
            _stepTimer.Start();

            OnStatusChanged("Stepping started");
        }

        public void Stop()
        {
            if (!_isRunning)
                return;

            _stepTimer.Stop();
            _isRunning = false;

            OnStatusChanged("Stepping stopped");
        }

        private void StepTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Move to next frequency
                _currentFrequencyMHz += _config.StepSizeMHz;

                // Check if we've reached the end
                if (_currentFrequencyMHz > _config.EndFrequencyMHz)
                {
                    _stepTimer.Stop();
                    _isRunning = false;
                    OnStatusChanged("Stepping completed");
                    OnSteppingCompleted();
                    return;
                }

                // Set the new frequency
                SetFrequency(_currentFrequencyMHz);
                OnStatusChanged($"Stepping: {_currentFrequencyMHz:F6} MHz");
            }
            catch (Exception ex)
            {
                _stepTimer.Stop();
                _isRunning = false;
                OnStatusChanged($"Error: {ex.Message}");
                OnSteppingCompleted();
            }
        }

        private void SetFrequency(decimal frequencyMHz)
        {
            try
            {
                // Convert MHz to Hz for SDRSharp
                long frequencyHz = (long)(frequencyMHz * 1_000_000);
                
                // Set frequency via SDRSharp control
                _control.Frequency = frequencyHz;
                
                OnFrequencyChanged(frequencyMHz);
            }
            catch (Exception ex)
            {
                OnStatusChanged($"Frequency set error: {ex.Message}");
            }
        }

        protected virtual void OnFrequencyChanged(decimal frequencyMHz)
        {
            FrequencyChanged?.Invoke(this, new FrequencyChangedEventArgs(frequencyMHz));
        }

        protected virtual void OnSteppingCompleted()
        {
            SteppingCompleted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnStatusChanged(string status)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status));
        }

        public void Dispose()
        {
            _stepTimer?.Stop();
            _stepTimer?.Dispose();
        }
    }

    public class FrequencyStepperConfig
    {
        public decimal StartFrequencyMHz { get; set; }
        public decimal EndFrequencyMHz { get; set; }
        public decimal StepSizeMHz { get; set; }
        public int StepIntervalMs { get; set; }
    }

    public class FrequencyChangedEventArgs : EventArgs
    {
        public decimal FrequencyMHz { get; }

        public FrequencyChangedEventArgs(decimal frequencyMHz)
        {
            FrequencyMHz = frequencyMHz;
        }
    }

    public class StatusChangedEventArgs : EventArgs
    {
        public string Status { get; }

        public StatusChangedEventArgs(string status)
        {
            Status = status;
        }
    }
}