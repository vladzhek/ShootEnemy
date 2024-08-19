using System;

namespace Services
{
    public class EventService
    {
        public event Action OnVictory;
        public event Action OnRestart;
        public event Action<int> OnChangeHealth;

        public void InvokeVictory()
        {
            OnVictory?.Invoke();
        }
        
        public void InvokeChangeHealth(int hp)
        {
            OnChangeHealth?.Invoke(hp);
        }
        
        public void InvokeRestart()
        {
            OnRestart?.Invoke();
        }
    }
}