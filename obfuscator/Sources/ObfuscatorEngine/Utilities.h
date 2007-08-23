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
//The Original Code is File ObfuscatorEngine.Utilities.h
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


extern _IMAGE_SECTION_HEADER *FindSectionForVirtualAddress(DWORD va, _IMAGE_SECTION_HEADER *FirstSection, WORD NumberOfSections);
extern LPVOID FindPhysicalForVirtualAddress(DWORD va, _IMAGE_SECTION_HEADER *FirstSection, WORD NumberOfSections, LPBYTE buffer, ULONG __nogc *SizeToEnd);

struct METADATAROOT2
{
	WORD Flags;
	WORD Streams;
};

#define ALIGNLENGTH32(x)	((x+3) & 0xFFFFFFFC)
#define ALIGNSTRING32(x)	(reinterpret_cast<LPSTR>(((reinterpret_cast<__int64>(x))+3) & 0xFFFFFFFC))
#define ALIGNSTRING16(x)	(reinterpret_cast<LPSTR>(((reinterpret_cast<__int64>(x))+1) & 0xFFFFFFFE))

struct METADATAROOT
{
	DWORD Signature;
	WORD MajorVersion;
	WORD MinorVersion;
	DWORD Reserved;
	DWORD Length;
	METADATAROOT2 *GetNextPart()
	{
		LPBYTE ptr;
		ptr = (((LPBYTE)(this+1))+ALIGNLENGTH32(Length));
		return((METADATAROOT2 *)ptr);
	}
};


struct STREAMHEADER
{
	DWORD Offset;
	DWORD Size;
	STREAMHEADER *GetNextStream()
	{
		return((STREAMHEADER *)(((LPBYTE)(this+1)) + ALIGNLENGTH32(lstrlen((LPSTR)(this + 1))+1)));
	}
};
typedef STREAMHEADER* PSTREAMHEADER;

struct METADATATABLES
{
	DWORD	reserved;
	BYTE	MajorVersion;
	BYTE	MinorVersion;
	BYTE	HeapSize;
	BYTE	Reserved;
	DWORD	Valid;
	DWORD	Sorted;


	int BytesPerStringEntry()
	{
		return((HeapSize & 1)?4:2);
	}

};

struct StringInfo
{
	LPSTR pointer;	// Pointer into #Strings heap
	StringInfo *next;	// Pointer to next StringInfo
	ULONG offset;	// offset from start of #Strings heap
	bool Swapped;	// True if mangled
	StringInfo() {
		pointer = NULL;
		next = NULL;
		offset = 0;
		Swapped = FALSE;
	}

	static bool OffsetIsShadowed(StringInfo *root, ULONG offset)
	{
		char varname[512];
		while(root) {
			if(root->offset == offset) {
				if (root->Swapped) {
#if _DEBUG
					wsprintf(varname, "swapped: %s", root->pointer);
					System::Diagnostics::Debug::WriteLine(varname);
#endif
					return(TRUE);
				} 
				else {
#if _DEBUG
					wsprintf(varname, "Not swapped: - %s", root->pointer);
					System::Diagnostics::Debug::WriteLine(varname);
#endif
					return(FALSE);
				}
			}
			root= root->next ;
		}
		return(FALSE);
	}

	static StringInfo **GetSwappedList(StringInfo *root, ULONG *plength);
	static StringInfo **GetUnswappedList(StringInfo *root, ULONG *plength);
private:
	static StringInfo **GetSubList(StringInfo *root, bool swapped, ULONG *plength);
};
typedef StringInfo *PSTRINGINFO;


#define MAX_STRING_LENGTH 1024

