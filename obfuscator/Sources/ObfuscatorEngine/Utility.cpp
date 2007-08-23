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
//The Original Code is File ObfuscatorEngine.Utility.cpp
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

#include "stdafx.h"

// Determine which header contains a specified virtual address
_IMAGE_SECTION_HEADER *FindSectionForVirtualAddress(DWORD va, _IMAGE_SECTION_HEADER *FirstSection, WORD NumberOfSections)
{
	short x;
	for(x=0; x<NumberOfSections; x++) {
		if (va >= FirstSection->VirtualAddress && va<= (FirstSection->VirtualAddress + FirstSection->Misc.VirtualSize)) {
			return(FirstSection);
		}
		FirstSection++;
	}
	return(NULL);
}

LPVOID FindPhysicalForVirtualAddress(DWORD va, _IMAGE_SECTION_HEADER *FirstSection, WORD NumberOfSections, LPBYTE buffer, DWORD *SizeToEnd)
{
	LPBYTE result;
	_IMAGE_SECTION_HEADER *pSection;
	pSection = FindSectionForVirtualAddress(va, FirstSection, NumberOfSections);
	result= (LPBYTE)(buffer + pSection->PointerToRawData + va - pSection->VirtualAddress);
	if(SizeToEnd) {
		*SizeToEnd = (DWORD)((buffer + pSection->PointerToRawData + pSection->SizeOfRawData) - result);
		}
	return(result);
}

// Ignore null entries
StringInfo **StringInfo::GetSubList(StringInfo *root, bool swapped, ULONG *length)
{
	StringInfo *sptr = root;
	StringInfo **result;
	int x = 0;
	while(sptr) {
		if(sptr->Swapped==swapped && *sptr->pointer ) x++;
		sptr = sptr->next ;
	}
	result = new PSTRINGINFO[x];
	sptr = root;
	x = 0;
	while(sptr) {
		if(sptr->Swapped==swapped && *sptr->pointer) result[x++]=sptr;
		sptr = sptr->next ;
	}
	*length = x;
	return(result);
}


StringInfo **StringInfo::GetSwappedList(StringInfo *root, ULONG *length)
{
	return(GetSubList(root, TRUE, length));
}

StringInfo **StringInfo::GetUnswappedList(StringInfo *root, ULONG *length)
{
	return(GetSubList(root, FALSE, length));
}
