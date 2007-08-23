@ECHO OFF
:BEGIN

SET path="obfuscator\Executables"
SET orig="src\DCT\bin\Release\DCT.exe"
SET obfusc="DCT release.exe"
SET snpath="C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin"
SET snkey="src\DCT\key.snk"

CLS

ECHO Obfuscating...
CD %path%
START /WAIT QNDObfuscate20.exe %orig% %obfusc%

ECHO Re-signing...
CD %snpath%
START /WAIT sn.exe -R %obfusc% %snkey%

ECHO Finished
:END