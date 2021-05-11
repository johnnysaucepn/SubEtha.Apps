# README #

## SubEtha Apps for Elite: Dangerous

[![AppVeyor build status](https://img.shields.io/appveyor/ci/johnnysaucepn/subetha-apps/master)](https://ci.appveyor.com/project/johnnysaucepn/subetha-apps/branch/master)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/johnnysaucepn/subetha-apps/master)](https://ci.appveyor.com/project/johnnysaucepn/subetha-apps/build/tests?branch=master)
[![Coverlet code coverage](https://img.shields.io/codecov/c/github/johnnysaucepn/SubEtha.Apps/master)](https://codecov.io/gh/johnnysaucepn/SubEtha.Apps)

This is a set of .NET Core apps that use the [![SubEtha libraries](https://github.com/johnnysaucepn/SubEtha)] for parsing and consuming Elite: Dangerous Player's Journal log events and key bindings.

These make use of a set of additional libraries (called *Howatworks.Thumb*) that offer some useful capabilities for getting started in making tools.

### Howatworks.Matrix and Howatworks.Assistant

These working applications use the libraries to illustrate some of the possibilities.

*Matrix* watches for certain status changes of ships and locations, and submits them to a central ASP.NET Core server - this allows groups of ships to be monitored in real-time.

*Assistant* operates as a virtual control panel - by checking for controls bound to keys, it can provide a Websocket-powered web front-end to ship operations. In addition, since it also monitors journal events, it can dynamically switch control options according to the situation.

**See README files in each application's source folder for more information**

## Installation

Build and run the applications using Visual Studio 2019+, or at the command line using:
```
dotnet tool restore
dotnet cake
```

## Dependencies

The SubEtha apps make use of the following libraries and applications:
* log4net
* Entity Framework Core and PostgreSQL for Matrix web site
* ASP.NET Core for web service hosting
* Autofac for dependency injection
* GitVersion for semantic versioning
* InputSimulatorStandard for virtual key presses
* Newtonsoft.Json for journal serialization
* xUnit.net for testing

## Release History

* 0.1
  * First public release
* 0.5
  * Rearchitected to use Reactive Extensions (Rx) to simplify data flow
* 0.7
  * Migrated to separate repository
  
Distributed under the MIT license. See ``LICENSE.md`` for more information.
