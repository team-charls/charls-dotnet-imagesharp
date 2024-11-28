// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CharLS.Managed.ImageSharp.Test;

public class JpegLSEncoderTest
{
    [Fact]
    public void EncodeMonochromeImage()
    {
        using Image image = Image.Load("test-images/tulips-gray-8bit-512-512.pgm").CloneAs<L8>();

        using var memoryStream = new MemoryStream();
        Configuration configuration = new(new JpegLSConfigurationModule());
        var encoder = configuration.ImageFormatsManager.GetEncoder(JpegLSFormat.Instance);

        image.Save(memoryStream, encoder);

        var expected = File.ReadAllBytes("test-images/tulips-gray-8bit-512-512-hp-encoder.jls");
        Compare(expected, memoryStream.ToArray());
    }

    [Fact]
    public void EncodeColorByPixelImage()
    {
        using Image image = Image.Load("conformance/test8.ppm").CloneAs<Rgb24>();

        using var memoryStream = new MemoryStream();
        Configuration configuration = new(new JpegLSConfigurationModule());
        var encoder = configuration.ImageFormatsManager.GetEncoder(JpegLSFormat.Instance);

        image.Save(memoryStream, encoder);

        var expected = File.ReadAllBytes("conformance/t8c2e0.jls");
        Compare(expected, memoryStream.ToArray());
    }

    [Fact]
    public void UnsupportedEncodeThrows()
    {
        using Image image = Image.Load("conformance/test8.ppm").CloneAs<Rgba1010102>();

        using var memoryStream = new MemoryStream();
        Configuration configuration = new(new JpegLSConfigurationModule());
        var encoder = configuration.ImageFormatsManager.GetEncoder(JpegLSFormat.Instance);

        var exception = Assert.Throws<UnknownImageFormatException>(() => image.Save(memoryStream, encoder));
        Assert.False(string.IsNullOrEmpty(exception.Message));
    }

    [Fact]
    public void PassNullImageThrows()
    {
        Configuration configuration = new(new JpegLSConfigurationModule());
        var encoder = configuration.ImageFormatsManager.GetEncoder(JpegLSFormat.Instance);

        using var memoryStream = new MemoryStream();

        var exception = Assert.Throws<NullReferenceException>(() => encoder.Encode<L8>(null!, memoryStream));
        Assert.False(string.IsNullOrEmpty(exception.Message));
    }

    [Fact]
    public void PassNullStreamThrows()
    {
        using var image = Image.Load("test-images/tulips-gray-8bit-512-512.pgm").CloneAs<L8>();
        Configuration configuration = new(new JpegLSConfigurationModule());
        var encoder = configuration.ImageFormatsManager.GetEncoder(JpegLSFormat.Instance);

        var exception = Assert.Throws<NullReferenceException>(() => encoder.Encode(image, null!));
        Assert.False(string.IsNullOrEmpty(exception.Message));
    }

    private static void Compare(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
    {
        for (int i = 0; i < a.Length; i++)
        {
            Assert.Equal(a[i], b[i]);
        }
    }
}
