# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.2] - 2021-11-22

### Changed

- Removed Polly package reference from Project
- Project nuget package description

## [1.0.1] - 2021-10-08

### Changed

- Removed retry and circuit breaker (Polly)cies

## [1.0.0] - 2021-09-22

### Added

- NuGet package automatic deployment
- SendRequestAsync method for type string return
- SendRequestAsync method for custom deserializable type
- (Polly)cies for retry and circuit breaker
