:: Release setup file
:: Last modified 8/12/2011

@ECHO OFF
:BEGIN

:: Target path for compiled program
SET orig="src\DCT\bin\Release\DCT.exe"

:: Target path for obfuscated program
SET obfusc="DCT release.exe"

:: Path to directory containg Babel executable
SET babel="C:\Program Files (x86)\Babel\babel.exe"

:: Path to strong name key used to sign/resign executable
SET snkey="src\DCT\key.snk"

ECHO Obfuscating...
START /B /WAIT %babel% --output %obfusc% --keyfile %snkey% --nostringencrypt --nomsil --novirtual %orig%

ECHO Finished
:END