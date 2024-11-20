// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

namespace CharLS.Managed.ImageSharp;

internal static class JpegLSConstants
{
    /// <summary>
    /// The collection of mimetypes that equate to a JPEG-LS file.
    /// </summary>
    public static readonly IEnumerable<string> MimeTypes = ["image/jls"];

    /// <summary>
    /// The collection of file extensions that equate to a JPEG-LS file.
    /// </summary>
    public static readonly IEnumerable<string> FileExtensions = ["jls"];
}
