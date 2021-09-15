# File-Scoped Namespaces Resources Repro

## Overview

This repository contains a console application that reproduces an issue with
version `6.0.100-rc.1.21458.32` of the .NET SDK where a type that uses file-scoped
namespaces causes the assembly resource file to be incorrectly named.

To reproduce the issue, clone the repository and then run `dotnet run`.

To get the expected behaviour, change the
[`Resources`](https://github.com/martincostello/FileScopedNamespacesResourcesRepro/blob/3d7cc962c9ecfadc78f294731a93da4501e9a329/Resources.cs#L4-L7)
class to not use file-scoped namespaces.

## Expected Behaviour

```sh
> dotnet run
Resources:en = This is a test string.
Resources:en-AU = This is an Australian test string.
Resources:en-GB = This is a British test string.
Resources:en-IE = This is an Irish test string.
Resources:en-NZ = This is a test string from New Zealand.
Resources:en-US = This is a test string.
Resources:it-IT = This is an Italian test string.
```

## Actual Behavior

```sh
> dotnet run
Resources:en = TestString
Resources:en-AU = TestString
Resources:en-GB = TestString
Resources:en-IE = TestString
Resources:en-NZ = TestString
Resources:en-US = TestString
Resources:it-IT = TestString
```
