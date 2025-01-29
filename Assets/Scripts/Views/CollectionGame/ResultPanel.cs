using System.Collections.Generic;
using UnityEngine;
using Views.General;

namespace Views.CollectionGame
{
    public class ResultPanel : PanelView
    {
        [SerializeField] 
        private ProgressView _progressView;

        public void Initialize(List<int> indexes, List<bool> states)
        {
            _progressView.UpdateProgressIcons(indexes, states);
        }
    }
}