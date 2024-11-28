// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.PixelFormats;

namespace CharLS.Managed.ImageSharp;

internal sealed class JpegLSDecoderCore
{
    private readonly Managed.JpegLSDecoder _decoder = new();

    internal JpegLSDecoderCore(DecoderOptions options) => Options = options;

    public DecoderOptions Options { get; }

    internal ImageInfo Identify(Stream stream)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            _decoder.Source = memoryStream.ToArray();
            _decoder.ReadHeader();

            var frameInfo = _decoder.FrameInfo;
            return new ImageInfo(new PixelTypeInfo(frameInfo.BitsPerSample * frameInfo.ComponentCount),
                new Size(frameInfo.Width, frameInfo.Height), new ImageMetadata());
        }
        catch (InvalidDataException e)
        {
            throw new InvalidImageContentException(e.Message, e);
        }
    }

    internal Image<TPixel> Decode<TPixel>(Stream stream)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        memoryStream.ToArray();
        _decoder.Source = memoryStream.ToArray();
        _decoder.ReadHeader();

        var frameInfo = _decoder.FrameInfo;
        var image = new Image<TPixel>(Options.Configuration, frameInfo.Width, frameInfo.Height);

        Buffer2D<TPixel> pixels = image.Frames.RootFrame.PixelBuffer;
        Span<byte> sourceInBytes = MemoryMarshal.Cast<TPixel, byte>(pixels.MemoryGroup[0].Span);
        _decoder.Decode(sourceInBytes);

        return image;
    }
}
