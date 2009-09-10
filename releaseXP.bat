@ECHO OFF
:BEGIN

SET path="C:\Documents and Settings\Administrator\My Documents\DCT\obfuscator\Executables"
SET orig="C:\Documents and Settings\Administrator\My Documents\DCT\src\DCT\bin\Release\DCT.exe"
SET obfusc="C:\Documents and Settings\Administrator\My Documents\DCT\DCT release.exe"
SET snpath="C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin"
SET snkey="C:\Documents and Settings\Administrator\My Documents\DCT\src\DCT\key.snk"

ECHO Obfuscating...
CD %path%
START /B /WAIT QNDObfuscate20.exe %orig% %obfusc%

ECHO Re-signing...
CD %snpath%
START /B /WAIT sn.exe -R %obfusc% %snkey%

ECHO Finished
:END