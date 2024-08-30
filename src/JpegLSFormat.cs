// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats;

namespace CharLS.Managed.ImageSharp;

/// <summary>
/// Registers the image encoders, decoders and mime type detectors for the jpeg format.
/// </summary>
public sealed class JpegLSFormat : IImageFormat<JpegMetadata>
{
    private JpegLSFormat()
    {
    }

    /// <summary>
    /// Gets the shared instance.
    /// </summary>
    public static JpegLSFormat Instance { get; } = new();

    /// <inheritdoc/>
    public string Name => "JPEG-LS";

    /// <inheritdoc/>
    public string DefaultMimeType => "image/jls";

    /// <inheritdoc/>
    public IEnumerable<string> MimeTypes => JpegLSConstants.MimeTypes;

    /// <inheritdoc/>
    public IEnumerable<string> FileExtensions => JpegLSConstants.FileExtensions;

    /// <inheritdoc/>
    public JpegMetadata CreateDefaultFormatMetadata() => new(); // TODO
}
