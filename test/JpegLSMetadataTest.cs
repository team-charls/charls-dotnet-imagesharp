// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

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
        _ = Assert.IsType<JpegLSMetadata>(clone);
    }

    [Fact]
    public void CloneUsingTheInterface()
    {
#pragma warning disable CA1859
        IDeepCloneable jpegLSMetadata = new JpegLSMetadata();

        var deepClone = jpegLSMetadata.DeepClone();
        Assert.NotNull(deepClone);
        _ = Assert.IsType<JpegLSMetadata>(deepClone);
    }

    [Fact]
    public void FromFormatConnectingMetadata()
    {
        FormatConnectingMetadata metadata = new();
        var jpegLSMetadata = JpegLSMetadata.FromFormatConnectingMetadata(metadata);
        Assert.NotNull(jpegLSMetadata);
    }

    [Fact]
    public void ToFormatConnectingMetadata()
    {
        var jpegLSMetadata = new JpegLSMetadata();
        var metadata = jpegLSMetadata.ToFormatConnectingMetadata();
        Assert.NotNull(metadata);
        Assert.Equal(EncodingType.Lossless, metadata.EncodingType);
    }

    [Fact]
    public void AfterImageApply()
    {
        var jpegLSMetadata = new JpegLSMetadata();
        using var image = new Image<Rgba32>(10, 10);
        var matrix = Matrix4x4.Identity;

        jpegLSMetadata.AfterImageApply(image, matrix);

        Assert.NotNull(image);
    }
}
