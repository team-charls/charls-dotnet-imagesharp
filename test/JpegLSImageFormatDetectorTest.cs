// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

namespace CharLS.Managed.ImageSharp.Test;

public class JpegLSImageFormatDetectorTest
{
    [Fact]
    public void HeaderSizeIs4()
    {
        var imageFormatDetector = new JpegLSImageFormatDetector();
        Assert.Equal(4, imageFormatDetector.HeaderSize);
    }

    [Fact]
    public void TryDetectFormatValid()
    {
        var imageFormatDetector = new JpegLSImageFormatDetector();

        var header = new byte[] { 0xFF, 0xD8, 0xFF, 0xC3 };
        bool result = imageFormatDetector.TryDetectFormat(header, out var imageFormat);

        Assert.True(result);
        Assert.NotNull(imageFormat);
    }

    [Fact]
    public void TryDetectFormatNotEnoughBytes()
    {
        var imageFormatDetector = new JpegLSImageFormatDetector();

        var header = new byte[] { 0xFF, 0xD8 };
        bool result = imageFormatDetector.TryDetectFormat(header, out var imageFormat);

        Assert.False(result);
        Assert.Null(imageFormat);
    }

    [Fact]
    public void TryDetectFormatNoMarkersInside()
    {
        var imageFormatDetector = new JpegLSImageFormatDetector();

        var header = new byte[] { 0xFF, 0xD8, 0xFF, 0xD9 };
        bool result = imageFormatDetector.TryDetectFormat(header, out var imageFormat);

        Assert.False(result);
        Assert.Null(imageFormat);
    }

    [Fact]
    public void TryDetectFormatBadVariants()
    {
        var imageFormatDetector = new JpegLSImageFormatDetector();

        var header = new byte[] { 0x00, 0xD8, 0xFF, 0xD9 };
        bool result = imageFormatDetector.TryDetectFormat(header, out var imageFormat);

        Assert.False(result);
        Assert.Null(imageFormat);

        header = [0xFF, 0xD9, 0xFF, 0xD9];
        result = imageFormatDetector.TryDetectFormat(header, out _);
        Assert.False(result);
    }
}
