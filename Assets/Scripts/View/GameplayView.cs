using System;
using Infastructure;
using Services;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace View
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hpText;
        [Inject] private EventService _eventService;
        
        private void Start()
        {
            InjectService.Instance.Inject(this);
            _eventService.OnChangeHealth += SetHealth;
        }

        public void SetHealth(int hp)
        {
            _hpText.text = $"Здоровье: {hp}";
        }
    }
}