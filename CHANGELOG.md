<!--
  SPDX-FileCopyrightText: © 2024 Team CharLS
  SPDX-License-Identifier: BSD-3-Clause
-->

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/) and this project adheres to [Semantic Versioning](http://semver.org/).

## [0.5.0 - UNRELEASED]

### Changed

- Updated configuration to build with .NET SDK 10.0

### Added

- Added support for .NET 10.0

## [0.4.1 - 2025-08-10]

### Changed

- Updated the project format from .sln to the new .snlx format (requires .NET SDK 9.0.200 or newer and/or Visual Studio 2022 17.14 or newer).
- Updated to xUnit.net V3.
- Switched to the artifacts output directory model.
- Updated NuGet dependencies to the latest released versions.
  Resolves the issue that SixLabors.ImageSharp 3.1.7 has a known moderate severity vulnerability.
- The project now conforms to the REUSE guidelines.
- Add support for AOT compilation in .NET 8.0 and later.

## [0.4.0 - 2024-11-28]

### Added

- Initial release.
