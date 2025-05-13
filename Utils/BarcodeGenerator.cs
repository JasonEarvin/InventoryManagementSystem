using ZXing;
using ZXing.Common;
using SkiaSharp;

public static class BarcodeGenerator
{
    public static byte[] GenerateQRCode(string content)
    {
        var writer = new BarcodeWriterPixelData
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Width = 300,
                Height = 300,
                Margin = 1
            }
        };

        var pixelData = writer.Write(content);

        using var bitmap = new SKBitmap(pixelData.Width, pixelData.Height);
        for (int y = 0; y < pixelData.Height; y++)
        {
            for (int x = 0; x < pixelData.Width; x++)
            {
                int index = (y * pixelData.Width + x) * 4;
                byte r = pixelData.Pixels[index];
                byte g = pixelData.Pixels[index + 1];
                byte b = pixelData.Pixels[index + 2];
                byte a = pixelData.Pixels[index + 3];
                bitmap.SetPixel(x, y, new SKColor(r, g, b, a));
            }
        }

        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        return data.ToArray();
    }
}
