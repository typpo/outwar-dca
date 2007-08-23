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

'The Original Code is File ObfuscatorLogic.clsObfLogic.vb.

'The Initial Developer of the Original Code is
'Desaware Inc.
'Portions created by the Initial Developer are Copyright (C) 2002
'the Initial Developer. All Rights Reserved.

'Contributor(s):    
'                   

'***** END LICENSE BLOCK *****

Imports Desaware
Imports System.Reflection



Friend Class ReferencedString
    Public StringValue As String
    Public ReferencedBy As ArrayList

    Public Shared Sub AddString(ByVal AllStrings As Hashtable, ByVal refobject As ObjectTree)
        Dim r As ReferencedString
        Dim obj As Object
        obj = AllStrings.Item(refobject.Name)
        If obj Is Nothing Then
            AllStrings.Add(refobject.Name, New ReferencedString(refobject.Name, refobject))
        Else
            r = CType(obj, ReferencedString)
            ' No need to keep two copies
            If r.ReferencedBy.IndexOf(refobject) < 0 Then
                r.ReferencedBy.Add(refobject)
            End If
        End If
    End Sub

    Public Shared Function GetArrayListForString(ByVal AllStrings As Hashtable, ByVal s As String) As ArrayList
        Dim r As ReferencedString
        r = CType(AllStrings(s), ReferencedString)
        If r Is Nothing Then Return Nothing
        Return r.ReferencedBy
    End Function

    Public Sub New(ByVal s As String, ByVal refobject As ObjectTree)
        StringValue = s
        ReferencedBy = New ArrayList()
        ReferencedBy.Add(refobject)
    End Sub

End Class

Public Class ObjectTree
    Public ThisNode As Object
    Public Name As String
    Public IsPublic As Boolean
    Public ChildNodes As ArrayList

    Public Sub New(ByVal node As Object, ByVal nodeName As String, ByVal IsPublic As Boolean)
        ThisNode = node
        Name = nodeName
        MyClass.IsPublic = IsPublic
        ChildNodes = New ArrayList()
    End Sub
End Class

