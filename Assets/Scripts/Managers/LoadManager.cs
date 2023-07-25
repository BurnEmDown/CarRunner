using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LoadManager : MonoBehaviour
    {
        public static LoadManager Instance;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Tried to create another GameLoader");
                Destroy(gameObject);
            }
        
            new MainManager();
            DontDestroyOnLoad(gameObject);
        }
        
        public void LoadMainMenuScene()
        {
            AudioManager.Instance.PlayMainMenuMusic();
            SceneManager.LoadScene(0);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(1);
            GameManager.ResetDefaultValues();
            GameManager.SubscribeToGameEvents();
            AudioManager.Instance.PlayCarDoorOpenSoundEffect();
            AudioManager.Instance.PlayCarDriveContinuousSoundEffect();
            AudioManager.Instance.PlayGameMusic();
        }
        
        public void LoadSummaryScene()
        {
            GameManager.UnsubscribeFromGameEvents();
            AudioManager.Instance.PlayMainMenuMusic();
            SceneManager.LoadScene(2);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

