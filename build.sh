#!/bin/bash

echo "Building SDRSharp Frequency Stepper Plugin..."
echo

# Check if SDRSharp assemblies exist
if [ ! -f "FrequencyStepperPlugin/lib/SDRSharp.Common.dll" ]; then
    echo "ERROR: SDRSharp.Common.dll not found in FrequencyStepperPlugin/lib/"
    echo "Please copy SDRSharp assemblies to the lib directory first."
    echo "Required files:"
    echo "  - SDRSharp.Common.dll"
    echo "  - SDRSharp.Radio.dll"
    echo "  - SDRSharp.PanView.dll"
    echo
    exit 1
fi

if [ ! -f "FrequencyStepperPlugin/lib/SDRSharp.Radio.dll" ]; then
    echo "ERROR: SDRSharp.Radio.dll not found in FrequencyStepperPlugin/lib/"
    echo "Please copy SDRSharp assemblies to the lib directory first."
    exit 1
fi

if [ ! -f "FrequencyStepperPlugin/lib/SDRSharp.PanView.dll" ]; then
    echo "ERROR: SDRSharp.PanView.dll not found in FrequencyStepperPlugin/lib/"
    echo "Please copy SDRSharp assemblies to the lib directory first."
    exit 1
fi

echo "SDRSharp assemblies found. Building..."
echo

# Build the solution
dotnet build FrequencyStepperPlugin.sln --configuration Release

if [ $? -eq 0 ]; then
    echo
    echo "========================================"
    echo "BUILD SUCCESSFUL!"
    echo "========================================"
    echo
    echo "Plugin location: FrequencyStepperPlugin/bin/Release/net5.0-windows/FrequencyStepperPlugin.dll"
    echo
    echo "Installation:"
    echo "1. Copy FrequencyStepperPlugin.dll to your SDRSharp Plugins directory"
    echo "2. Restart SDRSharp"
    echo "3. Look for 'Frequency Stepper' in the plugin panel"
    echo
else
    echo
    echo "========================================"
    echo "BUILD FAILED!"
    echo "========================================"
    echo "Please check the error messages above."
    echo
fi