:: Compilation file by Typpo
:: Last modified 8/11/2011

@ECHO OFF
:BEGIN

:: Target path for compiled program
SET orig="C:\Users\Ian\DCT\src\DCT\bin\Release\DCT.exe"

:: Target path for obfuscated program
SET obfusc="C:\Users\Ian\DCT\DCT release.exe"

:: Path to directory containg Babel executable
SET babel="C:\Program Files (x86)\Babel"

:: Path to strong name key used to sign/resign executable
SET snkey="C:\Users\Ian\DCT\src\DCT\key.snk"

ECHO Obfuscating...
CD %babel%
START /B /WAIT babel.exe --output %obfusc% --keyfile %snkey% --nostringencrypt --nomsil --novirtual %orig%

ECHO Finished
:END