// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp;

namespace CharLS.Managed.ImageSharp;

/// <summary>
/// Encoder for writing the data image to a stream in jpeg-ls format.
/// </summary>
public sealed class JpegLSEncoder : ImageEncoder
{
    /// <inheritdoc/>
    protected override void Encode<TPixel>(Image<TPixel> image, Stream stream, CancellationToken cancellationToken) =>
        JpegLSEncoderCore.Encode(image, stream, cancellationToken);
}
