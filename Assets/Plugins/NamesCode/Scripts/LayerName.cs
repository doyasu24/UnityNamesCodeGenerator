namespace NamesCode
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