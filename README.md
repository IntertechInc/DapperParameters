# DapperParameters
Facade of Dappers DynamicParameters class to facilitate unit testing.

Available on [NuGet](https://www.nuget.org/packages/DapperParameters/)

Blog post [here](http://www.intertech.com/Blog/unit-test-dapper-with-dapperparameters/)

## Define in your dependency injection tool of choice
* Interface:       IDapperParameters
* Implementation:  DapperParameters

SimpleInjector example:

    container.Register<IDapperParameters, DapperParameters>(Lifestyle.Transient);

## Use in conjunction with DapperWrapper
When used with [DapperWrapper](https://github.com/half-ogre/dapper-wrapper), it is possible to unit test your data layer.

## Sample Unit Tests
Sample unit tests (and setups) are available in the source using Moq and SimpleInjector.
