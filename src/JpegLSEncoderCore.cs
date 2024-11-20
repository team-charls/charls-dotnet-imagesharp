// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.PixelFormats;

namespace CharLS.Managed.ImageSharp;

internal sealed class JpegLSEncoderCore
{
    internal static void Encode<TPixel>(Image<TPixel> image, Stream stream, CancellationToken cancellationToken)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        cancellationToken.ThrowIfCancellationRequested();

        var frameInfo = new FrameInfo(image.Width, image.Height, 8, 1);
        var encoder = new Managed.JpegLSEncoder(frameInfo);

        Buffer2D<TPixel> pixels = image.Frames.RootFrame.PixelBuffer;
        Span<byte> sourceInBytes = MemoryMarshal.Cast<TPixel, byte>(pixels.MemoryGroup[0].Span);
        encoder.Encode(sourceInBytes);

        stream.Write(encoder.EncodedData.Span);
    }
}
