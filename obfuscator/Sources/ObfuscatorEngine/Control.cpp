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
//The Original Code is File ObfuscatorEngine.Control.cpp
//
//The Initial Developer of the Original Code is
//Desaware Inc.
//Portions created by the Initial Developer are Copyright (C) 2002-2005
//the Initial Developer. All Rights Reserved.
//
//Contributor(s):    
//                   
//
//***** END LICENSE BLOCK *****

#include "stdafx.h"

namespace Desaware {
	namespace ObfuscatorEngine
	{
		public __gc class Control
		{
		private:
			LPBYTE buffer;
			_IMAGE_DOS_HEADER* pDosHeader;
			_IMAGE_NT_HEADERS* pNTHeader;
			_IMAGE_SECTION_HEADER *pFirstSectionHeader;
			_IMAGE_DATA_DIRECTORY *pCLRDataDirectory;
			_IMAGE_SECTION_HEADER *pCLRSection;
			IMAGE_COR20_HEADER *pCLIHeader;
			METADATAROOT *pMetaDataRoot;
			STREAMHEADER **pStreams;
			LPSTR pStringStream;
			ULONG MetaDataSize;
			METADATATABLES *pMetaDataTables;
			StringInfo *StringsRoot;
			IMetaDataDispenserEx *pDispenser;
			IMetaDataImport *pImportMetadata;
			IMetaDataAssemblyImport *pAssemblyImport;
			ArrayList* UnresolvedNames;
			ULONG BufferLength;

			void AddToUnresolvedList(LPSTR ptr)
			{
				String *currentname;
				if(!ptr || ptr == pStringStream) return;	// NULL string
				currentname = Marshal::PtrToStringAnsi(System::IntPtr(ptr));
				if(UnresolvedNames->IndexOf(currentname)<0) {
					UnresolvedNames->Add(currentname);
				}
			}

			bool AnyStringOffsetMatches(ULONG offset)
			{
				StringInfo *sptr;

				sptr = StringsRoot;
				while(sptr) {
					if(sptr->offset == offset && sptr->Swapped) return(TRUE);
					sptr = sptr->next;
				}
				return(FALSE);
			}

			ULONG GetRowMember(ULONG pcbCol, ULONG poCol, ULONG rowptr) {
				DWORD *dwordptr;
				WORD *wordptr;
				if(pcbCol == 4) {
					dwordptr = (ULONG *)(rowptr + poCol);
					return(*dwordptr);
				}
				else {
					wordptr = (LPWORD)(rowptr + poCol);
					return((ULONG)*wordptr);
				}
			}

			void SetRowMember(ULONG pcbCol, ULONG poCol, ULONG rowptr, ULONG newvalue) {
				DWORD *dwordptr;
				WORD *wordptr;
				if(pcbCol == 4) {
					dwordptr = (ULONG *)(rowptr + poCol);
					*dwordptr = newvalue ;
				}
				else {
					wordptr = (LPWORD)(rowptr + poCol);
					*wordptr = (WORD)newvalue ;
					}
				}


			// Builds a list of names not to strip
			HRESULT SearchTablesForPublicNames()
			{
				IMetaDataTables *pTables;
				LPVOID ptr;
				HRESULT hr;
				ULONG bytesperrow, rows, column, key;
				LPCSTR tablename;
				LPSTR StringStart;
				ULONG StringOffset;
				ULONG row;
				ULONG col;


				// Uncomment these to look at column info
				//ULONG   poCol;
				//ULONG   pcbCol;
				//ULONG   pType;
				//LPCSTR   pName;

				hr = pImportMetadata->QueryInterface(IID_IMetaDataTables, (LPVOID *)&ptr);
				if(FAILED(hr)) return hr;
				pTables = (IMetaDataTables *)ptr;

				// Search TyepRef's first
				hr = pTables->GetTableInfo(1, &bytesperrow, &rows, &column, &key, &tablename);

				for(row=1; row<=rows; row++) {
					for(col = 1; col<=2; col++) {
						StringOffset = 0;
						hr = pTables->GetColumn(1, col, row, &StringOffset);
						StringStart = pStringStream + StringOffset;
						AddToUnresolvedList(StringStart);
					}
				}

				// Search AssemblyRef's
				hr = pTables->GetTableInfo(0x23, &bytesperrow, &rows, &column, &key, &tablename);
				//hr = pTables->GetColumnInfo(0x23, 7, &poCol, &pcbCol, &pType, &pName);

				for(row=1; row<=rows; row++) {
					for(col = 6; col<=7; col++) {
						StringOffset = 0;
						hr = pTables->GetColumn(0x23, col, row, &StringOffset);
						StringStart = pStringStream + StringOffset;
						AddToUnresolvedList(StringStart);
					}
				}

				// Search ExportedTypes's
				hr = pTables->GetTableInfo(0x27, &bytesperrow, &rows, &column, &key, &tablename);
				//hr = pTables->GetColumnInfo(0x27, 2, &poCol, &pcbCol, &pType, &pName);

				for(row=1; row<=rows; row++) {
					for(col = 2; col<=3; col++) {
						StringOffset = 0;
						hr = pTables->GetColumn(0x27, col, row, &StringOffset);
						StringStart = pStringStream + StringOffset;
						AddToUnresolvedList(StringStart);
					}
				}

				// Search Files
				hr = pTables->GetTableInfo(0x26, &bytesperrow, &rows, &column, &key, &tablename);
				//hr = pTables->GetColumnInfo(0x26, 1, &poCol, &pcbCol, &pType, &pName);

				for(row=1; row<=rows; row++) {
					StringOffset = 0;
					hr = pTables->GetColumn(0x26, 1, row, &StringOffset);
					StringStart = pStringStream + StringOffset;
					AddToUnresolvedList(StringStart);
				}

				// Search Manifest Resources
				hr = pTables->GetTableInfo(0x28, &bytesperrow, &rows, &column, &key, &tablename);
				//hr = pTables->GetColumnInfo(0x28, 2, &poCol, &pcbCol, &pType, &pName);

				for(row=1; row<=rows; row++) {
					StringOffset = 0;
					hr = pTables->GetColumn(0x28, 2, row, &StringOffset);
					StringStart = pStringStream + StringOffset;
					AddToUnresolvedList(StringStart);
				}


				// Search MemberRef Resources
				hr = pTables->GetTableInfo(0x0A, &bytesperrow, &rows, &column, &key, &tablename);
				//hr = pTables->GetColumnInfo(0x0A, 1, &poCol, &pcbCol, &pType, &pName);

				for(row=1; row<=rows; row++) {
					StringOffset = 0;
					hr = pTables->GetColumn(0x0A, 1, row, &StringOffset);
					StringStart = pStringStream + StringOffset;
					AddToUnresolvedList(StringStart);
				}

				// Search ModuleRef Resources
				hr = pTables->GetTableInfo(0x1A, &bytesperrow, &rows, &column, &key, &tablename);
				//hr = pTables->GetColumnInfo(0x1A, 0, &poCol, &pcbCol, &pType, &pName);

				for(row=1; row<=rows; row++) {
					StringOffset = 0;
					hr = pTables->GetColumn(0x1A, 0, row, &StringOffset);
					StringStart = pStringStream + StringOffset;
					AddToUnresolvedList(StringStart);
				}

				// Search ImplMap Resources
				hr = pTables->GetTableInfo(0x1C, &bytesperrow, &rows, &column, &key, &tablename);
				//hr = pTables->GetColumnInfo(0x1A, 0, &poCol, &pcbCol, &pType, &pName);

				for(row=1; row<=rows; row++) {
					StringOffset = 0;
					hr = pTables->GetColumn(0x1C, 2, row, &StringOffset);
					StringStart = pStringStream + StringOffset;
					AddToUnresolvedList(StringStart);
				}

				pTables->Release();
				return(hr);
			}

			// Add any additional unresolved methods
			HRESULT BuildUnresolvedList()
			{
				HRESULT hr = S_OK;

				HCORENUM hcorenum = 0;
				mdToken  token = 0;
				DWORD Methods;
				WCHAR methodname[MAX_STRING_LENGTH+1];

				String *currentname;

				while ( (hr = pImportMetadata->EnumUnresolvedMethods(&hcorenum, &token, 1, &Methods)) != E_FAIL && Methods==1 )
				{
					hr = pImportMetadata->GetMemberProps(token, NULL, methodname, MAX_STRING_LENGTH, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
					currentname = Marshal::PtrToStringUni(System::IntPtr(methodname));
					if(UnresolvedNames->IndexOf(currentname)<0) UnresolvedNames->Add(currentname);
				}
				pImportMetadata->CloseEnum(hcorenum);
				return(hr);
			} 


			// Get the list of external references
			HRESULT BuildOtherTables()
			{
				HRESULT hr = S_OK;
				LPWSTR scope=NULL;
				LPVOID ptr;

				hr = CoCreateInstance(CLSID_CorMetaDataDispenser, NULL, CLSCTX_INPROC_SERVER, 
					IID_IMetaDataDispenserEx, (LPVOID *)&ptr);
				if(FAILED(hr)) goto badtableexit;
				pDispenser = (IMetaDataDispenserEx * )ptr;

				hr = pDispenser->OpenScopeOnMemory(pMetaDataRoot, MetaDataSize , 0, IID_IMetaDataImport, (IUnknown **)&ptr);
				if(FAILED(hr)) goto badtableexit;
				pImportMetadata = (IMetaDataImport *)ptr;

				hr = pImportMetadata->QueryInterface(IID_IMetaDataAssemblyImport, (LPVOID*) &ptr);
				if(FAILED(hr)) goto badtableexit;
				pAssemblyImport = (IMetaDataAssemblyImport *)ptr;

				UnresolvedNames = new ArrayList();
				hr = SearchTablesForPublicNames();
				if(FAILED(hr)) goto badtableexit;
				hr = BuildUnresolvedList();
				if(FAILED(hr)) goto badtableexit;

badtableexit:
				if(pAssemblyImport) pAssemblyImport->Release();
				if(pImportMetadata) pImportMetadata->Release();
				if(pDispenser) pDispenser->Release();
				if(scope) delete scope;
				pAssemblyImport = NULL;
				pImportMetadata = NULL;
				pDispenser = NULL;
				return(hr);
			}


			void PreProcess()
			{
				short index;
				METADATAROOT2 *mr2;
				LPSTR thisstring;
				DWORD stringheapsize;
				StringInfo *currentstringheader;
				ULONG tval;

				// There's lots of room for improved error checking here, but assuming 
				// this is only called for executables that are valid .NET images, this
				// code should all work.
				pDosHeader = (_IMAGE_DOS_HEADER *)buffer;
				pNTHeader = (_IMAGE_NT_HEADERS *)(buffer + pDosHeader->e_lfanew);
				pFirstSectionHeader = IMAGE_FIRST_SECTION( pNTHeader );
				pCLRDataDirectory = &pNTHeader->OptionalHeader.DataDirectory[14];

				pCLRSection = FindSectionForVirtualAddress(pCLRDataDirectory->VirtualAddress, pFirstSectionHeader, pNTHeader->FileHeader.NumberOfSections);
				pCLIHeader = (IMAGE_COR20_HEADER *)(buffer + pCLRSection->PointerToRawData + pCLRDataDirectory->VirtualAddress - pCLRSection->VirtualAddress);
				pMetaDataRoot = (METADATAROOT *)FindPhysicalForVirtualAddress(pCLIHeader->MetaData.VirtualAddress, pFirstSectionHeader, pNTHeader->FileHeader.NumberOfSections, buffer, &tval);
				MetaDataSize = tval;
				mr2 = pMetaDataRoot->GetNextPart();
				pStreams = new PSTREAMHEADER[mr2->Streams] ;
				pStreams[0] = (STREAMHEADER *)(mr2+1);
				for(index = 1; index< mr2->Streams; index++) {
					pStreams[index] = pStreams[index-1]->GetNextStream();
				}
				// Find the string and tables manifest streams
				for(index=0; index<mr2->Streams; index++) {
					LPSTR pstreamname = (LPSTR)(pStreams[index]+1);
					if(lstrcmpi(pstreamname,"#Strings")==0) {
						pStringStream = (LPSTR)((LPBYTE)pMetaDataRoot + pStreams[index]->Offset);
						stringheapsize = pStreams[index]->Size;
					}
					if(lstrcmpi(pstreamname,"#~")==0) pMetaDataTables = (METADATATABLES *)((LPBYTE)pMetaDataRoot + pStreams[index]->Offset);
				}
				// Build a single linked list of StringInfo objects from the #Strings heap
				thisstring = pStringStream;
				currentstringheader = new StringInfo();
				StringsRoot = currentstringheader;
				currentstringheader->pointer  = thisstring;
				do {
					thisstring = thisstring+lstrlen(thisstring)+1;
					if(thisstring >= (pStringStream + stringheapsize)) break;
					currentstringheader->next = new StringInfo();
					currentstringheader = currentstringheader->next;
					currentstringheader->pointer = thisstring;
					currentstringheader->offset = (ULONG)(thisstring - pStringStream);
				} while(TRUE);
			}


			// Strips parameters from obfuscated methods
			void StripParameters(IMetaDataTables *pTables, ULONG MangledOffset)
			{
				HRESULT hr;
				ULONG bytesperrow, rows, column, key;
				ULONG   poCol;
				ULONG   pcbCol;
				ULONG   pType;
				LPCSTR   pName;
				LPCSTR tablename;
				ULONG row;
				HCORENUM hcorenum = 0;

				ULONG currentvalue;
				ULONG rowptr;
				ULONG rowtoken;
				ULONG paramcount;
				mdToken ParamTokens[32];
				ULONG paramrow;
				ULONG paramrowptr;
				ULONG poColp, pcbColp, pTypep;
				LPCSTR pNamep;

				// Method table
				hr = pTables->GetTableInfo(0x6, &bytesperrow, &rows, &column, &key, &tablename);
				hr = pTables->GetColumnInfo(0x6, 3, &poCol, &pcbCol, &pType, &pName);
				// Get the name column info for the parameter table
				hr = pTables->GetColumnInfo(0x8, 2, &poColp, &pcbColp, &pTypep, &pNamep);

				try {

					for(row=1; row<=rows; row++) {
						System::Diagnostics::Debug::WriteLine(Convert::ToString((int)row));
						hr = pTables->GetRow(6, row, (LPVOID *)&rowptr);
						currentvalue = GetRowMember(pcbCol, poCol, rowptr);
						if(StringInfo::OffsetIsShadowed(StringsRoot, currentvalue)) {
							rowtoken = 6<<24 | row;
							paramcount = 0;
							hcorenum = 0;
							hr = pImportMetadata->EnumParams(&hcorenum, rowtoken, ParamTokens, 32, &paramcount);
							System::Diagnostics::Debug::WriteLine(__box(paramcount));
							if(paramcount>0) {
								for(paramrow = 0; paramrow<paramcount; paramrow++) {
									hr = pTables->GetRow(ParamTokens[paramrow]>>24, ParamTokens[paramrow] & 0x0FFFFFF, (LPVOID *)&paramrowptr);
									SetRowMember(pcbColp, poColp, paramrowptr, MangledOffset);
								}



							}
						}
					}

					// Debugging code
					// else {
					//	paramcount = 0;
					//	rowtoken = 6<<24 | row;
					//	hcorenum = 0;
					//	hr = pImportMetadata->EnumParams(&hcorenum, rowtoken, ParamTokens, 32, &paramcount);
					//	System::Diagnostics::Debug::WriteLine(__box(paramcount));
					//}


				}
				catch (System::Exception* ex)
				{
					ex = ex;
				}

			}



		public:
			Control(String* filename)
			{
				IO::FileStream* fs;
				DWORD bytesread;
				buffer = NULL;
				pDispenser = NULL;
				pImportMetadata = NULL;
				pAssemblyImport = NULL;
				MetaDataSize = 0;

				// This will throw exception on failure - caller can catch it.
				fs = new IO::FileStream(filename, IO::FileMode::Open);
				buffer = new BYTE[(size_t)fs->Length];
				ReadFile((HANDLE)fs->Handle, buffer, (DWORD)fs->Length, &bytesread, NULL);
				BufferLength = bytesread;
				PreProcess();
				fs->Close();
				CoInitialize(0);    
				BuildOtherTables();
			}

			void SaveFile(String *filename)
			{
				IO::FileStream* fs;
				DWORD byteswritten;

				// This will throw exception on failure - caller can catch it.
				fs = new IO::FileStream(filename, IO::FileMode::OpenOrCreate );
				WriteFile((HANDLE)fs->Handle, buffer, BufferLength, &byteswritten, NULL);
				fs->Close();
			}

			// Covert #Strings heap entries into an ArrayList of String objects
			ArrayList *GetStringTable()
			{
				StringInfo *sptr;
				String *newstring;
				ArrayList *result = new ArrayList();
				sptr = StringsRoot;
				while(sptr) {
					newstring = Marshal::PtrToStringAnsi(sptr->pointer);
					result->Add(newstring);
					sptr = sptr->next;
				}
				return(result);
			}

			// Replace a string in the #Strings heap with the specified string
			bool ReplaceString(System::String *oldstring, System::String *newstring)
			{
				StringInfo *sptr = StringsRoot;
				LPSTR oldstringptr = NULL;
				LPSTR newstringptr = NULL;
				short counter;
				bool result = FALSE;
				if(!oldstring || !newstring) return(FALSE);
				oldstringptr = (LPSTR)Marshal::StringToHGlobalAnsi(oldstring).ToPointer();
				while(sptr) {
					if(lstrcmp(sptr->pointer , oldstringptr)==0) {
						sptr->Swapped = TRUE;
						LPSTR copyptr = sptr->pointer ;
						LPSTR newstringglobal = (LPSTR)Marshal::StringToHGlobalAnsi(newstring).ToPointer();
						LPSTR newstringptr = newstringglobal;
						int oldstringlength = lstrlen(sptr->pointer);
						// NULL out remaining characters in the string
						for(counter = 0; counter< oldstringlength; counter++) {
							*copyptr++ = (*newstringptr)? *newstringptr++ : '\0';
						}
						GlobalFree(newstringglobal);
						result = TRUE;
						break;
					}
					sptr = sptr->next; 
				}
				GlobalFree(oldstringptr);
				return(result);
			}

			~Control()
			{
				if(buffer) delete buffer;
				CoUninitialize();
			}

			ArrayList *GetUnresolvedStrings()
			{
				return(UnresolvedNames);
			}

			// Tables with mangled names have their name fields swapped 
			HRESULT StripTablesOfMangledNames(bool ParametersOnly)
			{
				StringInfo **MangledStrings;
				ULONG MangledStringCount;

				IMetaDataTables *pTables;
				LPVOID ptr;
				HRESULT hr;
				ULONG bytesperrow, rows, column, key;
				ULONG   poCol;
				ULONG   pcbCol;
				ULONG   pType;
				LPCSTR   pName;
				LPCSTR tablename;
				ULONG visibilityattribute;
				ULONG row;
				const void * pBlob;
				ULONG BlobLength;

				ULONG currentvalue;
				ULONG rowptr;
				ULONG rowtoken;

				// Get list of mangled and unmanagled names
				MangledStrings = StringInfo::GetSwappedList(StringsRoot, &MangledStringCount);

				hr = CoCreateInstance(CLSID_CorMetaDataDispenser, NULL, CLSCTX_INPROC_SERVER, 
					IID_IMetaDataDispenserEx, (LPVOID *)&ptr);
				if(FAILED(hr)) goto badmangledexit;
				pDispenser = (IMetaDataDispenserEx * )ptr;
				hr = pDispenser->OpenScopeOnMemory(pMetaDataRoot, MetaDataSize , 0, IID_IMetaDataImport, (IUnknown **)&ptr);
				if(FAILED(hr)) goto badmangledexit;
				pImportMetadata = (IMetaDataImport *)ptr;

				hr = pImportMetadata->QueryInterface(IID_IMetaDataTables, (LPVOID *)&ptr);
				if(FAILED(hr)) return hr;
				pTables = (IMetaDataTables *)ptr;

				if(!ParametersOnly) {
					// Typedef table
					hr = pTables->GetTableInfo(0x2, &bytesperrow, &rows, &column, &key, &tablename);
					hr = pTables->GetColumnInfo(0x2, 1, &poCol, &pcbCol, &pType, &pName);

					for(row=1; row<=rows; row++) {
						hr = pTables->GetRow(2, row, (LPVOID *)&rowptr);
						currentvalue = GetRowMember(pcbCol, poCol, rowptr);
						if(AnyStringOffsetMatches(currentvalue)) {
							SetRowMember(pcbCol, poCol, rowptr, MangledStrings[0]->offset);
						}
						else {
							visibilityattribute = GetRowMember(4, 0, rowptr);
							rowtoken = 2<<24 | row;
							switch(visibilityattribute) {
									case 0:	// Not public
									case 3:	// Nested private
									case 5:	// Nested assembly
									case 6:	// Nested family & assembly
										hr = pImportMetadata->GetCustomAttributeByName(rowtoken, L"ObfuscateAttribute", &pBlob, &BlobLength);
										if(hr==0) {	// If obfuscation is requested
											SetRowMember(pcbCol, poCol, rowptr, MangledStrings[0]->offset);
										}
										break;
									default:
										break;
							}

						}


						// Field table
						hr = pTables->GetTableInfo(0x4, &bytesperrow, &rows, &column, &key, &tablename);
						hr = pTables->GetColumnInfo(0x4, 1, &poCol, &pcbCol, &pType, &pName);

						for(row=1; row<=rows; row++) {
							hr = pTables->GetRow(4, row, (LPVOID *)&rowptr);
							currentvalue = GetRowMember(pcbCol, poCol, rowptr);
							if(AnyStringOffsetMatches(currentvalue)) {
								SetRowMember(pcbCol, poCol, rowptr, MangledStrings[0]->offset);
							}
							else {
								visibilityattribute = GetRowMember(2, 0, rowptr);
								rowtoken = 4<<24 | row;
								switch(visibilityattribute) {
									case 1:	// Private
									case 2:	// Fam & Assembly
									case 3:	// Assembly
										hr = pImportMetadata->GetCustomAttributeByName(rowtoken, L"ObfuscateAttribute", &pBlob, &BlobLength);
										if(hr==0) {	// If obfuscation is requested
											SetRowMember(pcbCol, poCol, rowptr, MangledStrings[0]->offset);
										}
										break;
									default:
										break;
								}
							}

						}

						// Method table
						hr = pTables->GetTableInfo(0x6, &bytesperrow, &rows, &column, &key, &tablename);
						hr = pTables->GetColumnInfo(0x6, 3, &poCol, &pcbCol, &pType, &pName);

						for(row=1; row<=rows; row++) {
							hr = pTables->GetRow(6, row, (LPVOID *)&rowptr);
							currentvalue = GetRowMember(pcbCol, poCol, rowptr);
							if(AnyStringOffsetMatches(currentvalue)) {
								SetRowMember(pcbCol, poCol, rowptr, MangledStrings[0]->offset);
							}
							else {
								visibilityattribute = GetRowMember(2, 6, rowptr);
								rowtoken = 6<<24 | row;
								switch(visibilityattribute) {
									case 1:	// Private
									case 2:	// Fam & Assembly
									case 3:	// Assembly
										hr = pImportMetadata->GetCustomAttributeByName(rowtoken, L"ObfuscateAttribute", &pBlob, &BlobLength);
										if(hr==0) {	// If obfuscation is requested
											SetRowMember(pcbCol, poCol, rowptr, MangledStrings[0]->offset);
										}
										break;
									default:
										break;
								}
							}

						}

					}
				}
				StripParameters(pTables, MangledStrings[0]->offset);
				pTables->Release();
badmangledexit:
				delete MangledStrings;
				if(pImportMetadata) pImportMetadata->Release();
				if(pDispenser) pDispenser->Release();
				pImportMetadata = NULL;
				pDispenser = NULL;
				return(hr);

			}

		};
	}

};

