Imports iBoltz.MultipleImageUpLoader.Web

Public Class MultiImageLoader
    Inherits System.Web.UI.UserControl

    Public Delegate Sub OnUploadCompletedDelegate(ByVal sender As Object, ByVal e As ImageUploadedEventArgs)
    Public Event OnUploadCompleted As OnUploadCompletedDelegate

    Private Property UploadedImages As List(Of String)
        Get
            Return ViewState("UploadedImages")
        End Get
        Set(ByVal value As List(Of String))
            ViewState.Add("UploadedImages", value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            UploadedImages = New List(Of String)
        End If
        pclAttachPhoto.UploaderClientID = Me.pnlMulti.ClientID
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "onload", "PhotoUploadManager.InitUploadTool('" & Me.pnlMulti.ClientID & "');", True)
    End Sub


    Private Sub btnGetUploadedFiles_Click(sender As Object, e As EventArgs) Handles btnGetUploadedFiles.Click

        Dim FromHiddenField = hidUploadedFiles.Value
        Dim MyPhoto = FromHiddenField.Split(If(FromHiddenField.Contains(vbCrLf), vbCrLf, vbLf)).ToList()

        UploadedImages.AddRange(MyPhoto)

        If (UploadedImages Is Nothing) Then Return

        UploadedImages = UploadedImages.Select(Function(x) x.Replace(vbCrLf, String.Empty).Replace(vbLf, String.Empty)).ToList
        UploadedImages = UploadedImages.Where(Function(x) Not String.IsNullOrEmpty(x)).ToList
        LoadGrid()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "onload", "PhotoUploadManager.InitUploadTool('" & Me.pnlMulti.ClientID & "');", True)

    End Sub

    Private Sub LoadGrid()
        rptUploaded.DataSource = UploadedImages
        rptUploaded.DataBind()
        lblPhotoCount.Text = UploadedImages.Count & " photo(s) added in list,Please Save!"
    End Sub
    Private Sub rptUploaded_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptUploaded.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim imgUploaded = DirectCast(e.Item.FindControl("imgUploaded"), Image)
            If (Not String.IsNullOrEmpty(e.Item.DataItem.ToString)) Then
                imgUploaded.ImageUrl = e.Item.DataItem.ToString
                imgUploaded.Visible = True
            Else
                imgUploaded.Visible = False
            End If
        End If

    End Sub
    Private Sub btnSavePhoto_Click(sender As Object, e As EventArgs) Handles btnSavePhoto.Click
        UploadedImages = UploadedImages.Where(Function(x) Not String.IsNullOrEmpty(x)).ToList
        If UploadedImages.Count > 0 Then

            RaiseEvent OnUploadCompleted(sender, New ImageUploadedEventArgs(UploadedImages))

        Else
            Utils.JqMsgBox("No Photos Selected. Drag and Drop Photos Or Click Attach to Select the Photos", "Alert", Me.Page)
        End If
        UploadedImages = New List(Of String)
        LoadGrid()
    End Sub

End Class

