<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MultiImageUpload.ascx.vb" Inherits="iBoltz.MultipleImageUpLoader.MultiImageLoader" %>
<%@ Register TagPrefix="iclx" TagName="PhotoUpload" Src="PhotoUploader.ascx" %>

<asp:Panel runat="server" ID="pnlMulti" class="MultiplePhotoUpload text-center">
    <div id="DropBox" class="DropBox">
        <div class="Message">
            <div class="Icon col-lg-1">
                <iclx:PhotoUpload runat="server" ID="pclAttachPhoto" AjaxOnly="True" />
            </div>
            <br />
            <div class="Content col-lg-9">Drag & Drop images from local folders</div>
        </div>

    </div>

    <div class="hidden">
        <asp:HiddenField runat="server" ID="hidUploadedFiles" ClientIDMode="Static" />
        <asp:Button runat="server" ID="btnGetUploadedFiles" ClientIDMode="Static"
            CssClass="UploadedFilesButton" Text="try push" />
    </div>

    <div class="UploadedFileList">
        <asp:Repeater runat="server" ID="rptUploaded">
            <ItemTemplate>
                <asp:Image ImageUrl="imageurl" ID="imgUploaded" runat="server" Height="100" Visible="false" />
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div class="col-md-12">
        <div class="Progress hide"></div>
        <asp:Label Text="" ID="lblPhotoCount" runat="server" />
    </div>
    <br />
    <asp:Button Text="Save & Exit" class="btn btn-success margin-l-top" runat="server" ID="btnSavePhoto" />
</asp:Panel>
