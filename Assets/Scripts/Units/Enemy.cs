using System;
using Ammo;
using Player;
using UnityEngine;

namespace Units
{
    public class Enemy : MonoBehaviour
    {
        public event Action OnEnemyKilled;
        public event Action OnReachFinish;
        [SerializeField] private ParticleSystem _effectDestroy;
        
        [Header("Settings")] public float speed = 3f;
        public int health = 3;

        private Vector3 _target;
        private PlayerHealth _playerHealth;
        
        private const int TARGET_POS_Y = -10;

        private void OnEnable()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
            
            var position = transform.position;
            _target = new Vector3(position.x, TARGET_POS_Y, position.z);
        }

        private void Update()
        {
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            Vector3 direction = (_target - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, _target) < 0.1f)
            {
                ReachFinishLine();
            }
        }

        private void ReachFinishLine()
        {
            _playerHealth.TakeDamage(1);
            Instantiate(_effectDestroy, transform.position, Quaternion.identity);
            Destroy(gameObject);
            OnReachFinish.Invoke();
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnEnemyKilled?.Invoke();
            Instantiate(_effectDestroy, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void SetNewTarget(Transform pos)
        {
            var position = pos.position;
            _target = new Vector3(_target.x, position.y, _target.z);
        }
    }
}