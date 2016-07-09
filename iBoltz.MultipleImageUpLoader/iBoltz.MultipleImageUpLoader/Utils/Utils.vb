Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Web.UI
Imports System.Web
Imports System.Web.HttpUtility
Imports System.Collections.Generic
Imports System.Net
Imports System.Text
Imports iboltz.common.helpers.File



Namespace Web
    Public Class Utils



        Public Shared Sub JqMsgBox(Message As String, Title As String, CurrentPage As Page)
            Dim ScriptBuilder As New StringBuilder
            'ScriptBuilder.AppendLine("<script type='text/javascript'>")

            ScriptBuilder.AppendLine("ShowJqMsgBox('" & Message & "','" & Title & "');")
            'ScriptBuilder.AppendLine("</script>")

            ScriptManager.RegisterClientScriptBlock(CurrentPage, CurrentPage.GetType,
                                                    "JqMsgBox", ScriptBuilder.ToString, True)

        End Sub

        Public Shared Sub JqToast(Message As String, CurrentPage As Page)
            Dim ScriptBuilder As New StringBuilder
            'ScriptBuilder.AppendLine("<script type='text/javascript'>")

            ScriptBuilder.AppendLine("$(function() {ShowJqToast('" & Message & "');});")
            'ScriptBuilder.AppendLine("</script>")

            ScriptManager.RegisterClientScriptBlock(CurrentPage, CurrentPage.GetType,
                                                    "ShowJqToast", ScriptBuilder.ToString, True)

        End Sub
    End Class
End Namespace
