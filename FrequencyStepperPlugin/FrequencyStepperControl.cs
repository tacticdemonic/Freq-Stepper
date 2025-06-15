using System;
using System.Drawing;
using System.Windows.Forms;
using SDRSharp.Common;

namespace FrequencyStepperPlugin
{
    public partial class FrequencyStepperControl : UserControl
    {
        private readonly ISharpControl _control;
        private readonly FrequencyStepperLogic _stepperLogic;
        private readonly HardwareValidator _hardwareValidator;

        // UI Controls
        private NumericUpDown _startFreqNumeric;
        private NumericUpDown _endFreqNumeric;
        private NumericUpDown _stepSizeNumeric;
        private NumericUpDown _stepIntervalNumeric;
        private Button _startButton;
        private Button _pauseButton;
        private Button _stopButton;
        private Label _currentFreqLabel;
        private Label _statusLabel;

        public FrequencyStepperControl(ISharpControl control)
        {
            _control = control;
            _stepperLogic = new FrequencyStepperLogic(_control);
            _hardwareValidator = new HardwareValidator(_control);
            
            InitializeComponent();
            SetupEventHandlers();
            LoadSettings();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            // Configure control
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ForeColor = Color.White;
            Size = new Size(250, 350);

            // Create main panel
            var mainPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(25, 25, 25),
                Padding = new Padding(5)
            };

            // Title
            var titleLabel = new Label
            {
                Text = "Frequency Stepper",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 10)
            };

