# SDRSharp Frequency Stepper Plugin

## Overview

This project involves building a plugin for SDRSharp (SDR#) that automatically steps through frequencies from a user-defined start to end point. The frequency stepping continues at intervals specified by the user until manually stopped.

---

## Functional Requirements

* Allow the user to specify:

  * **Start Frequency (Hz)**
  * **End Frequency (Hz)**
  * **Step Size (Hz)**
  * **Step Interval (ms)**
* Automatically increment the SDRSharp tuner frequency according to these settings.
* Allow the user to manually stop the frequency stepping at any point.

---

## Technical Approach

### Plugin Development Framework (2024 Update)

**SDRSharp Current Status:**
* SDRSharp upgraded to .NET 5 (version 1788+) with enhanced plugin SDK
* Current compatible versions: 1919, 1920 beta (as of March 2024)
* Improved plugin development framework with better documentation

**ISharpPlugin Interface:**
```csharp
public interface ISharpPlugin
{
    void Initialize(ISharpControl control);
    void Close();
    bool HasGui { get; }
    UserControl GuiControl { get; }
}
```

**Implementation Requirements:**
* Implement the plugin using SDRSharp's `ISharpPlugin` interface
* Control frequency via SDRSharp's `ISharpControl` interface
* Build a GUI within SDRSharp using Windows Forms (`NumericUpDown`, `Button`, `FlowLayoutPanel`)
* Reference required SDRSharp assemblies: `SDRSharp.radio.dll`, `SDRsharp.panview.dll`, `SDRSharp.common.dll`

### Core Functionality

* Use a `System.Windows.Forms.Timer` for stepping through frequencies to ensure UI responsiveness and ease of integration.
* Manage frequency stepping via the plugin’s UI controls (Start/Stop buttons).

---

## Enhanced Robustness & Optional Features

### Input Validation

* Ensure logical frequency bounds:

  * Start frequency < End frequency
  * Step size positive and less than total range
* Display validation errors clearly in the GUI.

### Tuner Stability

* Introduce small optional delays to account for SDR hardware settling times.
* Ensure stepping frequency is adjusted based on user input for optimal stability.

### State Preservation

* Save and load last-used frequency settings and step interval for improved user convenience.
* Store settings using simple configuration storage (e.g., XML or JSON).

### Hardware Capability Checks

* Automatically validate frequencies against SDR hardware's supported range.
* Clearly notify the user if entered frequencies exceed hardware capabilities.

---

## Deployment

### Modern Deployment (2024)

**Target Framework:**
* Compile the plugin targeting .NET 5+ (compatible with SDRSharp 1788+)
* Ensure compatibility with current SDRSharp versions (1919, 1920 beta)

**Installation Methods:**

**Method 1: Automatic Recognition (Preferred)**
* Create a subfolder within SDRSharp's `Plugins` directory
* Copy compiled DLL and dependencies to the subfolder
* SDRSharp will automatically recognize and load the plugin

**Method 2: Manual Registration**
* Place compiled DLL in SDRSharp's `Plugins` directory
* Register the plugin by updating `Plugins.xml`:

```xml
<add key="FrequencyStepperPlugin" value="Frequency Stepper Plugin.FrequencyStepperPlugin, FrequencyStepperPlugin" />
```

**Development Dependencies:**
* Copy `SDRSharp.radio.dll`, `SDRsharp.panview.dll`, and `SDRSharp.common.dll` to project folders during development
* Ensure references match the target SDRSharp version

---

## Testing and Validation

* Test iterative builds within SDRSharp connected to RTL-SDR or other SDR hardware.
* Verify smooth frequency transitions, user interface responsiveness, and stability.
* Perform boundary testing for frequency limits and UI input validation.

---

## Future Expansion Ideas

* Logging of frequency steps and detected signals.
* Visual indicators or bookmarks on the waterfall display for notable frequencies.
* Integration of automated peak detection and frequency bookmarking.
* Advanced sweeping modes (reverse, looping, custom sequences).

---

## Tools & References

### Current Development Resources (2024)

**Official Resources:**
* [SDRSharp Downloads](https://airspy.com/download/) - Official SDR# downloads and updates
* [SDRSharp Community Plugin Package](https://www.rtl-sdr.com/sdrsharp-community-plugin-package-now-available/) - Community plugin collection

**Development Tutorials:**
* [SDRSharp Plugin Base Tutorial](https://github.com/huktonfonix/sdrSharpPluginBase) - Practical plugin development tutorial
* [ISharpPlugin Interface Source](https://github.com/cgommel/sdrsharp/blob/master/Common/ISharpPlugin.cs) - Interface definition

**Plugin Management:**
* [SDRSharp Plugin Manager](https://github.com/slapec/SDRSharpPluginManager) - Plugin management utility
* [RTL-SDR Plugin List](https://www.rtl-sdr.com/sdrsharp-plugins/) - Comprehensive plugin list

**Development Notes:**
* Limited formal documentation available - rely on community examples and GitHub repositories
* Plugin development primarily example-driven
* Active community with regular updates and version compatibility discussions

---
