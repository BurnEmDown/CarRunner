using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loaders
{
    public class GameLoader : MonoBehaviour
    {
        public static GameLoader Instance;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Tried to create another GameLoader");
            }
        
            new MainManager();
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Start()
        {
            
            //SceneManager.LoadScene("MainGameScene");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Destroy(gameObject);
        }
    }
}

