using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneInitializers
{
    public class SummarySceneInitializer : MonoBehaviour
    {
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text carsPassedText;

        private void Start()
        {
            mainMenuButton.onClick.AddListener(LoadManager.Instance.LoadMainMenuScene);
            scoreText.text = GameManager.GetScore().ToString();
            carsPassedText.text = GameManager.GetCarsPassed().ToString();
        }
    }
}

