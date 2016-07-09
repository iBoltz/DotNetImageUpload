var IsPhotoUploaderLoaded = true;


var PhotoUploadManager = {
    box: {},
    InitUploadTool: function (PhotoUploaderID) {
        try {
            if (!Modernizr.draganddrop) {
                //incase of not supported browsers
                alert("This browser doesn't support File API and Drag & Drop features of HTML5!");
                return;
            }

            var Uploader = $('#' + PhotoUploaderID);
            var Box = Uploader.find(".DropBox");
            box = Box[0];

            //register events
            box.addEventListener("dragenter", OnDragEnter, false);
            box.addEventListener("dragover", OnDragOver, false);
            box.addEventListener("drop", OnDrop, false);

            function OnDragEnter(e) {
                e.stopPropagation();
                e.preventDefault();
            }

            function OnDragOver(e) {
                e.stopPropagation();
                e.preventDefault();
            }

            function OnDrop(e) {
                e.stopPropagation();
                e.preventDefault();
                var selectedFiles = e.dataTransfer.files;
                Box.text(selectedFiles.length + " file(s) selected for uploading!");

                PhotoUploadManager.UploadPhotos(selectedFiles, Uploader);

            }
        }
        catch (ex) {
            Loghelper.HandleException("InitUploadTool", ex)
        }

    },
    UploadSinglePhoto: function (FileUploaderClientID, Uploader) {
        try {
            var selectedFiles = [];
            var fileInput = $('#' + FileUploaderClientID);

            if (fileInput.prop("files").length = 0) {
                //no files selected
                alert('No Files Selected.');
                return;
            }

            var fileData = fileInput.prop("files")[0];   // Gettingfiles

            selectedFiles.push(fileData);
            PhotoUploadManager.UploadPhotos(selectedFiles, Uploader);
        }
        catch (ex) {

            Loghelper.HandleException("UploadSinglePhoto", ex)

        }
    },
    UploadPhotos: function (selectedFiles, Uploader) {
        try {

            var data = new FormData();
            for (var i = 0; i < selectedFiles.length; i++) {
                if (selectedFiles[i] != null) {
                    data.append(selectedFiles[i].name, selectedFiles[i]);
                }
            }
            $.ajax({
                type: "POST",
                url: "/Utils/MultipleImageHandler.ashx",
                contentType: false,
                processData: false,
                data: data,
                xhr: function () {
                    var myXhr = $.ajaxSettings.xhr();
                    if (myXhr.upload) {
                        myXhr.upload.addEventListener('progress', PhotoUploadManager.DoProgress(Uploader), false);
                    }
                    return myXhr;
                },
                success: function (result) {
                    // CallBack(result);
                    if (Uploader.length > 1) { Uploader = $('#' + Uploader); }
                    Uploader.find(".DropBox").html("Files Uploaded Successfully!<br />" + result)

                    Uploader.find("#hidUploadedFiles").val(result); // Save the file names into hidden field
                    Uploader.find('#btnGetUploadedFiles').trigger('click'); //trigger a click to raise event in the server to notify the file uploaded

                },
                error: function (e) {
                    PhotoUploadManager.box.text("There was error uploading files!");
                }
            });
        }
        catch (ex) {
            Loghelper.HandleException("UploadPhotos", ex)
        }
    },
    DoProgress: function (Uploader, e) {
        try {
            if (typeof (e) != 'undefined') {
                if (e.lengthComputable) {
                    var max = e.total;
                    var current = e.loaded;

                    var Percentage = current / max;
                    var TotalWidth = Uploader.find('.ProgressBar').width()
                    var CurrentWidth = TotalWidth * Percentage;
                    Uploader.find('.Progress').width(CurrentWidth);

                    if (Percentage >= 100) {
                        PhotoUploadManager.Uploader.find('.Progress').width(0);
                    }
                }
            }
        }
        catch (ex) {
            Loghelper.HandleException("DoProgress", ex)
        }
    }
};
