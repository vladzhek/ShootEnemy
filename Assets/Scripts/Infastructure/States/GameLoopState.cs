
using Data;
using Services;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infastructure
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        [Inject] private WindowsService _windowsService;
        [Inject] private EventService _eventService;
        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            InjectService.Instance.Inject(this);
            _sceneLoader.Load("Game", OnLoaded);
            _windowsService.OpenWindow(WindowType.Gameplay);
            
            _eventService.OnVictory += Victory;
            _eventService.OnRestart += Restart;
        }

        private void Restart()
        {
            _windowsService.CloseAllWindows();
            _windowsService.OpenWindow(WindowType.Gameplay);
            SceneManager.LoadScene("Game");
        }

        private void Victory()
        {
            _windowsService.OpenWindow(WindowType.GameOver);
        }

        private void OnLoaded()
        {
            
        }
        
        public void Exit()
        {
            
        }
    }
}