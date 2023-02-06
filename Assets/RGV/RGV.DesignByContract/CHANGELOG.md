# Changelog
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.1.0] - 2022-10-02
### Added
- Unify msgs.

## [1.0.1] - 2022-08-28

### Changed
- Use conditional compilation through UNITY_ASSERTIONS directive.
- Fix exception msg whenever any exception was null.

### Removed
- Delete tatic flag to enable/disable contracts.

## [1.0.0] - 2022-7-10
### Added
- Create contracts taxonomy to add semantic.

#### Changed
- Make base contract class abstract.

#### Removed
- Avoid return of the evalueé when satisfying contracts.
  - This breaks the assignation+contract one-liner!

## [0.1.0] - 2022-6-19
### Added
- Upload initial commit.