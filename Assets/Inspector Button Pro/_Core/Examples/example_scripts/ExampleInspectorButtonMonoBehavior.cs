using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace com.cyborgAssets.internalIBPExample
{

    public class ExampleInspectorButtonMonoBehavior : MonoBehaviour
    {

        public GameObject[] targets;

        [ProButton]
        string GetGameObjectName()
        {
            return gameObject.name;
        }

        [ProButton]
        void SpecialAttack(float attackPower)
        {
            Debug.Log("perfromed special attack with attackPower = " + attackPower);
        }

        [ProPlayButton]
        static void LoadScene(int sceneIndex)
        {
            Debug.Log("Loading Scene:" + sceneIndex);
            SceneManager.LoadScene(sceneIndex);
        }

        [ProPlayButton]
        void DestroyAllEnemies()
        {
            Debug.Log("Destroying " + targets.Length + " enemies");
            for (int i = 0; i < targets.Length; i++)
            {
                GameObject.Destroy(targets[i]);
            }
        }

        

    }
}
