Option Strict On
Option Explicit On
Imports MWBot.net.WikiBot
Imports MWBot.net
Imports Utils.Utils
Namespace IRC
    Public Class IRCCommandParams

#Region "Properties"
        Public ReadOnly Property Source As String
        Public ReadOnly Property Realname As String
        Public ReadOnly Property CParam As String
        Public ReadOnly Property Client As IRC_Client
        Public ReadOnly Property Imputline As String
        Public ReadOnly Property IsOp As Boolean
        Public ReadOnly Property Workerbot As Bot
        Public ReadOnly Property CommandName As String
        Public ReadOnly Property MessageLine As String
        Public ReadOnly Property TaskAdm As TaskAdmin

#End Region

        Sub New(ByVal cimputline As String, ByRef commandResolver As IRC_Client, ByRef rWorkerbot As Bot, ByRef taskadmin As TaskAdmin)
            If commandResolver Is Nothing Then Throw New ArgumentNullException(Reflection.MethodBase.GetCurrentMethod().Name)
            If rWorkerbot Is Nothing Then Throw New ArgumentNullException(Reflection.MethodBase.GetCurrentMethod().Name)

            _Source = String.Empty
            _Realname = String.Empty
            _CParam = String.Empty
            _CommandName = String.Empty
            _MessageLine = String.Empty

            _Client = commandResolver
            _Imputline = cimputline
            _Workerbot = rWorkerbot
            _TaskAdm = taskadmin

            Dim sCommandParts As String() = Imputline.Split(CType(" ", Char()))
            If sCommandParts.Length < 4 Then Exit Sub
            Dim sPrefix As String = sCommandParts(0)
            'Dim sCommand As String = sCommandParts(1) //Useless
            Dim sSource As String = sCommandParts(2)
            Dim sParam As String = GetParamString(Imputline)

            Dim sRealname As String = GetUserFromChatresponse(sPrefix)
            If sSource.ToLower = _Client.NickName.ToLower Then
                sSource = sRealname
            End If

            Dim sCommandText As String = String.Empty
            For i As Integer = 3 To sCommandParts.Length - 1
                sCommandText = sCommandText & " " & sCommandParts(i)
            Next
            Dim sParams As String() = GetParams(sParam)
            Dim MainParam As String = sParams(0).ToLower
            Dim commandParam As String = String.Empty

            If Not MainParam = String.Empty Then
                Dim usrarr As String() = sParam.Split(CType(" ", Char()))
                For i As Integer = 0 To usrarr.Count - 1
                    If i = 0 Then
                    Else
                        commandParam = commandParam & " " & usrarr(i)
                    End If
                Next
                commandParam = commandParam.Trim(CType(" ", Char()))
            End If
            _Source = sSource
            _Realname = sRealname
            _CParam = commandParam
            _CommandName = MainParam
            _MessageLine = sParam
        End Sub

        Private Function GetParams(ByVal param As String) As String()
            Return param.Split(CType(" ", Char()))
        End Function

        Private Function GetParamString(ByVal message As String) As String
            If message.Contains(":") Then
                Dim matchedstrings As String() = TextInBetweenInclusive(message, ":", " :")
                If matchedstrings.Count = 0 Then Return String.Empty
                Dim stringtoremove As String = matchedstrings(0)
                Dim paramstring As String = message.Replace(StringToRemove, String.Empty)
                Return paramstring
            Else
                Return String.Empty
            End If
        End Function

    End Class
End Namespace