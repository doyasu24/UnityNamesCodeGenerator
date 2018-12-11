using System;
using UnityEngine;

namespace NamesCode
{
    [Serializable]
    public struct SceneName
    {
        public string Name
        {
            get { return _name; }
        }

        [SerializeField] private string _name;

        public int Index
        {
            get { return _index; }
        }

        [SerializeField] private int _index;

        public SceneName(string name, int index)
        {
            _name = name;
            _index = index;
        }
    }
}