<%@ Page Title="Home Page" Language="VB" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="iBoltz.MultipleImageUpLoader._Default" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Photo Upoader</title>

    <!--    Change Favicon later -->
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <link href="Styles/PhotoUpload.css" rel="stylesheet" />
    <!--                                    Bootstrap                               -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css" />


    <!--                                JQ                        -->
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.3.min.js"></script>

    <!--                                Bootstrap                        -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>


    <!--                                JQ-Plugins                        -->
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.3.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/modernizr/modernizr-2.6.2.js"></script>

    <!--                                iBoltz                        -->
    <script src="Scripts/iBoltz.Loghelper.js"></script>
    <script src="Scripts/iBoltz.PhotoUploader.js"></script>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>

        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container col-lg-12">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand padding-s-top" runat="server" href="~/">
                        <img class="Logo" src="Images/Logo.png" />
                    </a>
                </div>

            </div>
        </div>


        <div class="container body-content">
            <div class="PageContent">
                <!-- Trigger the modal with a button -->
                <div class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Click here to Upload Images</div>

                <!-- Modal -->
                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Multiple Image Uploader</h4>
                            </div>
                            <div class="modal-body">
                                <p>Select and Upload Multiple Images at same time</p>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <icl:MultipleImageUpload runat="server" ID="pclMultipleImageUpload"></icl:MultipleImageUpload>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="pclMultipleImageUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <hr />
            <footer>
                <p>&copy; <%: DateTime.Now.Year %> - iBoltz pvt ltd</p>
            </footer>
        </div>
    </form>
</body>
</html>






