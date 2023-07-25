using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SceneInitializers
{
    public class MainMenuSceneInitializer : MonoBehaviour
    {
        [SerializeField] private Button StartGameButton;
        [SerializeField] private Button QuitGameButton;

        private void Start()
        {
            StartGameButton.onClick.AddListener(LoadManager.Instance.LoadGameScene);
            QuitGameButton.onClick.AddListener(LoadManager.Instance.QuitGame);
        }
    }
}

