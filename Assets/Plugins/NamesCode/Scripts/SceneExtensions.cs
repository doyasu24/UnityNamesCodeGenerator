using UnityEngine.SceneManagement;

namespace NamesCode
{
    public static class SceneExtensions
    {
        public static Scene GetScene(this SceneName sceneName)
        {
            return SceneManager.GetSceneByBuildIndex(sceneName.Index);
        }
    }
}