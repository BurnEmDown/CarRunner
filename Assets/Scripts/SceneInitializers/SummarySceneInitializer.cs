using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SceneInitializers
{
    public class SummarySceneInitializer : MonoBehaviour
    {
        [SerializeField] private Button mainMenuButton;
        [SerializeField]

        private void Start()
        {
            mainMenuButton.onClick.AddListener(LoadManager.Instance.LoadMainMenuScene);
        }
    }
}

