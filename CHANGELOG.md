# Changelog

All notable changes to the Frequency Stepper Plugin project will be documented in this file.

## v0.0.4 - 15-06-2025

### Added
- **Pause/Resume Functionality**: Added pause button between Start and Stop buttons
  - Pause button allows temporary halting of frequency stepping without losing progress
  - Button text dynamically changes between "Pause" and "Resume" based on current state
  - Frequency position is preserved during pause - stepping resumes from exact pause point
  - Status messages show "Stepping paused" and "Stepping resumed" notifications
  - Proper button state management during start/pause/resume/stop operations

### Fixed
- **Fixed plugin theming to match exact SDRSharp dark theme specification**: Applied user-specified RGB colors for perfect match
  - Main background: `Color.FromArgb(25, 25, 25)` (very dark gray as specified by user color picker)
  - Panel background: `Color.FromArgb(25, 25, 25)` (very dark gray as specified by user color picker)
  - Label text: `Color.White`
  - Numeric control backgrounds: `Color.FromArgb(40, 40, 40)` (slightly lighter than main background for contrast)
  - Numeric control text: `Color.White`
  - Status labels: `Color.Cyan` for frequency, `Color.LightGreen` for status
- Added ForceNumericControlColors() method to ensure dark theme colors are properly applied
- Added Load and VisibleChanged event handlers to force color application when the control is displayed
- Updated button style from `FlatStyle.Flat` to `FlatStyle.Standard` for better integration

### Previous Changes
- Initial project setup with frequency stepping functionality
- Hardware validation implementation
- Plugin integration with SDRSharp 
