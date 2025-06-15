using System;
using System.IO;
using System.Xml.Serialization;

namespace FrequencyStepperPlugin
{
    [Serializable]
    public class FrequencyStepperSettings
    {
        public decimal StartFrequencyMHz { get; set; } = 88.0m;
        public decimal EndFrequencyMHz { get; set; } = 108.0m;
        public decimal StepSizeMHz { get; set; } = 0.1m;
        public int StepIntervalMs { get; set; } = 1000;
        public bool LoopContinuously { get; set; } = false;

        private static readonly string SettingsFileName = "FrequencyStepperSettings.xml";
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SDRSharp",
            "Plugins",
            SettingsFileName);

        public static FrequencyStepperSettings Load()
        {
            try
            {
                if (!File.Exists(SettingsPath))
                    return new FrequencyStepperSettings();

                var serializer = new XmlSerializer(typeof(FrequencyStepperSettings));
                using (var reader = new FileStream(SettingsPath, FileMode.Open))
                {
                    return (FrequencyStepperSettings)serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                // Return default settings if loading fails
                return new FrequencyStepperSettings();
            }
        }

        public void Save()
        {
            try
            {
                // Ensure directory exists
                var directory = Path.GetDirectoryName(SettingsPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var serializer = new XmlSerializer(typeof(FrequencyStepperSettings));
                using (var writer = new FileStream(SettingsPath, FileMode.Create))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch (Exception)
            {
                // Silently fail if settings can't be saved
                // This prevents the plugin from crashing if there are permission issues
            }
        }

        public bool IsValid()
        {
            return StartFrequencyMHz >= 0 &&
                   EndFrequencyMHz > StartFrequencyMHz &&
                   StepSizeMHz > 0 &&
                   StepSizeMHz < (EndFrequencyMHz - StartFrequencyMHz) &&
                   StepIntervalMs >= 100;
        }

        public void ValidateAndCorrect()
        {
            // Ensure minimum values
            if (StartFrequencyMHz < 0)
                StartFrequencyMHz = 0;

            if (EndFrequencyMHz <= StartFrequencyMHz)
                EndFrequencyMHz = StartFrequencyMHz + 1;

            if (StepSizeMHz <= 0)
                StepSizeMHz = 0.1m;

            if (StepSizeMHz >= (EndFrequencyMHz - StartFrequencyMHz))
                StepSizeMHz = (EndFrequencyMHz - StartFrequencyMHz) / 10;

            if (StepIntervalMs < 100)
                StepIntervalMs = 100;

            // Ensure maximum values for safety
            if (StartFrequencyMHz > 6000)
                StartFrequencyMHz = 6000;

            if (EndFrequencyMHz > 6000)
                EndFrequencyMHz = 6000;

            if (StepIntervalMs > 60000)
                StepIntervalMs = 60000;
        }
    }
}