using UnityEngine;

namespace NamesCode
{
    public class Sample : MonoBehaviour
    {
        [SerializeField] private SceneName _sceneName;
        [SerializeField] private TagName _tagName;
        [SerializeField] private LayerName _layerName;
        [SerializeField] private SortingLayerName _sortingLayerName;

        private void Start()
        {
            Debug.Log(_sceneName);
            Debug.Log(_tagName);
            Debug.Log(_layerName);
            Debug.Log(_sortingLayerName);
        }
    }
}