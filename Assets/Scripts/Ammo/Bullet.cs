using Units;
using UnityEngine;

namespace Ammo
{
    public class Bullet : MonoBehaviour
    {
        private int _damage;
        private Vector3 _velocity;
        private Rigidbody2D _rb;  
        
        public float lifetime = 5f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, lifetime);
        }

        private void Update()
        {
            _rb.velocity = _velocity;
        }
        
        public void Initialize(int damage, Vector3 velocity)
        {
            _damage = damage;
            _velocity = velocity;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_damage);
                }
                
                Destroy(gameObject);
            }
        }

        public int GetDamage()
        {
            return _damage;
        }
    }
}