using UnityEngine;

namespace Units
{
    public class EnemyFactory
    {
        private GameObject _enemyPrefab;
        private Transform _parent;

        public EnemyFactory(GameObject enemyPrefab, Transform parent = null)
        {
            _enemyPrefab = enemyPrefab;
            _parent = parent;
        }
        
        public Enemy CreateEnemy(Vector3 spawnPosition, float speed, int health)
        {
            GameObject enemyObject = Object.Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, _parent);
            
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.speed = speed;
                enemy.health = health;
            }

            return enemy;
        }
    }
}