using TMPro;
using UnityEngine;

namespace UI
{
    public class TopStats : MonoBehaviour
    {
        [SerializeField] private TMP_Text livesNumText;
        [SerializeField] private TMP_Text scoreNumText;
        [SerializeField] private TMP_Text carsNumText;

        public void UpdateLivesNum(int num)
        {
            livesNumText.text = num.ToString();
        }
        
        public void UpdateScoreNum(int num)
        {
            scoreNumText.text = num.ToString();
        }
        
        public void UpdateCarsNum(int num)
        {
            carsNumText.text = num.ToString();
        }
    } 
}

