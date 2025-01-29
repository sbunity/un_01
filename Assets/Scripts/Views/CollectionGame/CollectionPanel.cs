using System;
using System.Collections.Generic;

using UnityEngine;
using Views.General;

namespace Views.CollectionGame
{
    public class CollectionPanel : PanelView
    {
        public event Action<int> PressCollectBtnAction;
        
        [SerializeField] 
        private List<CollectionItemView> _collectionItemViews;

        private void Start()
        {
            _collectionItemViews.ForEach(item => item.PressBtnAction += OnPressCollectBtn);
        }

        private void OnDestroy()
        {
            _collectionItemViews.ForEach(item => item.PressBtnAction -= OnPressCollectBtn);
        }

        public void SetStates(List<bool> states)
        {
            for (int i = 0; i < _collectionItemViews.Count; i++)
            {
                _collectionItemViews[i].SetState(states[i]);
            }
        }

        public void SetCounts(List<int> counts)
        {
            for (int i = 0; i < _collectionItemViews.Count; i++)
            {
                _collectionItemViews[i].SetCount(counts[i]);
            }
        }
        
        public void SetFills(List<float> fills)
        {
            for (int i = 0; i < _collectionItemViews.Count; i++)
            {
                _collectionItemViews[i].SetFillAmount(fills[i]);
            }
        }

        private void OnPressCollectBtn(CollectionItemView item)
        {
            int index = _collectionItemViews.IndexOf(item);
            
            PressCollectBtnAction?.Invoke(index);
        }
    }
}