            // Start Frequency
            var startFreqLabel = new Label
            {
                Text = "Start Frequency (MHz):",
                ForeColor = Color.White,
                AutoSize = true
            };
            _startFreqNumeric = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 6000,
                DecimalPlaces = 6,
                Value = 88.0m,
                Width = 200,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };

            // End Frequency
            var endFreqLabel = new Label
            {
                Text = "End Frequency (MHz):",
                ForeColor = Color.White,
                AutoSize = true
            };
            _endFreqNumeric = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 6000,
                DecimalPlaces = 6,
                Value = 108.0m,
                Width = 200,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };

            // Step Size
            var stepSizeLabel = new Label
            {
                Text = "Step Size (MHz):",
                ForeColor = Color.White,
                AutoSize = true
            };
            _stepSizeNumeric = new NumericUpDown
            {
                Minimum = 0.001m,
                Maximum = 100,
                DecimalPlaces = 6,
                Value = 0.1m,
                Width = 200,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };

            // Step Interval
            var stepIntervalLabel = new Label
            {
                Text = "Step Interval (ms):",
                ForeColor = Color.White,
                AutoSize = true
            };
            _stepIntervalNumeric = new NumericUpDown
            {
                Minimum = 100,
                Maximum = 60000,
                Value = 1000,
                Width = 200,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };

            // Control buttons
            _startButton = new Button
            {
                Text = "Start Stepping",
                Width = 200,
                Height = 30,
                BackColor = Color.FromArgb(0, 120, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Standard,
                Margin = new Padding(0, 10, 0, 5)
            };

            _pauseButton = new Button
            {
                Text = "Pause",
                Width = 200,
                Height = 30,
                BackColor = Color.FromArgb(120, 120, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Standard,
                Enabled = false,
                Margin = new Padding(0, 5, 0, 5)
            };

            _stopButton = new Button
            {
                Text = "Stop Stepping",
                Width = 200,
                Height = 30,
                BackColor = Color.FromArgb(120, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Standard,
                Enabled = false,
                Margin = new Padding(0, 5, 0, 10)
            };

            // Status labels
            _currentFreqLabel = new Label
            {
                Text = "Current: 0.000000 MHz",
                ForeColor = Color.Cyan,
                AutoSize = true,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            _statusLabel = new Label
            {
                Text = "Ready",
                ForeColor = Color.LightGreen,
                AutoSize = true,
                Font = new Font("Arial", 8)
            };

            // Add controls to panel
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(startFreqLabel);
            mainPanel.Controls.Add(_startFreqNumeric);
            mainPanel.Controls.Add(endFreqLabel);
            mainPanel.Controls.Add(_endFreqNumeric);
            mainPanel.Controls.Add(stepSizeLabel);
            mainPanel.Controls.Add(_stepSizeNumeric);
            mainPanel.Controls.Add(stepIntervalLabel);
            mainPanel.Controls.Add(_stepIntervalNumeric);
            mainPanel.Controls.Add(_startButton);
            mainPanel.Controls.Add(_pauseButton);
            mainPanel.Controls.Add(_stopButton);
            mainPanel.Controls.Add(_currentFreqLabel);
            mainPanel.Controls.Add(_statusLabel);

            Controls.Add(mainPanel);
            
            // Force color application after controls are added
            ForceNumericControlColors();
            
            ResumeLayout(false);
        }

        private void ForceNumericControlColors()
        {
            // Sometimes numeric controls don't respect initial color settings
            // Force them to use exact RGB colors specified by user
            _startFreqNumeric.BackColor = Color.FromArgb(40, 40, 40);
            _startFreqNumeric.ForeColor = Color.White;
            _endFreqNumeric.BackColor = Color.FromArgb(40, 40, 40);
            _endFreqNumeric.ForeColor = Color.White;
            _stepSizeNumeric.BackColor = Color.FromArgb(40, 40, 40);
            _stepSizeNumeric.ForeColor = Color.White;
            _stepIntervalNumeric.BackColor = Color.FromArgb(40, 40, 40);
            _stepIntervalNumeric.ForeColor = Color.White;
            
            // Force a refresh
            _startFreqNumeric.Refresh();
            _endFreqNumeric.Refresh();
            _stepSizeNumeric.Refresh();
            _stepIntervalNumeric.Refresh();
        }

        private void SetupEventHandlers()
        {
            _startButton.Click += StartButton_Click;
            _pauseButton.Click += PauseButton_Click;
            _stopButton.Click += StopButton_Click;
            
            _stepperLogic.FrequencyChanged += OnFrequencyChanged;
            _stepperLogic.SteppingCompleted += OnSteppingCompleted;
            _stepperLogic.StatusChanged += OnStatusChanged;
            
            // Force colors when control is loaded
            this.Load += (s, e) => ForceNumericControlColors();
            this.VisibleChanged += (s, e) => { if (Visible) ForceNumericControlColors(); };
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            var config = new FrequencyStepperConfig
            {
                StartFrequencyMHz = _startFreqNumeric.Value,
                EndFrequencyMHz = _endFreqNumeric.Value,
                StepSizeMHz = _stepSizeNumeric.Value,
                StepIntervalMs = (int)_stepIntervalNumeric.Value
            };

            _stepperLogic.Start(config);
            
            _startButton.Enabled = false;
            _pauseButton.Enabled = true;
            _stopButton.Enabled = true;
            EnableControls(false);
            
            SaveSettings();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _stepperLogic.Stop();
            
            _startButton.Enabled = true;
            _pauseButton.Enabled = false;
            _stopButton.Enabled = false;
            EnableControls(true);
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (_stepperLogic.IsPaused)
            {
                _stepperLogic.Resume();
                _pauseButton.Text = "Pause";
            }
            else
            {
                _stepperLogic.Pause();
                _pauseButton.Text = "Resume";
            }
        }

        private void OnFrequencyChanged(object sender, FrequencyChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, FrequencyChangedEventArgs>(OnFrequencyChanged), sender, e);
                return;
            }

            _currentFreqLabel.Text = $"Current: {e.FrequencyMHz:F6} MHz";
        }

        private void OnSteppingCompleted(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(OnSteppingCompleted), sender, e);
                return;
            }

            _startButton.Enabled = true;
            _pauseButton.Enabled = false;
            _pauseButton.Text = "Pause";
            _stopButton.Enabled = false;
            EnableControls(true);
        }

        private void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, StatusChangedEventArgs>(OnStatusChanged), sender, e);
                return;
            }

            _statusLabel.Text = e.Status;
        }

        private bool ValidateInputs()
        {
            if (_startFreqNumeric.Value >= _endFreqNumeric.Value)
            {
                MessageBox.Show("Start frequency must be less than end frequency.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_stepSizeNumeric.Value <= 0)
            {
                MessageBox.Show("Step size must be greater than zero.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_stepSizeNumeric.Value >= (_endFreqNumeric.Value - _startFreqNumeric.Value))
            {
                MessageBox.Show("Step size must be smaller than the frequency range.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Hardware validation
            var frequencyValidation = _hardwareValidator.ValidateFrequencyRange(
                _startFreqNumeric.Value, _endFreqNumeric.Value);
            
            if (!frequencyValidation.IsValid)
            {
                MessageBox.Show(frequencyValidation.Message, "Hardware Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var stepValidation = _hardwareValidator.ValidateStepParameters(
                _stepSizeNumeric.Value, (int)_stepIntervalNumeric.Value);
            
            if (!stepValidation.IsValid)
            {
                MessageBox.Show(stepValidation.Message, "Step Parameter Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void EnableControls(bool enabled)
        {
            _startFreqNumeric.Enabled = enabled;
            _endFreqNumeric.Enabled = enabled;
            _stepSizeNumeric.Enabled = enabled;
            _stepIntervalNumeric.Enabled = enabled;
        }

        private void LoadSettings()
        {
            var settings = FrequencyStepperSettings.Load();
            if (settings != null)
            {
                _startFreqNumeric.Value = settings.StartFrequencyMHz;
                _endFreqNumeric.Value = settings.EndFrequencyMHz;
                _stepSizeNumeric.Value = settings.StepSizeMHz;
                _stepIntervalNumeric.Value = settings.StepIntervalMs;
            }
        }

        private void SaveSettings()
        {
            var settings = new FrequencyStepperSettings
            {
                StartFrequencyMHz = _startFreqNumeric.Value,
                EndFrequencyMHz = _endFreqNumeric.Value,
                StepSizeMHz = _stepSizeNumeric.Value,
                StepIntervalMs = (int)_stepIntervalNumeric.Value
            };
            settings.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stepperLogic?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}