// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp;

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
        IDeepCloneable jpegLSMetadata = new JpegLSMetadata();

        var clone = jpegLSMetadata.DeepClone();
        Assert.NotNull(clone);
    }
}
