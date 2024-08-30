// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp.Formats;

namespace CharLS.Managed.ImageSharp.Test;

public class JpegLSFormatTest
{
    [Fact]
    public void InstanceHasExpectedJpegLSValues()
    {
        IImageFormat imageFormat = JpegLSFormat.Instance;

        Assert.Equal("JPEG-LS", imageFormat.Name);
        Assert.Equal("image/jls", imageFormat.DefaultMimeType);
        Assert.Single(imageFormat.MimeTypes);
        Assert.Equal("image/jls", imageFormat.MimeTypes.First());
        Assert.Single(imageFormat.FileExtensions);
        Assert.Equal("jls", imageFormat.FileExtensions.First());
    }
}
