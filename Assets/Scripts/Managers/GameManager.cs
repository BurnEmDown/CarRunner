using UI;
using UnityEngine;
using EventType = Managers.Events.EventType;

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

        public static void SubscribeToGameEvents()
        {
            UnsubscribeFromGameEvents();
            MainManager.Instance.eventsManager.AddListener(EventType.EnemyCarPassed, PlayerPassedEnemyCar);
            MainManager.Instance.eventsManager.AddListener(EventType.EnemyCarHitPlayer, EnemyCarHitPlayer);
        }

        public static void UnsubscribeFromGameEvents()
        {
            MainManager.Instance.eventsManager.RemoveListener(EventType.EnemyCarPassed, PlayerPassedEnemyCar);
            MainManager.Instance.eventsManager.RemoveListener(EventType.EnemyCarHitPlayer, EnemyCarHitPlayer);
        }

        private static void EnemyCarHitPlayer(object obj)
        {
            if(obj is EnemyCar car)
                EnemyCarHitPlayer(car);
            else
            {
                Debug.LogError($"Called event with wrong object type {obj}");
            }
        }

        public static void EnemyCarHitPlayer(EnemyCar car)
        {
            lives--;
            topStats.UpdateLivesNum(lives);
            if (lives == 0)
            {
                LoadManager.Instance.LoadSummaryScene();
                AudioManager.Instance.PlayCarDestroyedSoundEffect();
            }
            else
            {
                AudioManager.Instance.PlayCarCrashSoundEffect();
            }
            MainManager.Instance.poolManager.ReturnToPool(nameof(EnemyCar),car);
        }
        
        private static void PlayerPassedEnemyCar(object obj)
        {
            if(obj is EnemyCar car)
                PlayerPassedEnemyCar(car);
            else
            {
                Debug.LogError($"Called event with wrong object type {obj}");
            }
        }

        public static void PlayerPassedEnemyCar(EnemyCar car)
        {
            AudioManager.Instance.PlayCarPassSoundEffect();
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

        public static void AssignTopStats(TopStats topStatsObject)
        {
            topStats = topStatsObject;
        }

        public static int GetScore()
        {
            return score;
        }

        public static int GetCarsPassed()
        {
            return carsPassed;
        }
    }
}

