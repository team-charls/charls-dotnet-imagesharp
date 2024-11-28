// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using CharLS.Managed.ImageSharp;
using SixLabors.ImageSharp;

const int success = 0;
const int failure = 1;

// This sample demonstrates how to convert another encoded image to a JPEG-LS encoded image.
// The input path should be an absolute path to a file format .NET can read (.bmp, .png, etc.).
if (!TryParseArguments(args, out string inputPath))
{
    Console.WriteLine("Usage: Convert input-image-filename");
    return failure;
}

try
{
    using var image = Image.Load(inputPath);

    Configuration configuration = new(new JpegLSConfigurationModule());
    var encoder = configuration.ImageFormatsManager.GetEncoder(JpegLSFormat.Instance);

    using FileStream output = new(GetOutputPath(inputPath), FileMode.OpenOrCreate);
    image.Save(output, encoder);

    return success;
}
catch (IOException e)
{
    Console.WriteLine("Error: " + e.Message);
    return failure;
}
catch (ArgumentException e)
{
    Console.WriteLine($"Invalid path: {inputPath}.");
    Console.WriteLine("Error: " + e.Message);
    return failure;
}

static string GetOutputPath(string inputPathArg)
    => Path.ChangeExtension(inputPathArg, ".jls");

static bool TryParseArguments(IReadOnlyList<string> args, out string inputPathArg)
{
    if (args.Count != 1)
    {
        inputPathArg = string.Empty;
        return false;
    }

    inputPathArg = args[0];
    return true;
}
