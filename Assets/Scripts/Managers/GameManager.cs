using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public static class GameManager
    {
        private static TopStats topStats;

        private const int startLives = 3;
        private const int startScore = 0;
        private const int startCarsPassed = 0;
        
        private static int lives;
        private static int score;
        private static int carsPassed;

        public static void EnemyCarHitPlayer()
        {
            lives--;
            topStats.UpdateLivesNum(lives);
            if (lives == 0)
            {
                LoadManager.Instance.LoadSummaryScene();
            }
        }

        public static void PlayerPassedEnemyCar(EnemyCar car)
        {
            score += car.ScoreGiven;
            topStats.UpdateScoreNum(score);
            carsPassed++;
            topStats.UpdateCarsNum(carsPassed);
            MainManager.Instance.poolManager.ReturnToPool(nameof(EnemyCar),car);
        }

        public static void ResetDefaultValues()
        {
            lives = startLives;
            score = startScore;
            carsPassed = startCarsPassed;
        }
    }
}

