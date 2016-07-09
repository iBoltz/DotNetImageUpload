Imports System.ComponentModel

Public Class PhotoUploader
    Inherits System.Web.UI.UserControl

    Public Delegate Sub OnUploadCompletedDelegate(ByVal sender As Object, ByVal e As ImageUploadedEventArgs)
    Public Event OnUploadCompleted As OnUploadCompletedDelegate

    <Browsable(True),
     Category("MaximumResolution"),
     Description("Indicates what is the maximum size the uploaded photo can be.")>
    Public Property MaximumResolution As Integer?
        Get
            Return ViewState("MaximumResolution_")
        End Get
        Set(ByVal value As Integer?)
            ViewState.Add("MaximumResolution_", value)
        End Set
    End Property

    <Browsable(True),
    Category("AjaxOnly"),
    Description("Indicates Control Is Fully Ajax Compatible.")>
    Public Property AjaxOnly As Boolean
        Get
            Return ViewState("AjaxOnly_")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Add("AjaxOnly_", value)
        End Set
    End Property


    <Browsable(True),
    Category("UploaderClientID"),
    Description("Uploader ClientID.")>
    Public Property UploaderClientID As String
        Get
            Return ViewState("UploaderClientID_")
        End Get
        Set(ByVal value As String)
            ViewState.Add("UploaderClientID_", value)
        End Set
    End Property

    Public Property FilePath As String
    Public Property ImageUrl As String
        Get
            Return imgPhoto.ImageUrl
        End Get
        Set(value As String)
            imgPhoto.ImageUrl = value
        End Set
    End Property

    Public Property CommandArgument As String
        Get
            Return btnUpload.CommandArgument
        End Get
        Set(value As String)
            btnUpload.CommandArgument = value
        End Set
    End Property

    Public Property CommandName As String
        Get
            Return btnUpload.CommandName
        End Get
        Set(value As String)
            btnUpload.CommandName = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (MaximumResolution Is Nothing) Then MaximumResolution = 1024

    End Sub

    Protected Overrides Sub Render(writer As System.Web.UI.HtmlTextWriter)
        Dim UploadPostback As PostBackOptions = New PostBackOptions(btnUpload)

        If (AjaxOnly) Then
            fluPhoto.Attributes.Add("onchange", "PhotoUploadManager.UploadSinglePhoto('" & fluPhoto.ClientID & "','" & UploaderClientID & "');")
        Else
            fluPhoto.Attributes.Add("onchange",
                                Page.ClientScript.GetPostBackEventReference(UploadPostback, True))
        End If


        MyBase.Render(writer)
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As System.EventArgs) Handles btnUpload.Click
        Dim FolderPath = "~/UploadedImages/" & Now.Year & "/" & Now.Month & "/" & Now.Day & "/"
        If (Not My.Computer.FileSystem.DirectoryExists(FolderPath)) Then My.Computer.FileSystem.CreateDirectory(Server.MapPath(FolderPath))
        FilePath = FolderPath & Now.Ticks & fluPhoto.FileName.Remove(0, fluPhoto.FileName.IndexOf("."))

        Dim Resized = ResizePhoto()
        Resized.Save(Server.MapPath(FilePath))

        Dim Output As New List(Of String)

        Output.Add(FilePath)

        If (String.IsNullOrEmpty(CommandName)) Then
            RaiseEvent OnUploadCompleted(sender, New ImageUploadedEventArgs(Output))

        Else
            RaiseBubbleEvent(Me, e)
        End If

    End Sub

    Private Function ResizePhoto() As System.Drawing.Image
        If Not fluPhoto.HasFile Then Return Nothing

        Dim ImageMemStream = New System.IO.MemoryStream(fluPhoto.FileBytes)
        Dim UploadedImage = System.Drawing.Image.FromStream(ImageMemStream)
        If (UploadedImage Is Nothing) Then Return Nothing
        Dim ActualWidth = UploadedImage.Width
        If (ActualWidth <= MaximumResolution) Then Return UploadedImage

        Dim HorizontalResizeRatio = (MaximumResolution / ActualWidth)

        Return ImageHelper.ResizeImage(HorizontalResizeRatio, UploadedImage)


    End Function

End Class
Public Class ImageUploadedEventArgs
    Inherits EventArgs
    Public UploadedImages As List(Of String)


    Public Sub New(ByVal UploadedImages As List(Of String))
        Me.UploadedImages = UploadedImages
    End Sub
End Class