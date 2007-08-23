//***** BEGIN LICENSE BLOCK *****
//Version: MPL 1.1
//
//The contents of this file are subject to the Mozilla Public License Version 
//1.1 (the "License"); you may not use this file except in compliance with 
//the License. You may obtain a copy of the License at 
//http://www.mozilla.org/MPL/
//
//Software distributed under the License is distributed on an "AS IS" basis,
//WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
//for the specific language governing rights and limitations under the
//License.
//
//The Original Code is File ObfuscatorEngine.stdafx.h
//
//The Initial Developer of the Original Code is
//Desaware Inc.
//Portions created by the Initial Developer are Copyright (C) 2002
//the Initial Developer. All Rights Reserved.
//
//Contributor(s):    
//                   
//
//***** END LICENSE BLOCK *****

// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#define WIN32_LEAN_AND_MEAN		// Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#pragma unmanaged
#include <windows.h>
#include <winnt.h>
#include <corhdr.h>

#include <cor.h>
#using <mscorlib.dll>
#using <system.dll>
#pragma managed

#include "utilities.h"
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Collections;

// TODO: reference additional headers your program requires here
