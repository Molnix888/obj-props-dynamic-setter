# ObjPropsDynamicSetter

[![Coverage](https://app.codacy.com/project/badge/Coverage/409ed4fb783142a397248831005bae74)](https://www.codacy.com/gh/Molnix888/obj-props-dynamic-setter)
[![Mutation Score](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FMolnix888%2Fobj-props-dynamic-setter%2Fmaster)](https://dashboard.stryker-mutator.io/reports/github.com/Molnix888/obj-props-dynamic-setter/master)

A small library providing object extensions for getting properties information and manipulating their values via property names known at runtime.
Uses reflection under the hood.
Might be particularly useful for playing with models properties in data-driven testing but not limited to it.
Supports nested properties access and access to non-public properties.

## Use

Simple usage examples:

```csharp
var model = new Model();
var propertyName = "Foo";

// PropertyInfo retrieval from a property
var propertyInfo = model.GetPropertyInfo(propertyName);

// Property value retrieval
var propertyValue = model.GetPropertyValue(propertyName);

// Setting new value to a property
var value = 1000;
model.SetPropertyValue(propertyName, value);
```

Public properties are accessible by default.
To access non-public ones, optional parameter 'includeNonPublic' should be set to 'true':

```csharp
model.SetPropertyValue(propertyName, value, true);
```

To access nested property, full path should be specified with dot ('.') as delimiter:

```csharp
public class Model
{
    public NestedModel NestedModel { get; set; }
}

public class NestedModel
{
    public int Bar { get; set; }
}

// Internal initialization logic skipped for simplicity
var model = new Model();
var propertyName = "NestedModel.Bar";

var value = model.GetPropertyValue(propertyName);
```

## Download

The latest release is on [NuGet](https://www.nuget.org/packages/ObjPropsDynamicSetter/) and [GitHub](https://github.com/Molnix888/obj-props-dynamic-setter/packages).
