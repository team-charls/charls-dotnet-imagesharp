// Copyright (c) Team CharLS.
// SPDX-License-Identifier: BSD-3-Clause

using SixLabors.ImageSharp;

namespace CharLS.Managed.ImageSharp.Test;

public class JpegLSConfigurationModuleTest
{
    [Fact]
    public void Configure()
    {
        var jpegLsConfigurationModule = new JpegLSConfigurationModule();
        var configuration = new Configuration();

        jpegLsConfigurationModule.Configure(configuration);

        var imageFormat = configuration.ImageFormats.First();
        Assert.NotNull(imageFormat);
        Assert.Equal(JpegLSFormat.Instance, imageFormat);
    }

    [Fact]
    public void ConfigureWithNullThrows()
    {
        var jpegLsConfigurationModule = new JpegLSConfigurationModule();

        var exception = Assert.Throws<ArgumentNullException>(() => jpegLsConfigurationModule.Configure(null!));
        Assert.False(string.IsNullOrEmpty(exception.Message));
    }
}
