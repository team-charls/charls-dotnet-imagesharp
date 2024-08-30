// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp.Formats;
using System.Diagnostics.CodeAnalysis;

namespace CharLS.Managed.ImageSharp;

/// <summary>
/// Detects Jpeg file headers
/// </summary>
public sealed class JpegLSImageFormatDetector : IImageFormatDetector
{
    /// <inheritdoc/>
    public int HeaderSize => 2;

    /// <inheritdoc/>
    public bool TryDetectFormat(ReadOnlySpan<byte> header, [NotNullWhen(true)] out IImageFormat? format)
    {
        format = IsSupportedFileFormat(header) ? JpegLSFormat.Instance : null;
        return format != null;
    }

    private bool IsSupportedFileFormat(ReadOnlySpan<byte> header) => header.Length >= HeaderSize && IsJpeg(header);

    private static bool IsJpeg(ReadOnlySpan<byte> header) =>
        header[0] == 0xFF && // 255
        header[1] == 0xD8; // 216
}

