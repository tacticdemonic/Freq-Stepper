using System;
using SDRSharp.Common;

namespace FrequencyStepperPlugin
{
    public class HardwareValidator
    {
        private readonly ISharpControl _control;

        public HardwareValidator(ISharpControl control)
        {
            _control = control;
        }

        public ValidationResult ValidateFrequencyRange(decimal startFreqMHz, decimal endFreqMHz)
        {
            try
            {
                // Get hardware frequency limits
                var hardwareLimits = GetHardwareFrequencyLimits();
                
                if (hardwareLimits == null)
                {
                    return new ValidationResult
                    {
                        IsValid = true,
                        Message = "Hardware limits could not be determined - proceeding with user values"
                    };
                }

                var startFreqHz = (long)(startFreqMHz * 1_000_000);
                var endFreqHz = (long)(endFreqMHz * 1_000_000);

                // Check if frequencies are within hardware limits
                if (startFreqHz < hardwareLimits.MinFrequencyHz)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Message = $"Start frequency {startFreqMHz:F6} MHz is below hardware minimum {hardwareLimits.MinFrequencyHz / 1_000_000.0:F6} MHz"
                    };
                }

                if (endFreqHz > hardwareLimits.MaxFrequencyHz)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Message = $"End frequency {endFreqMHz:F6} MHz exceeds hardware maximum {hardwareLimits.MaxFrequencyHz / 1_000_000.0:F6} MHz"
                    };
                }

                return new ValidationResult
                {
                    IsValid = true,
                    Message = $"Frequency range valid for hardware ({hardwareLimits.MinFrequencyHz / 1_000_000.0:F1} - {hardwareLimits.MaxFrequencyHz / 1_000_000.0:F1} MHz)"
                };
            }
            catch (Exception ex)
            {
                return new ValidationResult
                {
                    IsValid = true,
                    Message = $"Hardware validation unavailable: {ex.Message}"
                };
            }
        }

        private HardwareFrequencyLimits GetHardwareFrequencyLimits()
        {
            try
            {
                // Try to get hardware info from SDRSharp control
                // Note: This is a simplified implementation as the actual SDRSharp API
                // may vary depending on the version and hardware type
                
                // Default limits for common SDR hardware
                var limits = new HardwareFrequencyLimits();

                // Try to determine hardware type and set appropriate limits
                if (IsRTLSDR())
                {
                    // RTL-SDR typical limits
                    limits.MinFrequencyHz = 24_000_000;      // 24 MHz
                    limits.MaxFrequencyHz = 1_766_000_000;   // 1766 MHz
                }
                else if (IsAirspy())
                {
                    // Airspy typical limits
                    limits.MinFrequencyHz = 24_000_000;      // 24 MHz
                    limits.MaxFrequencyHz = 1_800_000_000;   // 1800 MHz
                }
                else if (IsHackRF())
                {
                    // HackRF typical limits
                    limits.MinFrequencyHz = 1_000_000;       // 1 MHz
                    limits.MaxFrequencyHz = 6_000_000_000;   // 6 GHz
                }
                else
                {
                    // Generic SDR limits - conservative range
                    limits.MinFrequencyHz = 1_000_000;       // 1 MHz
                    limits.MaxFrequencyHz = 2_000_000_000;   // 2 GHz
                }

                return limits;
            }
            catch
            {
                return null;
            }
        }

        private bool IsRTLSDR()
        {
            try
            {
                // Check if RTL-SDR is being used
                // This would need to be implemented based on actual SDRSharp API
                // For now, return false as we can't determine this reliably
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsAirspy()
        {
            try
            {
                // Check if Airspy is being used
                // This would need to be implemented based on actual SDRSharp API
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsHackRF()
        {
            try
            {
                // Check if HackRF is being used
                // This would need to be implemented based on actual SDRSharp API
                return false;
            }
            catch
            {
                return false;
            }
        }

        public ValidationResult ValidateStepParameters(decimal stepSizeMHz, int stepIntervalMs)
        {
            // Validate step size
            if (stepSizeMHz <= 0)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "Step size must be greater than zero"
                };
            }

            if (stepSizeMHz > 100)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "Step size too large - maximum 100 MHz recommended"
                };
            }

            // Validate step interval
            if (stepIntervalMs < 100)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "Step interval too fast - minimum 100ms required for hardware stability"
                };
            }

            if (stepIntervalMs > 60000)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "Step interval too slow - maximum 60 seconds recommended"
                };
            }

            return new ValidationResult
            {
                IsValid = true,
                Message = "Step parameters are valid"
            };
        }
    }

    public class HardwareFrequencyLimits
    {
        public long MinFrequencyHz { get; set; }
        public long MaxFrequencyHz { get; set; }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}