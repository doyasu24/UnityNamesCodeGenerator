using System;
using UnityEngine;

namespace NamesCode
{
    [Serializable]
    public struct SortingLayerName
    {
        public string Name
        {
            get { return _name; }
        }

        [SerializeField] private string _name;

        public int Id
        {
            get { return _id; }
        }

        [SerializeField] private int _id;

        public SortingLayerName(string name, int id)
        {
            _name = name;
            _id = id;
        }
    }
}