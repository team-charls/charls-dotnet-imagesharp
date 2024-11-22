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
}
