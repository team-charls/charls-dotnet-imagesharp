// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp;

namespace CharLS.Managed.ImageSharp;

/// <summary>
/// Registers the image encoders, decoders and mime type detectors for the JPEG-LS format.
/// </summary>
public sealed class JpegLSConfigurationModule : IImageFormatConfigurationModule
{
    /// <inheritdoc/>
    public void Configure(Configuration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        configuration.ImageFormatsManager.SetEncoder(JpegLSFormat.Instance, new JpegLSEncoder());
        configuration.ImageFormatsManager.SetDecoder(JpegLSFormat.Instance, JpegLSDecoder.Instance);
        configuration.ImageFormatsManager.AddImageFormatDetector(new JpegLSImageFormatDetector());
    }
}
