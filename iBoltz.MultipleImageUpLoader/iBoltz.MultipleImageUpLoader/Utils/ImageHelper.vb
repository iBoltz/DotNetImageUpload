Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class ImageHelper


    Public Sub New()
        MyBase.New()
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here.			
    End Sub

    Public Shared Function ResizeImage(ByVal scaleFactor As Double, ByVal SourceImage As Image) As Image
        'scaleFactor = scaleFactor / 100
        Dim newWidth = CType(SourceImage.Width * scaleFactor, Integer)
        Dim newHeight = CType(SourceImage.Height * scaleFactor, Integer)

        Dim thumbnailBitmap = New Bitmap(newWidth, newHeight)
        Dim thumbnailGraph = Graphics.FromImage(thumbnailBitmap)
        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality
        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality
        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic

        Dim imageRectangle = New Rectangle(0, 0, newWidth, newHeight)

        thumbnailGraph.DrawImage(SourceImage, imageRectangle)
        thumbnailGraph.Dispose()
        'thumbnailBitmap.Dispose()
        Return thumbnailBitmap
    End Function

    Public Shared Function CropImage(ByVal OriginalImage As Bitmap,
                           ByVal TopLeft As Point,
                           ByVal BottomRight As Point) As Bitmap

        Dim btmCropped As New Bitmap((BottomRight.X - TopLeft.X), (BottomRight.Y - TopLeft.Y))
        Using grpOriginal = Graphics.FromImage(btmCropped)
            grpOriginal.DrawImage(OriginalImage,
                              New Rectangle(0, 0, btmCropped.Width, btmCropped.Height),
                                            TopLeft.X, TopLeft.Y, btmCropped.Width,
                                            btmCropped.Height, GraphicsUnit.Pixel)

        End Using
        'grpOriginal.Dispose()
        Return (btmCropped)
    End Function



End Class