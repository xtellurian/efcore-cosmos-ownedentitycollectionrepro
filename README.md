# EF Core Cosmos Bug Reproduction

This bug seems to manifest when there are nested collections of Owned Entities using the Cosmos DB provider with Lazy Loading.

[EF Core Issue](https://github.com/aspnet/EntityFrameworkCore/issues/19299)

## Steps to Reproduce

### Normal Operation (In Memory DB)

0. In [AppSettings](src/appsettings.Development.json) check that Key is not set (null or empty)
1. Start the app with `dotnet run` or hit Debug in VS Code
2. Execute `reproduce.sh`. This should work as expected using an In Memory database.

### Error Operation (Cosmos Db Provider)

0. Create a COSMOS Db account
1. In [AppSettings](src/appsettings.Development.json) set the Endpoint, Key, and Database values.
2. Start the app with `dotnet run` or hit Debug in VS Code
3. EF Core will throw an error.

## Error Output

```
ArgumentException: Expression of type 'System.Collections.Generic.ICollection`1[src.Model.Building]' cannot be used for return type 'src.Model.Building'
System.Linq.Expressions.Expression.ValidateLambdaArgs(Type delegateType, ref Expression body, ReadOnlyCollection<ParameterExpression> parameters, string paramName)
```

## Observations

The `Devices` Lazy Loader is throwing the exception, but the exception is not thrown on `Rooms`. The alphabetical order of the properties matters - change `Devices` to `ZDevices` and the exception will throw on `Rooms`.

