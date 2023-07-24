using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        protected MainManager Manager => MainManager.Instance;

        [SerializeField] private TopStats topStats;
        
        [SerializeField] private int lives;
        [SerializeField] private int score;
        [SerializeField] private int carsPassed;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Manager.poolManager.InitPool<EnemyCar>(nameof(EnemyCar),10);
        }

        public void EnemyCarHitPlayer()
        {
            lives--;
            topStats.UpdateLivesNum(lives);
            if (lives == 0)
            {
                SceneManager.LoadScene(2); // summary scene
            }
        }

        public void PlayerPassedEnemyCar(EnemyCar car)
        {
            score += car.ScoreGiven;
            topStats.UpdateScoreNum(score);
            carsPassed++;
            topStats.UpdateCarsNum(carsPassed);
            MainManager.Instance.poolManager.ReturnToPool(nameof(EnemyCar),car);
        }
    }
}

