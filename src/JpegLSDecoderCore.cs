// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using System.Runtime.InteropServices;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.PixelFormats;

namespace CharLS.Managed.ImageSharp;

internal sealed class JpegLSDecoderCore
{
    private readonly Managed.JpegLSDecoder _decoder = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="JpegLSDecoderCore"/> class.
    /// </summary>
    /// <param name="options">The general decoder options.</param>
    internal JpegLSDecoderCore(DecoderOptions options) => Options = options;

    /// <summary>
    /// Gets the general decoder options.
    /// </summary>
    public DecoderOptions Options { get; }

    /// <summary>
    /// Gets or sets the dimensions of the image being decoded.
    /// </summary>
    public Size Dimensions { get; set; }

    /// <summary>
    /// Reads the raw image information from the specified stream.
    /// </summary>
    /// <param name="configuration">The shared configuration.</param>
    /// <param name="stream">The <see cref="Stream" /> containing image data.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The <see cref="ImageInfo" />.</returns>
    /// <exception cref="InvalidImageContentException">Thrown if the encoded image contains errors.</exception>
    public ImageInfo Identify(
        Configuration configuration,
        Stream stream,
        CancellationToken cancellationToken)
    {
        try
        {
            return Identify(stream, cancellationToken);
        }
        catch (InvalidDataException ex)
        {
            throw new InvalidImageContentException(ex.Message, ex);
        }
    }

    internal ImageInfo Identify(Stream stream, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream(); // TODO optimize (no need to read the complete file)
        stream.CopyTo(memoryStream);
        memoryStream.ToArray();
        _decoder.Source = memoryStream.ToArray();
        _decoder.ReadHeader();

        var frameInfo = _decoder.FrameInfo;
        return new ImageInfo(new PixelTypeInfo(frameInfo.BitsPerSample * frameInfo.ComponentCount),
            new Size(frameInfo.Width, frameInfo.Height), new ImageMetadata());
    }

    internal Image<TPixel> Decode<TPixel>(Stream stream, CancellationToken cancellationToken)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        using var memoryStream = new MemoryStream(); // TODO optimize (no need to read the complete file)
        stream.CopyTo(memoryStream);
        memoryStream.ToArray();
        _decoder.Source = memoryStream.ToArray();
        _decoder.ReadHeader();

        var frameInfo = _decoder.FrameInfo;
        var image = new Image<TPixel>(Options.Configuration, frameInfo.Width, frameInfo.Height);

        Buffer2D<TPixel> pixels = image.Frames.RootFrame.PixelBuffer;

        var a = pixels.MemoryGroup.First();
        Span<byte> sourceInBytes = MemoryMarshal.Cast<TPixel, byte>(a.Span);
        _decoder.Decode(sourceInBytes);

        return image;
    }
}
