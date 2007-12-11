@ECHO OFF
:BEGIN

SET path="obfuscator\Executables"
SET orig="C:\Documents and Settings\Ian\Desktop\DCT svn\src\DCT\bin\Release\DCT.exe"
SET obfusc="C:\Documents and Settings\Ian\Desktop\DCT svn\DCT release.exe"
SET snpath="C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin"
SET snkey="C:\Documents and Settings\Ian\Desktop\DCT svn\src\DCT\key.snk"

CLS

ECHO Obfuscating...
CD %path%
START /WAIT QNDObfuscate20.exe %orig% %obfusc%

ECHO Re-signing...
CD %snpath%
START /WAIT sn.exe -R %obfusc% %snkey%

ECHO Finished
:END