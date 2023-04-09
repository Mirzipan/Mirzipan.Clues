# Changelog

## [2.0.0] - 2023-04-09

### Changed
- Renamed from Framed.Definitions to Mirzipan.Clues, since it is no longer a plugin for Framed
- Renamed DefinitionsModule to just Definitions
- Updated namespaces

### Removed
- Removed reference to Mirzipan.Framed
- Removed reference to Mirzipan.Infusion
- Removed configuration

## [1.2.0] - 2023-03-24

### Added
- VisualDefinition now contains Color

### Changed
- VisualDefinition now only has read-only properties accessible

## [1.1.4] - 2023-03-16

### Changed
- Bibliotheca version 1.3.1
- Infusion version 2.0.0-alpha.1

## [1.1.3] - 2023-03-15

### Changed
- Bibliotheca version 1.3.0

## [1.1.2] - 2023-03-13

### Fixed
- Fixed trying to access definition.Code instead of definition.Id in DefinitionsModule

## [1.1.1] - 2023-03-11

### Added
- Methods for modifying default definitions during runtime

## [1.1.0] - 2023-03-11

### Changed
- Definition now has a CompositeId instead of just the SecondaryId

## [1.0.0] - 2023-03-09

### Added
- Initial version of DefinitionsModule
- Base abstract Definition
- Simple VisualDefinition to serve as a base of others