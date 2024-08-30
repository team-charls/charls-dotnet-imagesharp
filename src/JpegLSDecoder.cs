// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace CharLS.Managed.ImageSharp;

/// <summary>
/// Decoder for generating an image out of a gif encoded stream.
/// </summary>
public sealed class JpegLSDecoder : ImageDecoder
{
    private JpegLSDecoder()
    {
    }

    /// <summary>
    /// Gets the shared instance.
    /// </summary>
    public static JpegLSDecoder Instance { get; } = new();

    /// <inheritdoc/>
    protected override ImageInfo Identify(DecoderOptions options, Stream stream, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(stream, nameof(stream));

        return new JpegLSDecoderCore(options).Identify(options.Configuration, stream, cancellationToken);
    }

    /// <inheritdoc/>
    protected override Image<TPixel> Decode<TPixel>(DecoderOptions options, Stream stream, CancellationToken cancellationToken)
    {
        //Guard.NotNull(options, nameof(options));
        //Guard.NotNull(stream, nameof(stream));

        JpegLSDecoderCore decoder = new(options);
        Image<TPixel> image = decoder.Decode<TPixel>(stream, cancellationToken);

        ScaleToTargetSize(options, image);

        return image;
    }

    /// <inheritdoc/>
    protected override Image Decode(DecoderOptions options, Stream stream, CancellationToken cancellationToken)
        => Decode<L8>(options, stream, cancellationToken); // TODO: determine type needed to decode (see PNG method)
}
