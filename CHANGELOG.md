# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [unreleased]

## [0.0.4] - 2021-09-21

### Added

- NuGet package automatic deployment
- SendRequestAsync method for type string return
- SendRequestAsync method for custom deserializable type
- (Polly)cies for retry and circuit breaker

### Changed

- RequestAsync name to SendRequestAsync

### Fixed

-   Utils.GetRequestUri baseUri argument when null it throws exception
