using System.Linq;
using Ammo;
using Infastructure;
using Infastructure.Services;
using Services;
using Units;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")] public float speed = 5f;
        public float minX, maxX, minY, maxY;

        [Header("Combat Settings")] public float attackRadius = 10f;
        public float fireRate = 1f;
        public int damage = 1;
        public GameObject bulletPrefab; 
        public Transform bulletSpawnPoint;
        public float bulletSpeed = 10f;
        
        [SerializeField] private ParticleSystem _effectShoot;

        private float nextFireTime;
        private IInputService _inputService;

        private void Start()
        {
            _inputService = Game.InputService;
        }

        private void Update()
        {
            HandleMovement();
            HandleShooting();
        }

        private void HandleMovement()
        {
            Vector2 input = _inputService.Axis;
            Vector3 direction = new Vector3(input.x, input.y, 0f);
            
            transform.position += direction * speed * Time.deltaTime;
            
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
            transform.position = clampedPosition;
        }

        private void HandleShooting()
        {
            if (Time.time >= nextFireTime)
            {
                Enemy nearestEnemy = FindNearestEnemy();

                if (nearestEnemy != null)
                {
                    Shoot(nearestEnemy.transform.position);
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }

        private Enemy FindNearestEnemy()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            
            return enemies
                .Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= attackRadius)
                .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
                .FirstOrDefault();
        }

        private void Shoot(Vector3 targetPosition)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Vector3 direction = (targetPosition - bulletSpawnPoint.position).normalized;
            bullet.GetComponent<Bullet>().Initialize(damage, direction * bulletSpeed);
            Instantiate(_effectShoot, bulletSpawnPoint.position, Quaternion.identity);
        }
    }
}