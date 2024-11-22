// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp;

namespace CharLS.Managed.ImageSharp;

/// <summary>
/// Provides JPEG-LS specific metadata information for the image.
/// </summary>
public sealed class JpegLSMetadata : IDeepCloneable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JpegLSMetadata"/> class.
    /// </summary>
    public JpegLSMetadata()
    {
    }

    /// <inheritdoc />
    public IDeepCloneable DeepClone() => new JpegLSMetadata();
}
