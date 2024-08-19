using Data;
using Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Settings")]
        public int maxHealth = 5; 
        private int _currentHealth;  

        [Inject] private EventService _eventService;
        [Inject] private WindowsService _windowsService;

        private void Start()
        {
            _currentHealth = maxHealth;
            UpdateHealthUI();
        }
        
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            UpdateHealthUI();

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void UpdateHealthUI()
        {
            _eventService.InvokeChangeHealth(_currentHealth);
        }

        private void Die()
        {
            Debug.Log("Player die");
            GameOver();
        }

        private void GameOver()
        {
            Time.timeScale = 0f;
            _windowsService.OpenWindow(WindowType.GameOver);
        }
        
        public void Heal(int amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            UpdateHealthUI();
        }
    }
}