Public Class Mangler
    Private m_Control As ObfuscatorEngine.Control
    Private m_Assembly As [Assembly]
    Private RootTree As ObjectTree
    Private AllStrings As New Hashtable()
    Private m_PEbuffer() As Byte    ' Added 3/29/02

    Public ReadOnly Property Root() As ObjectTree
        Get
            Return RootTree
        End Get
    End Property

    Public Sub New(ByVal FileName As String)
        m_Control = New ObfuscatorEngine.Control(FileName)
        ' Modification to prevent lock on file - DSA 3/29/02
        Dim pefile As New IO.FileStream(FileName, IO.FileMode.Open)
        Dim pestream As New IO.BinaryReader(pefile)
        m_PEbuffer = pestream.ReadBytes(CInt(pefile.Length + 1))
        pestream.Close()
        pefile.Close()
        m_Assembly = [Assembly].Load(m_PEbuffer)
        ' Modification end - DSA 3/29/02
        'm_Assembly = [Assembly].LoadFrom(FileName) ' Removed 3/29/02
        ParseManifest(m_Assembly)
    End Sub

    Public Sub SaveFile(ByVal FileName As String)
        m_Control.SaveFile(FileName)
    End Sub

    Private Sub RecordInfo(ByVal CurrentNode As ObjectTree, ByVal newnode As ObjectTree)
        If CurrentNode Is Nothing Then
            RootTree = newnode
        Else
            CurrentNode.ChildNodes.Add(newnode)
        End If
        ReferencedString.AddString(AllStrings, newnode)
    End Sub

    Private Const BindingSearch As BindingFlags = BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.DeclaredOnly

    ' VSRestrict = True to limit chars to those that won't crash Visual Studio
    Public Sub RemovePrivateStrings(ByVal OkToMangle As ArrayList, ByVal NoVSRestrict As Boolean)
        If charvals Is Nothing Then
            Dim sb As New Text.StringBuilder
            If NoVSRestrict Then
                sb.Append("$")
                sb.Append(";")
                For x As Integer = 128 To 254
                    sb.Append(Chr(x))
                Next
            Else
                sb.Append("$")
                sb.Append(";")
                For x As Integer = 48 To 127
                    If Chr(x) <> ";"c Then sb.Append(Chr(x))
                    'If Char.IsLetterOrDigit(Chr(x)) Then sb.Append(Chr(x))
                Next
            End If
            charvals = sb.ToString
        End If

        Dim s As String
        Dim x1 As Integer = charvals.Length
        Dim x2 As Integer = 1
        For Each s In OkToMangle
            If s.Length = 1 Then
                m_Control.ReplaceString(s, GetSwapString(x2))
                x2 += 1
            Else
                m_Control.ReplaceString(s, GetSwapString(x1))
                x1 += 1
            End If
        Next
    End Sub

    Private Shared charvals As String
    Private Function GetSwapString(ByVal Index As Integer) As String
        Dim firstbyte As Integer = Index \ charvals.Length
        Dim secondbyte As Integer = Index Mod charvals.Length
        If firstbyte = 0 Then
            Return charvals(firstbyte)
        End If
        Return charvals(firstbyte - 1) & charvals(secondbyte)
    End Function

    Public Sub CollapsePrivateString(ByVal parametersonly As Boolean)
        m_Control.StripTablesOfMangledNames(parametersonly)
    End Sub

    Public Sub ReplaceString(ByVal OldString As String, ByVal NewString As String)
        m_Control.ReplaceString(OldString, NewString)
    End Sub

    Public Function GetStringList() As ArrayList
        Return m_Control.GetStringTable
    End Function

    Private Function TypeIsVisible(ByVal t As Type) As Boolean
        Do While Not t Is Nothing
            If t.DeclaringType Is Nothing Then
                ' It's top level
                Return t.IsPublic
            End If
            If t.IsNestedPrivate OrElse t.IsNestedAssembly OrElse t.IsNestedFamANDAssem Then Return False
            t = t.DeclaringType
        Loop
    End Function

    Private Function MethodIsVisible(ByVal m As MethodInfo) As Boolean
        Return m.IsPublic Or m.IsFamily Or m.IsFamilyOrAssembly
    End Function

    Private Function FindProblemNames() As ArrayList
        Dim rs As ReferencedString
        Dim objtree As ObjectTree
        Dim fi As FieldInfo
        Dim blockit As Boolean
        Dim resultlist As New ArrayList()

        ' If the declaring type of a delegate is visible, the delegate field must be visible
        ' this is to make events work
        For Each rs In AllStrings.Values
            blockit = False
            For Each objtree In rs.ReferencedBy
                If TypeOf objtree.ThisNode Is FieldInfo Then
                    fi = CType(objtree.ThisNode, FieldInfo)
                    Dim ft As Type = fi.FieldType
                    If ft.IsSubclassOf(GetType(System.Delegate)) Then
                        If TypeIsVisible(ft.DeclaringType) Then blockit = True
                    End If
                End If
            Next
            If blockit Then resultlist.Add(rs.StringValue)
        Next

        ' Look for ObfuscateBlock attributes at the Assembly level
        Dim assem As [Assembly]
        Dim attrs() As Object
        Dim attr As Object
        Dim objectresult As Object
        assem = CType(RootTree.ThisNode, [Assembly])
        attrs = assem.GetCustomAttributes(False)
        For Each attr In attrs
            If attr.GetType.Name = "ObfuscateBlockAttribute" Then
                ' We can't early bind because the attribute can be defined anywhere
                objectresult = attr.GetType.InvokeMember("BlockString", BindingFlags.GetField Or BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public, Nothing, attr, Nothing)
                If Not objectresult Is Nothing Then
                    resultlist.Add(CType(objectresult, String))
                End If
            End If
        Next

        resultlist.Add("BeginInvoke")
        resultlist.Add("EndInvoke")
        resultlist.Add("Invoke")
        ' Build 0.1.0.1 - Added Attribute suffix here to prevent obfuscation of these terms
        resultlist.Add("ObfuscateAttribute")
        resultlist.Add("DoNotObfuscateAttribute")
        resultlist.Add("ObfuscateBlockAttribute")
        resultlist.Add(".ctor")
        resultlist.Add(".cctor")
        Return resultlist
    End Function

    Public Sub DeriveStringLists(ByRef OkToMangle As ArrayList, ByRef BlockedByLocalConflict As Hashtable, ByRef BlockedByObjEngine As ArrayList)
        Dim s As String
        Dim objtree As ObjectTree
        Dim ispublic As Boolean
        Dim ObjEngineList As ArrayList
        Dim problemnames As ArrayList
        OkToMangle = New ArrayList()
        BlockedByLocalConflict = New Hashtable()
        BlockedByObjEngine = New ArrayList()
        ObjEngineList = m_Control.GetUnresolvedStrings()
        problemnames = FindProblemNames()
        For Each s In AllStrings.Keys
            ispublic = False
            For Each objtree In ReferencedString.GetArrayListForString(AllStrings, s)
                If objtree.IsPublic AndAlso Not BlockedByLocalConflict.ContainsKey(s) Then
                    BlockedByLocalConflict.Add(s, objtree)
                    ispublic = True
                End If
            Next
            If Not ispublic AndAlso problemnames.Contains(s) Then
                If Not BlockedByLocalConflict.ContainsKey(s) Then BlockedByLocalConflict.Add(s, Nothing)
                ispublic = True
            End If
            If Not ispublic AndAlso ObjEngineList.Contains(s) Then
                BlockedByObjEngine.Add(s)
                ispublic = True
            End If
            If Not ispublic Then OkToMangle.Add(s)
        Next
    End Sub


    Private Function IsBlocked(ByVal m As MemberInfo) As Boolean
        Dim attrs() As Object
        Dim attr As Object
        Try
            attrs = m.GetCustomAttributes(True)
            For Each attr In attrs
                If attr.GetType.Name = "DoNotObfuscateAttribute" Then Return (True)
            Next
        Catch
        End Try
        Return (False)
    End Function

    Private Function IsBlocked(ByVal p As ParameterInfo) As Boolean
        Dim attrs() As Object
        Dim attr As Object
        Try
            attrs = p.GetCustomAttributes(True)
            For Each attr In attrs
                If attr.GetType.Name = "DoNotObfuscateAttribute" Then Return (True)
            Next
        Catch
        End Try
        Return (False)
    End Function


    Private Sub ParseRecursive(ByVal CurrentNode As ObjectTree, ByVal t As Type)
        Dim newnode As ObjectTree
        Dim subnode As ObjectTree
        Dim Storeit As Boolean
        Dim MemberName As String = Nothing
        Dim MemberIsPublic As Boolean
        Dim ClassIsPublic As Boolean
        Dim ParamIsPublic As Boolean
        Dim BlockObfuscation As Boolean
        Dim BlockMemberObfuscation As Boolean    ' 1.0.0.0 - New

        If t Is Nothing Then Exit Sub

        ClassIsPublic = TypeIsVisible(t)
        If IsBlocked(t) Then
            ClassIsPublic = True
            BlockObfuscation = True
        End If

        newnode = New ObjectTree(t, t.Name, ClassIsPublic Or IsStateCoderState(t) Or IsMyClass(t))
        RecordInfo(CurrentNode, newnode)

        ' Add parsing methods, etc. here.
        Dim Members As MemberInfo()
        Dim AMember As MemberInfo
        Members = t.GetMembers(BindingSearch)
        If Not Members Is Nothing Then
            For Each AMember In Members
                BlockMemberObfuscation = False
                If IsMyClass(t) Then BlockMemberObfuscation = True ' All compiler generated members of My aren't obfuscated
                MemberIsPublic = False
                Storeit = True
                Select Case AMember.MemberType
                    Case MemberTypes.Constructor
                        MemberName = AMember.Name
                        MemberIsPublic = True
                    Case MemberTypes.Event
                        Dim ev As EventInfo
                        ev = CType(AMember, EventInfo)
                        If TypeIsVisible(ev.EventHandlerType) Then MemberIsPublic = True
                        MemberName = ev.Name
                    Case MemberTypes.Field
                        Dim fi As FieldInfo
                        fi = CType(AMember, FieldInfo)
                        'MemberIsPublic = fi.IsPublic Or fi.IsFamily Or fi.IsFamilyOrAssembly
                        ' Fixed 4/1/02 to correctly detect additional cases which may not be obfuscated
                        MemberIsPublic = fi.IsPublic Or fi.IsFamily Or fi.IsFamilyOrAssembly
                        ' Fixed 1.0.0.0 - BlockObfuscation needs to be true instead of Member is Public
                        ' Was If IsBlocked(fi) Then MemberIsPublic = True
                        If IsBlocked(fi) Then BlockMemberObfuscation = True
                        MemberName = fi.Name
                    Case MemberTypes.Method
                        Dim mt As MethodInfo
                        mt = CType(AMember, MethodInfo)
                        MemberIsPublic = MethodIsVisible(mt)
                        If IsBlocked(mt) Then BlockMemberObfuscation = True ' 1.0.0.0
                        MemberName = mt.Name
                    Case MemberTypes.NestedType
                        Dim nt As Type
                        nt = CType(AMember, Type)
                        MemberIsPublic = TypeIsVisible(nt)
                        ' Fixed 4/2/03 - BlockObfuscation needs to be true instead of Member is Public
                        ' Was If IsBlocked(nt) Then MemberIsPublic = True
                        If IsBlocked(nt) Then BlockMemberObfuscation = True
                        MemberName = nt.Name
                    Case MemberTypes.Property
                        Dim pt As PropertyInfo
                        pt = CType(AMember, PropertyInfo)
                        MemberName = pt.Name
                        If pt.GetAccessors(False).Length > 0 Then
                            ' if any public accessors exist, it's a public property
                            MemberIsPublic = True
                        End If
                    Case MemberTypes.TypeInfo
                        Dim ti As Type
                        ti = CType(AMember, Type)
                        MemberIsPublic = TypeIsVisible(ti)
                        MemberName = ti.Name
                    Case MemberTypes.Custom
                        ' TBD

                End Select
                If Storeit Then
                    ' Members in non-public classes won't be considered public
                    If Not ClassIsPublic Then
                        MemberIsPublic = False
                    End If
                    If BlockObfuscation OrElse BlockMemberObfuscation Then
                        MemberIsPublic = True
                    End If
                    subnode = New ObjectTree(AMember, MemberName, MemberIsPublic)
                    ' Record all parameters so we don't obfuscate public ones by accident
                    Select Case AMember.MemberType
                        Case MemberTypes.Method, MemberTypes.Constructor
                            Dim meminfo As MethodBase
                            Dim paramlist() As ParameterInfo
                            Dim param As ParameterInfo
                            meminfo = CType(AMember, MethodBase)
                            paramlist = meminfo.GetParameters()
                            For Each param In paramlist
                                If MemberIsPublic Then
                                    ParamIsPublic = True
                                Else
                                    ParamIsPublic = IsBlocked(param)
                                End If
                                RecordInfo(subnode, New ObjectTree(param, param.Name, ParamIsPublic))
                            Next
                    End Select
                    Select Case AMember.MemberType
                        Case MemberTypes.NestedType
                            ' nested type will be recorded on call
                            ParseRecursive(newnode, CType(AMember, Type))
                        Case Else
                            RecordInfo(newnode, subnode)
                    End Select
                End If
            Next
        End If
    End Sub

    ' Don't obfuscate classes that derive from State
    Private Function IsStateCoderState(ByVal T As Type) As Boolean
        Do While Not T Is Nothing
            If T.FullName = "Desaware.StateCoder.State" Then
                Return True
            End If
            T = T.BaseType
        Loop
    End Function

    Private Function IsMyClass(ByVal T As Type) As Boolean
        Do While Not T Is Nothing
            If T.FullName.Contains(".My.") Then
                Return True
            End If
            T = T.BaseType
        Loop
    End Function

    Private Sub ParseManifest(ByVal a As [Assembly])
        RootTree = New ObjectTree(a, a.GetName.Name, True)
        ReferencedString.AddString(AllStrings, RootTree)

        Dim types() As Type
        types = a.GetTypes()

        ' Only check top level types
        Dim t As Type
        For Each t In types
            If t.DeclaringType Is Nothing Then
                ParseRecursive(RootTree, t)
            End If
        Next

    End Sub


End Class
