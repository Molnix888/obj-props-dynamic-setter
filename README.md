# ObjPropsDynamicSetter

A small library providing object extensions dedicated to getting properties information and manipulating their values via property names known only at runtime. Uses reflection under the hood. Might be particularly useful for playing with models properties in data-driven testing but not limited to it. Supports nested properties access and access to non-public properties.

## Use

Some simple examples of usage:

```csharp
var model = new Model();
var propertyName = "SomeProperty";
var value = 1000;

// Retrieval of PropertyInfo attribute of the property
var propertyInfo = model.GetPropertyInfo(propertyName);

// Retrieval of property value
var value = model.GetPropertyValue<object>(propertyName);

// Setting a new value to a property
model.SetPropertyValue<Model>(propertyName, value);
```

By default only public properties are accessible. In order to access them, optional parameter 'includeNonPublic' should be set as 'true':

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

//Internal initialization logic skipped for simplicity
var model = new Model();
var propertyName = "NestedModel.IntProperty";

var value = model.GetPropertyValue<int>(propertyName);
```
