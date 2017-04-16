# HappyMe

Online educational platform supporting education, development and happiness of children with autism.

## Status

| master                                                                                                                                                                 | development                                                                                                                                                                            |
|------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![Build status](https://ci.appveyor.com/api/projects/status/1l6uxwqf6g4mgs2e/branch/master?svg=true)](https://ci.appveyor.com/project/Teodor92/happyme/branch/master) | [![Build status](https://ci.appveyor.com/api/projects/status/8uejjiw2qv4ac4qu?svg=true)](https://ci.appveyor.com/project/Teodor92/happyme-i6axp) |

| Better Code Hub Compliance                                                                                    | Test Coverage |
|---------------------------------------------------------------------------------------------------------------|---------------|
| [![BCH compliance](https://bettercodehub.com/edge/badge/HappyMeBulgaria/HappyMe)](https://bettercodehub.com/) | [![codecov](https://codecov.io/gh/HappyMeBulgaria/HappyMe/branch/master/graph/badge.svg)](https://codecov.io/gh/HappyMeBulgaria/HappyMe) |

## Technology stack

* .NET Framework - C#, EntityFramework 6, ASP.NET MVC 5
* HTML5, CSS3, Bootstrap 3
* JavaScript, JQuery, JQuery UI
* Xunit, Jasmine, Moq

## Development guidelines

### Git branching strategy
Branches:
- `master` - contains only production ready code.
- `development` - all active development is conducted in this branch.
- `feature-{description}` - contains spesific feature development.

When a new feature is started a new `feature` branch is created form the `development` branch. After the feature is complete a pull request from the `feature` branch to the `development` branch is created. Direct merges, without code review are **NOT** allowed.

### Definiton of done (DoD)

For a feature to be considered "Done", the following requirements **must be met**:
- The code of the feature **must compile**.
- The code of the feature **must NOT produce any warring**.
- The code must be **in compliance with the StyleCop and ESLint coding standards**.
- The code must be **covered by unit tests** (xUnit or Jasmine).
- The pull request created from the feature branch to the development branch, **must review by at least one person**.

## Contributors

* Kristian Mariyanov - ([https://github.com/KristianMariyanov](https://github.com/KristianMariyanov))
* Maria Hristoforova - ([https://github.com/mhristoforova](https://github.com/mhristoforova))
* Rositsa Popova - ([https://github.com/rozay](https://github.com/rozay))
* Teodor Kurtev - ([https://github.com/Teodor92](https://github.com/Teodor92))
* Yana Slavcheva - ([https://github.com/YanaSlavcheva](https://github.com/YanaSlavcheva))
