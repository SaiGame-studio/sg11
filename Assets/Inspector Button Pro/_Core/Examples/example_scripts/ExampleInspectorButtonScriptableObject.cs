

using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.cyborgAssets.internalIBPExample
{
    public class ExampleInspectorButtonScriptableObject : ScriptableObject
    {
        [ProButton]
        float Add2Numbers(int a, int b)
        {
            return a + b;
        }

        [ProPlayButton]
        void LoadScene(int sceneIndex)
        {
            Debug.Log("Loading Scene:" + sceneIndex);
            SceneManager.LoadScene(sceneIndex);
        }


    }
}
