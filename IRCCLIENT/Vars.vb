Option Strict On
Option Explicit On
Imports MWBot.net.Utility
Imports MWBot.net.Utility.Utils
Module Vars

    Public Log_Filepath As String = Exepath & "IRCLog.psv"
    Public UserPath As String = Exepath & "Users.psv"
    Public Verbose As Boolean = True
    Public EventLogger As New SimpleLogger(Log_Filepath, UserPath, Codename, Verbose)
    Public Uptime As Date = Date.Now

End Module
