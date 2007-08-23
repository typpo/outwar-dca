This is the version 2.0.1.0 release of the Desaware QND Obfuscator.

This includes both the 1.1 framework and 2.0 framework builds of the obfuscator. Please note that assemblies obfuscated with 
earlier versions of the obfuscator will not load correctly on the 2.0 framework.
Use the newer QNDObfuscate20.exe, with the ObfuscatorLogic20.dll and Obfuscator20.dll components when using .NET 2.0.
Components obfuscated with the 1.1 component should be configured to require the 1.1 framework.

The Desaware QND Obfuscator is distributed under the terms of the Mozilla 1.1 License (included in 
the file license.txt).

The files in the Executables directory represent the official Desaware distribution of the QND Obfuscator
and have been strong name signed by Desaware.

The files in the Sources directory represent the current source drop for the obfuscator.

Please contact the author, dan@desaware.com, if you wish to contribute to the project.

Version 2.0.1.0 Update:
Added -novs option. It turns out Visual Studio 2005 is does not tolerate use of characters > 128 in obfuscated assemblies. Use this 
option only if you are obfuscating components that are not going to be referenced from VS 2005.

Version 2.0.2.0 Update:
Automatically blocks obfuscation of all VB .NET 2005 compiler generated classes in the My namespaces.