using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Services
{
    public class StaticDataService
    {
        public Dictionary<WindowType, WindowData> Windows = new();
        
        public void Load()
        {
            LoadWindows();
        }

        private void LoadWindows()
        {
            var data = Resources.Load<AllWindowData>("Configs/AllWindowData");
            foreach (var window in data.Windows)
            {
                Windows.Add(window.Type, window);
            }
        }
    }
}