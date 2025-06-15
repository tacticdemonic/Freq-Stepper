<!-- File: README.md -->

# SDRSharp Frequency Stepper Plugin

## 📡 Purpose

I created this plugin because I was manually stepping through frequencies in SDR# (SDRSharp) to scan for signals — which was both inefficient and frustrating. Traditional scanning relies heavily on signal strength, but I prefer to _watch the waterfall visually_ for weak or intermittent signals. This plugin solves that problem by automating the frequency stepping, allowing me to focus on the waterfall without constantly interacting with the tuner.

---

## 🛠 Features

- Define **start** and **end** frequency in MHz
- Set **step size** and **step interval** in milliseconds
- **Start**, **Pause/Resume**, and **Stop** stepping at any time
- Real-time frequency display
- Settings are saved between sessions
- Validates input ranges and hardware capability

---

## 🧭 How It Works

The plugin increments your SDRSharp frequency in real time, based on the specified step size and delay. Unlike traditional signal-based scanning, this provides **visual control over scanning** using the waterfall — ideal for catching subtle or short-lived transmissions.

---

## 🧱 Installation

The plugin is **pre-compiled and included** in this repository — no compilation needed!

1. **Deploy** the plugin:
   - Copy `FrequencyStepperPlugin.dll` from the repository into your SDRSharp `Plugins` folder
   - Add to `Plugins.xml`:
     ```xml
     <add key="FrequencyStepperPlugin" value="Frequency Stepper Plugin.FrequencyStepperPlugin, FrequencyStepperPlugin" />
     ```

### 🔨 Compiling from Source (Optional)

If you want to modify and compile the plugin yourself:

1. Place SDRSharp assemblies in `FrequencyStepperPlugin/lib/`:
   ```
   SDRSharp.Common.dll  
   SDRSharp.Radio.dll  
   SDRSharp.PanView.dll
   ```

2. Build the project:
   ```bash
   dotnet build FrequencyStepperPlugin.sln --configuration Release
   ```

3. The compiled plugin will be available at `bin/Release/net9.0-windows/FrequencyStepperPlugin.dll`

---

## 🖥 UI Controls

- **Start Stepping** — begins frequency sweep
- **Pause/Resume** — allows pausing without losing place
- **Stop Stepping** — halts the sweep and resets state
- **Settings** — Start, End, Step Size, Interval (persisted)

---

## ✅ Requirements

- **SDRSharp Version**: Compatible with .NET 9-based versions (v1919+)
- **.NET SDK**: Targeting `.NET 9.0` with Windows Forms support
- **Hardware Support**: Tested with RTL-SDR and Airspy devices

---

## 🔧 Development

### Project Structure

- `FrequencyStepperPlugin.cs` — plugin entry point
- `FrequencyStepperControl.cs` — Windows Forms GUI
- `FrequencyStepperLogic.cs` — logic and frequency control
- `HardwareValidator.cs` — SDR hardware range checks
- `FrequencyStepperSettings.cs` — settings persistence
- `lib/` — contains SDRSharp assembly references (not included)

### Build Instructions

```bash
dotnet build FrequencyStepperPlugin.sln --configuration Release
```

### Output

```
bin/Release/net9.0-windows/FrequencyStepperPlugin.dll
```

---

## 📓 Example Use Case

> "I want to scan 144 MHz to 146 MHz in 12.5 kHz steps, watching the waterfall for weak SSB signals."

- **Start**: 144.000000
- **End**: 146.000000
- **Step Size**: 0.012500
- **Interval**: 500 ms

Start the plugin and observe — you'll see each segment of the band automatically displayed on the waterfall.

---

## 📦 Changelog

See [CHANGELOG.md](./CHANGELOG.md) for release notes.

---

## 🤝 Contributing

This plugin was built for personal use, but feel free to fork, modify, or open issues if you'd like to expand it — future ideas include:

- Frequency bookmarking
- Peak detection
- Logging
- Bidirectional scanning
- Plugin chaining

---

## 📬 Contact

Feel free to open a GitHub issue or submit a pull request if you'd like to contribute.

---

## 🧠 Credits

- Built with ❤️ and C#  
- Inspired by the need for **visual-first scanning**  
- Powered by SDRSharp and .NET

---
