// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace CharLS.Managed.ImageSharp.Test;

public class JpegLSDecoderTest
{
    [Fact]
    public void DecodeMonochromeImage()
    {
        Configuration configuration = new(new JpegLSConfigurationModule());
        var options = new DecoderOptions { Configuration = configuration };
        using var image = Image.Load(options, "test-images/tulips-gray-8bit-512-512-hp-encoder.jls");

        Assert.Equal(512, image.Width);
        Assert.Equal(512, image.Height);
        Assert.Equal(8, image.PixelType.BitsPerPixel);

        using var expected = Image.Load("test-images/tulips-gray-8bit-512-512.pgm");
        Compare(expected.CloneAs<L8>(), image.CloneAs<L8>());
    }

    [Fact]
    public void DecodeColorByPixelImage()
    {
        Configuration configuration = new(new JpegLSConfigurationModule());
        var options = new DecoderOptions { Configuration = configuration };
        using var image = Image.Load(options, "conformance/t8c2e0.jls");

        Assert.Equal(256, image.Width);
        Assert.Equal(256, image.Height);
        Assert.Equal(24, image.PixelType.BitsPerPixel);

        using var expected = Image.Load("conformance/test8.ppm");
        Compare(expected.CloneAs<Rgb24>(), image.CloneAs<Rgb24>());
    }

    [Fact]
    public void Identify()
    {
        Configuration configuration = new(new JpegLSConfigurationModule());
        var options = new DecoderOptions { Configuration = configuration };
        var imageInfo = Image.Identify(options, "test-images/tulips-gray-8bit-512-512-hp-encoder.jls");

        Assert.Equal(512, imageInfo.Width);
        Assert.Equal(512, imageInfo.Height);
        Assert.Equal(8, imageInfo.PixelType.BitsPerPixel);
    }

    [Fact]
    public void IdentifyNonJpegLSThrows()
    {
        Configuration configuration = new(new JpegLSConfigurationModule());
        var options = new DecoderOptions { Configuration = configuration };

        var exception = Assert.Throws<UnknownImageFormatException>(() => Image.Identify(options, "conformance/test8.ppm"));
        Assert.False(string.IsNullOrEmpty(exception.Message));
    }

    [Fact]
    public void IdentifyBadJpegLSThrows()
    {
        Configuration configuration = new(new JpegLSConfigurationModule());
        var options = new DecoderOptions { Configuration = configuration };

        var header = new byte[] { 0xFF, 0xD8, 0xFF, 0xC3 };
        var exception = Assert.Throws<InvalidImageContentException>(() => Image.Identify(options, header));
        Assert.False(string.IsNullOrEmpty(exception.Message));
    }

    [Fact]
    public void DecodeUnsupportedFormatThrows()
    {
        Configuration configuration = new(new JpegLSConfigurationModule());
        var options = new DecoderOptions { Configuration = configuration };

        var exception = Assert.Throws<UnknownImageFormatException>(() => Image.Load(options, "test-images/2bit_4x1.jls"));
        Assert.False(string.IsNullOrEmpty(exception.Message));
    }

    private static void Compare(Image<L8> expected, Image<L8> actual)
    {
        int height = expected.Height;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < expected.Width; j++)
            {
                bool result = expected[i, j] == actual[i, j];
                Assert.True(result);
            }
        }
    }

    private static void Compare(Image<Rgb24> expected, Image<Rgb24> actual)
    {
        int height = expected.Height;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < expected.Width; j++)
            {
                bool result = expected[i, j] == actual[i, j];
                Assert.True(result);
            }
        }
    }
}
