# UnityNamesCodeGenerator
NamesCodeGenerator for Unity generates type safe value objects and static classes from Tag, Layer, SortingLayer, and Scene.

# How to use

import NamesCodeGenerator unitypackage from release page.

execute `Tools/NamesCode/Generate`.

or automatically generate code OnPreprocessBuild.

The code output destination is defined in `Assets/Plugins/NamesCode/Editor/GeneratorSetting.asset`

# Code Generation Result

The format policy follows `Microsoft Visual Studio`

```:Layers.cs
// Generated code by NamesCodeGenerator

namespace NamesCode
{
    public static class Layers
    {
        public static readonly LayerName Default = new LayerName("Default", 0);
        public static readonly LayerName TransparentFX = new LayerName("TransparentFX", 1);
        public static readonly LayerName IgnoreRaycast = new LayerName("Ignore Raycast", 2);
        public static readonly LayerName Water = new LayerName("Water", 4);
        public static readonly LayerName UI = new LayerName("UI", 5);

        public static readonly LayerName[] Names =
        {
            Default,
            TransparentFX,
            IgnoreRaycast,
            Water,
            UI,
        };
    }
}
```

# License

MIT