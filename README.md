# SDRSharp Frequency Stepper Plugin

A plugin for SDRSharp that automatically steps through frequencies from a user-defined start to end point at specified intervals.

## Features

- **Automatic Frequency Stepping**: Steps through frequencies from start to end point
- **Configurable Parameters**: 
  - Start/End frequency (MHz)
  - Step size (MHz) 
  - Step interval (milliseconds)
- **Input Validation**: Ensures logical frequency bounds and hardware compatibility
- **Settings Persistence**: Saves and loads last-used settings
- **Hardware Validation**: Validates frequencies against SDR hardware capabilities
- **Real-time Status**: Shows current frequency and stepping status

## Requirements

- SDRSharp version 1919+ (.NET 9 compatible)
- Windows with .NET 9 Runtime
- Compatible SDR hardware (RTL-SDR, Airspy, HackRF, etc.)

## Build Instructions

1. **Install Dependencies**:
   - Download and install SDRSharp
   - Copy the following SDRSharp assemblies to `FrequencyStepperPlugin/lib/`:
     - `SDRSharp.Common.dll`
     - `SDRSharp.Radio.dll`
     - `SDRSharp.PanView.dll`

2. **Build the Project**:
   ```bash
   dotnet build FrequencyStepperPlugin.sln --configuration Release
   ```

3. **Output Location**:
   - The compiled plugin will be in `FrequencyStepperPlugin/bin/Release/net9.0-windows/`

## Installation

### Method 1: Automatic Recognition (Recommended)

1. Create a new folder in your SDRSharp `Plugins` directory:
   ```
   SDRSharp/Plugins/FrequencyStepperPlugin/
   ```

2. Copy the compiled `FrequencyStepperPlugin.dll` to this folder

3. Restart SDRSharp - the plugin will be automatically recognized

### Method 2: Manual Registration

1. Copy `FrequencyStepperPlugin.dll` to the SDRSharp `Plugins` directory

2. Edit `Plugins.xml` in your SDRSharp directory and add:
   ```xml
   <add key="FrequencyStepperPlugin" value="FrequencyStepperPlugin.FrequencyStepperPlugin, FrequencyStepperPlugin" />
   ```

3. Restart SDRSharp

## Usage

1. **Start SDRSharp** and ensure your SDR hardware is connected
2. **Locate the Plugin**: Look for "Frequency Stepper" in the plugin panel
3. **Configure Parameters**:
   - **Start Frequency**: Beginning frequency in MHz
   - **End Frequency**: Ending frequency in MHz  
   - **Step Size**: Frequency increment in MHz
   - **Step Interval**: Time between steps in milliseconds
4. **Start Stepping**: Click "Start Stepping" to begin
5. **Monitor Progress**: Watch the current frequency and status displays
6. **Stop Anytime**: Click "Stop Stepping" to halt the process

## Example Usage

**FM Band Scan**:
- Start Frequency: 88.0 MHz
- End Frequency: 108.0 MHz
- Step Size: 0.1 MHz
- Step Interval: 1000 ms (1 second)

**VHF Aircraft Band**:
- Start Frequency: 118.0 MHz
- End Frequency: 137.0 MHz
- Step Size: 0.025 MHz
- Step Interval: 500 ms

## Settings

Settings are automatically saved to:
```
%APPDATA%/SDRSharp/Plugins/FrequencyStepperSettings.xml
```

## Hardware Compatibility

The plugin includes validation for common SDR hardware:
- **RTL-SDR**: 24 MHz - 1.766 GHz
- **Airspy**: 24 MHz - 1.8 GHz  
- **HackRF**: 1 MHz - 6 GHz
- **Generic SDR**: 1 MHz - 2 GHz (conservative)

## Troubleshooting

**Plugin Not Loading**:
- Ensure SDRSharp assemblies are in the `lib` directory during build
- Check that the plugin DLL is in the correct folder
- Verify SDRSharp version compatibility (.NET 9+)

**Frequency Validation Errors**:
- Check that frequencies are within your SDR's supported range
- Ensure start frequency is less than end frequency
- Verify step size is reasonable for the frequency range

**Performance Issues**:
- Increase step interval if frequency changes are too fast
- Reduce step size for more precise scanning
- Ensure your SDR hardware can handle the specified parameters

## Development

Built with:
- .NET 9 (Windows)
- Windows Forms for UI
- SDRSharp Plugin API

## License

This project is provided as-is for educational and amateur radio use.