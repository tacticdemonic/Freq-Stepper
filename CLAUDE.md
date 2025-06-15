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

- Target .NET Framework 4.6.1+ for SDRSharp compatibility
- Output as `.dll` for plugin deployment
- Windows Forms for GUI components
- Plugin registration via `Plugins.xml` configuration

## Deployment Structure

- Compiled DLL goes in SDRSharp's `Plugins` directory
- Plugin registration entry in `Plugins.xml`:
  ```xml
  <add key="FrequencyStepperPlugin" value="Frequency Stepper Plugin.FrequencyStepperPlugin, FrequencyStepperPlugin" />
  ```

## Core Functionality Requirements

- Start/End frequency specification in Hz
- Step size and interval configuration
- Start/Stop controls for frequency stepping
- Input validation for frequency ranges
- State preservation between sessions
- Hardware capability validation against SDR limits