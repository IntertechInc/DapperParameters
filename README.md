# DapperParameters
Facade of Dappers DynamicParameters class to facilitate unit testing.

## Define in your dependency injection tool of choice
* Interface:       IDapperParameters
* Implementation:  DapperParameters

SimpleInjector example:

    container.Register<IDapperParameters, DapperParameters>(Lifestyle.Transient);