// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

namespace CharLS.Managed.ImageSharp.Test;

public sealed class JpegLSMetadataTest
{
    [Fact]
    public void Create()
    {
        var jpegLSMetadata = new JpegLSMetadata();
        Assert.NotNull(jpegLSMetadata);
    }

    [Fact]
    public void Clone()
    {
        var jpegLSMetadata = new JpegLSMetadata();

        var clone = jpegLSMetadata.DeepClone();
        Assert.NotNull(clone);
    }
}
