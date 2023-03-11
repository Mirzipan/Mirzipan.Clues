[![openupm](https://img.shields.io/npm/v/net.mirzipan.framed.definitions?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/net.mirzipan.framed.definitions/) ![GitHub](https://img.shields.io/github/license/Mirzipan/Framed.Definitions)

# Framed.Definitions

Simple module for definition management for Framed.

## Definitions Module

The module that allows access to definitions.

## IDefinitions

Technically the cleaner way of accessing definitions, as it does not expose any way of manipulating them.

```csharp
IEnumerable<T> GetAll<T>() where T : Definition;
```
Returns all definitions of the given type.

```csharp
T Get<T>(CompositeId id) where T : Definition;
```
Returns the definition of the given type, with the specified `id`.

```csharp
T Default<T>() where T : Definition;
```
Returns the default definition of the given type. If no definition is marked as default, it will return `null`.

## Configuration

There is a `DefinitionModuleConfiguration` that you may use for the default setup of `DefinitionsModule`, so that the `Core` will load it.
If you want something more custom, however, feel free to instantiate the module in your custom `IConfiguration` instead.

## CompositeId

Id that consists of 2 parts, `Primary` and `Secondary`, both of which are a `uint` internally. It is up to you, how you use this.
It comes with a property drawer, for easier editing. Both parts of the id are visually represented by 4 characters, with values between `0-255`.

## Definition

Basic type from which all of the other definitions need to inherit.
It contains some basic properties:
* **IsEnabled** - Should this definition be used? 
* **IsDefault** - It this the default definition for the type?
* **SecondaryId** - Second part of the `CompositeId`

You may also override the `Init` method, in case your definition requires some kind of preprocessing before it is usable.
If anything during initialization goes wrong, feel free to throw a `DefinitionInitializationException`.

## Visual Definition

This is both, an example custom definition, and a definition that might actually prove to be useful in your project, for anything that will need to be represented visually. 
It contains:
* **Icon**
* **Full Name**
* **Short Name**
* **Description**

## Definition Type Attribute

Attribute to use on your custom definition type, in order to have it registered as another definition type, in addition to its own.
The current types need to derived from the specified type.

# Examples

Here are some examples of how to use the module.
Keep in mind that there are always multiple possibilities of how to use the definitions.

## Example #1

You need to represent a creature, lets use cerberus as a concrete example.
You need to define its stats, and some kind of visuals.

### Solution A - Separate Definitions ###
Create `CreatureDefinition` with all the stats necessary.
Optionally add some limiters so that people cannot enter values that are no valid for gameplay purposes.
In this case, we will limit all stats to be between 0 and 255.
```csharp
public class CreatureDefinition: Definition
{
    [Header("Stats"]
    [Range(0, 255)]
    public int Health;
    [Range(0, 255)]
    public int Damage;
    [Range(0, 255)]
    public int ManaCost;
}
```
Create an instance of `CreatureDefinition` and fill in the data (we will use `CERB` as `SecondaryId`).
Create an instance of `VisualDefinition` and fill in visual data (same `SecondaryId` as `CreatureDefinition`).
Now, anywhere in your code that you need to use the cerberus, we can get both definitions like this:
```csharp
var id = new CompositeId("CREA", "CERB");
var creatureDefinition = _definitions.Get<CreatureDefinition>(id);
var visualDefinition = _definitions.Get<VisualDefinition>(id);
```

### Solution B - Combined Definition ###
Create `CreatureDefinition` as a child class of `VisualDefinition` with all the stats necessary.
Optionally add some limiters so that people cannot enter values that are no valid for gameplay purposes.
In this case, we will limit all stats to be between 0 and 255.
```csharp
[DefinitionType(typeof(VisualDefinition))]
public class CreatureDefinition: VisualDefinition
{
    [Header("Stats"]
    [Range(0, 255)]
    public int Health;
    [Range(0, 255)]
    public int Damage;
    [Range(0, 255)]
    public int ManaCost;
}
```
Create an instance of `CreatureDefinition` and fill in the data (we will use `CERB` as `SecondaryId`).
Now, anywhere in your code that you need to use the cerberus, we can get both definitions like this:
```csharp
var id = new CompositeId("CREA", "CERB");
var creatureDefinition = _definitions.Get<CreatureDefinition>(id);

// if you only need the visuals, you may call:
var visualDefinition = _definitions.Get<VisualDefinition>(id);
```
