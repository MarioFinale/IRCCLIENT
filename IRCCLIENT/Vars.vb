Option Strict On
Option Explicit On
Imports Utils.Utils
Module Vars

    Public Log_Filepath As String = Exepath & "Log.psv"
    Public UserPath As String = Exepath & "Users.psv"
    Public EventLogger As New LogEngine.LogEngine(Log_Filepath, UserPath, Codename)
    Public Uptime As Date = Date.Now

End Module
