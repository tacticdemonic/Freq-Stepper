@echo off
echo Building SDRSharp Frequency Stepper Plugin...
echo.

REM Check if SDRSharp assemblies exist
if not exist "FrequencyStepperPlugin\lib\SDRSharp.Common.dll" (
    echo ERROR: SDRSharp.Common.dll not found in FrequencyStepperPlugin\lib\
    echo Please copy SDRSharp assemblies to the lib directory first.
    echo Required files:
    echo   - SDRSharp.Common.dll
    echo   - SDRSharp.Radio.dll
    echo   - SDRSharp.PanView.dll
    echo.
    pause
    exit /b 1
)

if not exist "FrequencyStepperPlugin\lib\SDRSharp.Radio.dll" (
    echo ERROR: SDRSharp.Radio.dll not found in FrequencyStepperPlugin\lib\
    echo Please copy SDRSharp assemblies to the lib directory first.
    pause
    exit /b 1
)

if not exist "FrequencyStepperPlugin\lib\SDRSharp.PanView.dll" (
    echo ERROR: SDRSharp.PanView.dll not found in FrequencyStepperPlugin\lib\
    echo Please copy SDRSharp assemblies to the lib directory first.
    pause
    exit /b 1
)

echo SDRSharp assemblies found. Building...
echo.

REM Build the solution
dotnet build FrequencyStepperPlugin.sln --configuration Release

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo BUILD SUCCESSFUL!
    echo ========================================
    echo.
    echo Plugin location: FrequencyStepperPlugin\bin\Release\net9.0-windows\FrequencyStepperPlugin.dll
    echo.
    echo Installation:
    echo 1. Copy FrequencyStepperPlugin.dll to your SDRSharp Plugins directory
    echo 2. Restart SDRSharp
    echo 3. Look for "Frequency Stepper" in the plugin panel
    echo.
) else (
    echo.
    echo ========================================
    echo BUILD FAILED!
    echo ========================================
    echo Please check the error messages above.
    echo.
)

pause