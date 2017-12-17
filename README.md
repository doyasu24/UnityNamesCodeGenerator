# UnityNamesCodeGenerator
NamesCodeGenerator for Unity generates type safe value objects and static classes from Tag, Layer, etc.

# How to use

import NamesCodeGenerator unitypackage from release page.

create a EditorScript or use [SampleEditorScript](https://github.com/kado-yasuyuki/UnityNamesCodeGenerator/blob/master/UnityNamesCodeGenerator/Assets/NamesCodeGenerator/Sample/Editor/SampleEditorScript.cs).

select `Assets/Names Code Generator/Generate`.

# Code Generation Result

The format policy follows `Microsoft Visual Studio`

```:Layers.cs
// Generated code by NamesCodeGenerator
namespace Names
{
    public static class Layers
    {
        public static readonly LayerName Default = new LayerName("Default", 0);
        public static readonly LayerName TransparentFX = new LayerName("TransparentFX", 1);
        public static readonly LayerName Ignore_Raycast = new LayerName("Ignore Raycast", 2);
        public static readonly LayerName Water = new LayerName("Water", 4);
        public static readonly LayerName UI = new LayerName("UI", 5);

        public static readonly LayerName[] Names = { Default, TransparentFX, Ignore_Raycast, Water, UI };
    }
}

```

```:LayerName.cs
// Generated code by NamesCodeGenerator
namespace Names
{
    public struct LayerName
    {
        public readonly string Name;
        public readonly int Index;

        public LayerName(string name, int index)
        {
            Name = name;
            Index = index;
        }
    }
}

```

or you can also generate following code.

```LayerName.cs
// Generated code by NamesCodeGenerator
namespace Names.Simple
{
    public static class LayerName
    {
        public const string Default = "Default";
        public const string TransparentFX = "TransparentFX";
        public const string Ignore_Raycast = "Ignore Raycast";
        public const string Water = "Water";
        public const string UI = "UI";
    }
}

```


# License

MIT