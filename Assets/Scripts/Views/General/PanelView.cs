using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Views.General
{
    public class PanelView : MonoBehaviour
    {
        public Action<int> PressBtnAction { get; set; }

        [SerializeField] 
        private List<Button> _btns;

        private void OnEnable()
        {
            for (int i = 0; i < _btns.Count; i++)
            {
                int index = i;
                
                _btns[i].onClick.AddListener(delegate { NotificationPressBtn(index); });
            }
        }

        private void OnDisable()
        {
            foreach (var btn in _btns)
            {
                btn.onClick.RemoveAllListeners();
            }
        }

        private void NotificationPressBtn(int index)
        {
            PressBtnAction?.Invoke(index);
        }
    }
}