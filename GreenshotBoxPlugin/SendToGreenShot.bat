REM copy file to GreenShot folder
@echo off
cls

set GreenShotPath=%1
set PlugInPath="\Plugins\GreenshotBoxPlugin"

MD %GreenShotPath%%PlugInPath%
copy "bin\Release\GreenshotBoxPlugin.gsp" %GreenShotPath%%PlugInPath%\GreenshotBoxPlugin.gsp

MD %GreenShotPath%\Languages\%PlugInPath%
copy "Languages\*.*" %GreenShotPath%\Languages\%PlugInPath%