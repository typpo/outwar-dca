'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1

'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/

'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.

'The Original Code is File assemblyinfo.vb.

'The Initial Developer of the Original Code is
'Desaware Inc.
'Portions created by the Initial Developer are Copyright (C) 2002-2005
'the Initial Developer. All Rights Reserved.

'Contributor(s):    Daniel Appleman
'                   Eli Draluk

'***** END LICENSE BLOCK *****


Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("QNDObfuscate")> 
<Assembly: AssemblyDescription("Command line interface for Desaware QND Obfuscator")> 
<Assembly: AssemblyCompany("Desaware Inc.")> 
<Assembly: AssemblyProduct("Desaware QND-Obfuscator")> 
<Assembly: AssemblyCopyright("Copyright ©2002-2005 by Desaware Inc. All Rights Reserved")> 
<Assembly: AssemblyTrademark("QND-Obfuscator is a trademark of Desaware Inc.")> 
<Assembly: CLSCompliant(True)> 

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("86D9CCE7-2F9B-4372-A9BF-398F1C74EA8D")> 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:

<Assembly: AssemblyVersion("2.0.2.0")> 
#If DISTRIBUTION Then
<Assembly: AssemblyKeyFile("..\keys\qndkey.key")> 
#End If