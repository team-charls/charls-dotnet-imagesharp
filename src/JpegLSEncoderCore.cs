// SPDX-FileCopyrightText: © 2024 Team CharLS
// SPDX-License-Identifier: BSD-3-Clause

using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.PixelFormats;

namespace CharLS.Managed.ImageSharp;

internal static class JpegLSEncoderCore
{
    internal static void Encode<TPixel>(Image<TPixel> image, Stream stream, CancellationToken cancellationToken)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        cancellationToken.ThrowIfCancellationRequested();

        var frameInfo = new FrameInfo(image.Width, image.Height, 8, GetComponentCount(image));
        var encoder = new Managed.JpegLSEncoder(frameInfo) { InterleaveMode = GetInterleaveMode(image) };

        Buffer2D<TPixel> pixels = image.Frames.RootFrame.PixelBuffer;
        Span<byte> sourceInBytes = MemoryMarshal.Cast<TPixel, byte>(pixels.MemoryGroup[0].Span);
        encoder.Encode(sourceInBytes);

        stream.Write(encoder.EncodedData.Span);
    }

    private static int GetComponentCount(Image image)
        => image.PixelType.BitsPerPixel switch
        {
            8 => 1,
            24 => 3,
            _ => throw new UnknownImageFormatException("Unsupported pixel format.")
        };

    private static InterleaveMode GetInterleaveMode(Image image)
    => image.PixelType.BitsPerPixel switch
    {
        24 => InterleaveMode.Sample,
        _ => InterleaveMode.None
    };
}
