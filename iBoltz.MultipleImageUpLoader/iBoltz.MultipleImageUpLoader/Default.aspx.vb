Imports iBoltz.MultipleImageUpLoader

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Private Sub pclMultipleImageUpload_OnUploadCompleted(sender As Object, e As ImageUploadedEventArgs) Handles pclMultipleImageUpload.OnUploadCompleted

        MsgBox("Photos Uploaded Succesfully")
    End Sub
End Class