<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PhotoUploader.ascx.vb" Inherits="iBoltz.MultipleImageUpLoader.PhotoUploader" %>
<div class="PhotoUpload">
    <asp:FileUpload runat="server" AllowMultiple="true" ID="fluPhoto" CssClass="UploadButton" />
    <asp:Image ID="imgPhoto" ImageUrl="../Images/UploadImages.jpg" runat="server" />
    <asp:Button runat="server" ID="btnUpload" Visible="false" />
    
</div>
