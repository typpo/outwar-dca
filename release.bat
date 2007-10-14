@ECHO OFF
:BEGIN

SET path="obfuscator\Executables"
SET orig="C:\Users\Ian\Desktop\svn\trunk\DCT\src\DCT\bin\Release\DCT.exe"
SET obfusc="C:\Users\Ian\Desktop\svn\trunk\DCT\DCT release.exe"
SET snpath="C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin"
SET snkey="C:\Users\Ian\Desktop\svn\trunk\DCT\src\DCT\key.snk"

CLS

ECHO Obfuscating...
CD %path%
START /WAIT QNDObfuscate20.exe %orig% %obfusc%

ECHO Re-signing...
CD %snpath%
START /WAIT sn.exe -R %obfusc% %snkey%

ECHO Finished
:END