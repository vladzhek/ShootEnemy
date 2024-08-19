using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "AllWindowData", menuName = "Data/AllWindowData")]
    public class AllWindowData : ScriptableObject
    {
        public List<WindowData> Windows;
    }
}