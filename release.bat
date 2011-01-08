:: Compilation file by Typpo
:: Last modified 4/18/2010

@ECHO OFF
:BEGIN

:: Target path for compiled program
SET orig="C:\Documents and Settings\i\My Documents\DCT\src\DCT\bin\Release\DCT.exe"

:: Target path for obfuscated program
SET obfusc="C:\Documents and Settings\i\My Documents\DCT\DCT release.exe"

:: Path to directory containg Babel executable
SET babel="C:\Program Files\Babel"

:: Path to strong name key used to sign/resign executable
SET snkey="C:\Documents and Settings\i\My Documents\DCT\src\DCT\key.snk"

ECHO Obfuscating...
CD %babel%
START /B /WAIT babel.exe --output %obfusc% --keyfile %snkey% --nostringencrypt --nomsil --novirtual %orig%

ECHO Finished
:END