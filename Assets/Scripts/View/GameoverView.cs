using System;
using Infastructure;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View
{
    public class GameoverView : MonoBehaviour
    {
        [SerializeField] private Button _restartBtn;

        [Inject] private EventService _eventService;

        private void OnEnable()
        {
            _restartBtn.onClick.AddListener(RestartClick);
        }

        private void OnDisable()
        {
            _restartBtn.onClick.RemoveListener(RestartClick);
        }

        private void Start()
        {
            InjectService.Instance.Inject(this);
        }
        
        private void RestartClick()
        {
            _eventService.InvokeRestart();
        }
    }
}