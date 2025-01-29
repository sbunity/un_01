using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.CollectionGame
{
    public class ProgressView : MonoBehaviour
    {
        [SerializeField] 
        private List<Sprite> _disableSprites;
        [SerializeField] 
        private List<Sprite> _activeSprites;
        [SerializeField] 
        private List<Image> _images;

        public void UpdateProgressIcons(List<int> indexes, List<bool> states)
        {
            for (int i = 0; i < _images.Count; i++)
            {
                _images[i].sprite = states[i] ? _activeSprites[indexes[i]] : _disableSprites[indexes[i]];
            }
        }
    }
}