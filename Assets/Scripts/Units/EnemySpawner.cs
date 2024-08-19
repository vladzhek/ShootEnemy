using System;
using System.Collections;
using Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Units
{
    public class EnemySpawner : MonoBehaviour
    {
        private const int MIN_ENEMY_KILL = 5;
        [Header("Settings")] 
        public GameObject enemyPrefab;
        public Transform[] spawnPoints;
        public float minSpawnInterval = 1f;
        public float maxSpawnInterval = 3f;
        public float minEnemySpeed = 2f;
        public float maxEnemySpeed = 5f; 
        public int enemyHealth = 3;
        public int enemiesToKillForVictory = 5; 
        public Transform FinishLine;

        private int _enemiesKilled = 0; 
        private int _totalEnemiesToSpawn; 
        private int _totalEnemies; 
        private EnemyFactory _enemyFactory; 
        [Inject] private EventService _eventService;

        private void Start()
        {
            _enemyFactory = new EnemyFactory(enemyPrefab);
            
            _totalEnemiesToSpawn = Random.Range(MIN_ENEMY_KILL, enemiesToKillForVictory);
            _totalEnemies = _totalEnemiesToSpawn;
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (_totalEnemiesToSpawn > 0)
            {
                var spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
                yield return new WaitForSeconds(spawnInterval);
                
                var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                
                var enemySpeed = Random.Range(minEnemySpeed, maxEnemySpeed);
                
                Enemy enemy = _enemyFactory.CreateEnemy(spawnPoint.position, enemySpeed, enemyHealth);
                enemy.SetNewTarget(FinishLine);
                enemy.OnEnemyKilled += HandleEnemyKilled;
                enemy.OnReachFinish += EnemyReachFinish;

                _totalEnemiesToSpawn--;
            }
        }

        private void EnemyReachFinish()
        {
            _totalEnemies--;
            if (_totalEnemies <= 0)
                Victory();
        }

        private void HandleEnemyKilled()
        {
            _enemiesKilled++;
            _totalEnemies--;
            if (_enemiesKilled >= enemiesToKillForVictory || _totalEnemies <= 0)
            {
                Victory();
            }
        }

        private void Victory()
        {
            _eventService.InvokeVictory();
            Debug.Log("Player wins!");
        }
    }
}