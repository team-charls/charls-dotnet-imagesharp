// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace CharLS.Managed.ImageSharp;

/// <summary>
/// Provides JPEG-LS specific metadata information for the image.
/// </summary>
public sealed class JpegLSMetadata : IFormatMetadata<JpegLSMetadata>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JpegLSMetadata"/> class.
    /// </summary>
    public JpegLSMetadata()
    {
    }

    internal int BitsPerSample { get; set; }

    internal int ComponentCount { get; set; }

    /// <inheritdoc/>
    public static JpegLSMetadata FromFormatConnectingMetadata(FormatConnectingMetadata metadata) => new();

    /// <inheritdoc/>
    public PixelTypeInfo GetPixelTypeInfo() => new(BitsPerSample * ComponentCount);

    /// <inheritdoc/>
    public FormatConnectingMetadata ToFormatConnectingMetadata()
        => new()
        {
            EncodingType = EncodingType.Lossless,
            PixelTypeInfo = GetPixelTypeInfo(),
        };

    /// <inheritdoc/>
    public void AfterImageApply<TPixel>(Image<TPixel> destination, Matrix4x4 matrix)
        where TPixel : unmanaged, IPixel<TPixel>
    {
    }

    /// <inheritdoc/>
    IDeepCloneable IDeepCloneable.DeepClone() => DeepClone();

    /// <inheritdoc />
    public JpegLSMetadata DeepClone() => new()
    {
        BitsPerSample = BitsPerSample,
        ComponentCount = ComponentCount
    };
}
