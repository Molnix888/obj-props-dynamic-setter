# ObjPropsDynamicSetter

![CI](https://github.com/Molnix888/obj-props-dynamic-setter/workflows/CI/badge.svg)
[![Coverage](https://app.codacy.com/project/badge/Coverage/409ed4fb783142a397248831005bae74)](https://www.codacy.com/gh/Molnix888/obj-props-dynamic-setter)
[![Code Quality](https://app.codacy.com/project/badge/Grade/409ed4fb783142a397248831005bae74)](https://www.codacy.com/gh/Molnix888/obj-props-dynamic-setter)
[![Mutation Score](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FMolnix888%2Fobj-props-dynamic-setter%2Fmaster)](https://dashboard.stryker-mutator.io/reports/github.com/Molnix888/obj-props-dynamic-setter/master)

A small library providing object extensions dedicated to getting properties information and manipulating their values via property names known only at runtime. Uses reflection under the hood. Might be particularly useful for playing with models properties in data-driven testing but not limited to it. Supports nested properties access and access to non-public properties.

## Use

Some simple examples of usage:

```csharp
var model = new Model();
var propertyName = "SomeProperty";

// Retrieval of PropertyInfo attribute of the property
var propertyInfo = model.GetPropertyInfo(propertyName);

// Retrieval of property value
var returnedValue = model.GetPropertyValue<object>(propertyName);

// Setting a new value to a property
var value = 1000;
model.SetPropertyValue<Model>(propertyName, value);
```

By default only public properties are accessible. In order to access non-public ones, optional parameter 'includeNonPublic' should be set as 'true':

```csharp
model.SetPropertyValue<Model>(propertyName, value, true);
```

In order to access nested property, full path should be specified with dot ('.') as delimiter:

```csharp
public class Model
{
    public NestedModel NestedModel { get; set; }
}

public class NestedModel
{
    public int IntProperty { get; set; }
}

// Internal initialization logic skipped for simplicity
var model = new Model();
var propertyName = "NestedModel.IntProperty";

var value = model.GetPropertyValue<int>(propertyName);
```

## Download

The latest release is available on [NuGet](https://www.nuget.org/packages/ObjPropsDynamicSetter/) and on [GitHub](https://github.com/Molnix888/obj-props-dynamic-setter/packages).
