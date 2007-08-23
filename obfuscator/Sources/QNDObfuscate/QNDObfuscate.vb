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

'The Original Code is File QNDObfuscate.vb.

'The Initial Developer of the Original Code is
'Desaware Inc.
'Portions created by the Initial Developer are Copyright (C) 2002
'the Initial Developer. All Rights Reserved.

'Contributor(s):    Daniel Appleman
'                   Eli Draluk

'***** END LICENSE BLOCK *****



Imports System.IO
Friend Module Module1
    Private Cancel As Boolean
    Private InputPath As String
    Private NoVSRestriction As Boolean

    Sub Main()
        Dim args As String() = System.Environment.GetCommandLineArgs()
        Dim i As Integer
        Dim iname As String = Nothing
        Dim oname As String = Nothing
        Console.WriteLine()
        Console.WriteLine("The QND-Obfuscator, Copyright (c) 2002-2005 by Desaware Inc.")
        Console.WriteLine("This software comes with ABSOLUTELY NO WARRANTY.")
        Console.WriteLine("This software is distributed under the terms of the Mozilla")
        Console.WriteLine("Public License 1.1. You may obtain a copy of the License ")
        Console.WriteLine("at http://www.mozilla.org/MPL/.")
        Console.WriteLine("Further information on this product may be found ")
        Console.WriteLine("at http://www.desaware.com/obfuscator.htm")
        Console.WriteLine()

        For i = 1 To UBound(args)
            Dim temp As String = args(i)
            Dim flag As Boolean = False
            If Left(temp, 1) = "/" Or Left(temp, 1) = "-" Then
                DoOptions(Mid(temp, 2))
                flag = True

            End If
            If i = 1 And flag <> True Then
                iname = DoPath(temp, True)
            End If
            If i = 2 And flag <> True Then
                oname = DoPath(temp, False)
            End If
            If Cancel Then Exit Sub
        Next

        If iname = Nothing Then
            Console.WriteLine("Invalid input file name")
            Exit Sub
        End If
        If oname = Nothing Then
            Console.WriteLine("Invalid output file name")
            Exit Sub
        End If

        ' V1.1 - Improved assembly resolution for dependent files
        Dim fi As New FileInfo(iname)
        InputPath = fi.DirectoryName
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ResolveAssemblyLoad

        DoTheObfuscate(iname, oname)
    End Sub

    Sub DoTheObfuscate(ByVal inputfile As String, ByVal outputfile As String)
        Dim oktomangle As ArrayList = Nothing
        Dim blockedinternal As Hashtable = Nothing
        Dim blockedexternal As ArrayList = Nothing
        Dim mangler As ObfuscatorLogic.Mangler
        Try
            mangler = New ObfuscatorLogic.Mangler(inputfile)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Exit Sub
        End Try
        If mangler Is Nothing Then
            Console.WriteLine("Unable to create obfuscator object")
            Exit Sub
        End If
        mangler.DeriveStringLists(oktomangle, blockedinternal, blockedexternal)
        mangler.RemovePrivateStrings(oktomangle, NoVSRestriction)
#If VS2005 = 1 Then ' Don't collapse strings on 2.0 framework
        mangler.CollapsePrivateString(True)
#Else
        mangler.CollapsePrivateString(False)
#End If
        Try
            mangler.SaveFile(outputfile)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub DoOptions(ByVal opt As String)
        If Char.ToLower(opt.Chars(0)) = "h"c Then
            Console.WriteLine("QNDObfuscate infile outfile [-help] [-novs]")
            Console.WriteLine("-novs = No Visual Studio 2005 Restriction. Use this option only for components that will not be referenced from Visual Studio 2005")
            Cancel = True
        End If
        If opt.ToLower().StartsWith("novs") Then
            NoVSRestriction = True
        End If
    End Sub

    Function DoPath(ByVal opt As String, ByVal MustExist As Boolean) As String
        Dim f2 As FileInfo
        Try
            f2 = New FileInfo(opt)
            If MustExist Then
                If Not f2.Exists Then Return Nothing
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
            Return Nothing
        End Try
        Return f2.FullName

    End Function

    ' V1.1 - Improved assembly resolution for dependent files
    Public Function ResolveAssemblyLoad(ByVal sender As Object, ByVal args As ResolveEventArgs) As System.Reflection.Assembly
        Debug.WriteLine(args.Name)
        Dim asm As Reflection.Assembly
        Dim asmname As String
        asmname = InputPath & "\" & args.Name.Substring(0, args.Name.IndexOf(","c)) & ".dll"
        ' If this raises an exception, just let it propogate upward
        asm = Reflection.Assembly.LoadFrom(asmname)
        Return asm
    End Function
End Module
