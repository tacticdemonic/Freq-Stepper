# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an SDRSharp frequency stepper plugin that automatically steps through frequencies from a user-defined start to end point. The plugin integrates with SDRSharp's plugin architecture using the `ISharpPlugin` interface.

## Architecture

The plugin follows SDRSharp's standard plugin pattern:
- Main plugin class implements `ISharpPlugin` interface
- GUI built using Windows Forms integrated into SDRSharp's interface
- Frequency control via SDRSharp's `ISharpControl` interface
- Timer-based frequency stepping using `System.Windows.Forms.Timer`

## Key Components

- **Plugin Entry Point**: Implements `ISharpPlugin` for SDRSharp integration
- **Frequency Controller**: Manages stepping logic and hardware communication
- **GUI Controls**: User interface with NumericUpDown controls for frequency parameters
- **Configuration Management**: Settings persistence using XML or JSON
- **Input Validation**: Ensures logical frequency bounds and hardware compatibility

## Development Requirements

- Target .NET 9+ for current SDRSharp compatibility
- Output as `.dll` for plugin deployment
- Windows Forms for GUI components
- Plugin registration via `Plugins.xml` configuration or automatic recognition

## Deployment Structure

- Compiled DLL goes in SDRSharp's `Plugins` directory
- Plugin registration entry in `Plugins.xml`:
  ```xml
  <add key="FrequencyStepperPlugin" value="Frequency Stepper Plugin.FrequencyStepperPlugin, FrequencyStepperPlugin" />
  ```

## Core Functionality Requirements

- Start/End frequency specification in Hz
- Step size and interval configuration
- Start/Pause/Resume/Stop controls for frequency stepping
- Loop continuously option for indefinite scanning
- Input validation for frequency ranges
- State preservation between sessions
- Hardware capability validation against SDR limits

## Build Commands

**Prerequisites:**
- Place SDRSharp assemblies in `FrequencyStepperPlugin/lib/` directory:
  - `SDRSharp.Common.dll`
  - `SDRSharp.Radio.dll` 
  - `SDRSharp.PanView.dll`

**Build:**
```bash
dotnet build FrequencyStepperPlugin.sln --configuration Release
```

**Build Scripts:**
- Windows: `build.bat`
- Linux/macOS: `build.sh`

**Output Location:**
- `FrequencyStepperPlugin/bin/Release/net9.0-windows/FrequencyStepperPlugin.dll`