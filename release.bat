@ECHO OFF
:BEGIN

SET orig="C:\Documents and Settings\Administrator\My Documents\DCT\src\DCT\bin\Release\DCT.exe"
SET obfusc="C:\Documents and Settings\Administrator\My Documents\DCT\DCT release.exe"
SET babel="C:\Program Files\Babel"
SET snkey="C:\Documents and Settings\Administrator\My Documents\DCT\src\DCT\key.snk"

ECHO Obfuscating...
CD %babel%
START /B /WAIT babel.exe --output %obfusc% --keyfile %snkey% --nostringencrypt --nomsil --novirtual %orig%

ECHO Finished
:END