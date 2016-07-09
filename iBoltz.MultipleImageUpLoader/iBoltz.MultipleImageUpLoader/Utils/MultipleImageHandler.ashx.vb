Imports System.Web
Imports System.Web.Services
Imports System.IO
Public Class MultipleImageHandler
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim OutputWriter As New StringBuilder

        If (context.Request.Files.Count > 0) Then
            Dim UploadedFiles = context.Request.Files
            For Each UploadedImageName In UploadedFiles
                Dim PhotoFile As HttpPostedFile = UploadedFiles(UploadedImageName)

                Dim SavedPath = SavePhoto(PhotoFile, context)
                OutputWriter.AppendLine(SavedPath)
            Next
            context.Response.ContentType = "text/plain"
            context.Response.Write(OutputWriter.ToString)
        End If
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Private Function SavePhoto(PhotoFile As HttpPostedFile, context As HttpContext) As String
        Dim FolderPath = "~/UploadedImages/" & Now.Year & "/" & Now.Month & "/" & Now.Day & "/"
        If (Not My.Computer.FileSystem.DirectoryExists(FolderPath)) Then My.Computer.FileSystem.CreateDirectory(context.Server.MapPath(FolderPath))
        Dim FilePath = FolderPath & Now.Ticks & PhotoFile.FileName.Remove(0, PhotoFile.FileName.LastIndexOf("."))

        Dim Resized = ResizePhoto(PhotoFile)
        Resized.Save(context.Server.MapPath(FilePath))
        Return FilePath
    End Function

    Private Function ResizePhoto(fluPhoto As HttpPostedFile) As System.Drawing.Image
        If fluPhoto Is Nothing Then Return Nothing
        Dim MaximumResolution = 1024
        'Dim ImageMemStream = New System.IO.MemoryStream.r()
        Dim UploadedImage = System.Drawing.Image.FromStream(fluPhoto.InputStream)
        If (UploadedImage Is Nothing) Then Return Nothing
        Dim ActualWidth = UploadedImage.Width
        If (ActualWidth <= MaximumResolution) Then Return UploadedImage

        Dim HorizontalResizeRatio = (MaximumResolution / ActualWidth)
        Return ImageHelper.ResizeImage(HorizontalResizeRatio, UploadedImage)
    End Function
End Class