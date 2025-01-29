using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Views.CollectionGame;
using Models.CollectionGame;

namespace Controllers.CollectionGame
{
    public class SpawnBaloonController : MonoBehaviour
    {
        public event Action<int> PressBaloonAction;
        public event Action AllBaloonsFinishedMoveAction; 

        [SerializeField] 
        private GameObject _baloonPrefab;
        [SerializeField] 
        private List<Sprite> _baloonSprites;
        [SerializeField] 
        private List<Sprite> _splatterSprites;
        [SerializeField] 
        private List<Sprite> _iconSprites;
        [SerializeField] 
        private List<RectTransform> _startPosints;

        private SpawnModel _model;
        private List<BaloonView> _baloonViews;
        private List<Sprite> _activeIconSprites;

        public void Initialize(List<int> indexes)
        {
            _model = new SpawnModel(indexes, _baloonSprites.Count ,_startPosints.Count);

            _baloonViews = new List<BaloonView>();
            _activeIconSprites = new List<Sprite>();
        }

        public void StartGame()
        {
            StartCoroutine(StartSpawn());
        }

        private void SpawnBaloon()
        {
            GameObject baloon = Instantiate(_baloonPrefab, transform);

            BaloonView baloonView = baloon.GetComponent<BaloonView>();

            baloonView.PressAction += OnPressBaloon;
            baloonView.EndMoveAction += DestroyBaloon;
            
            baloonView.SetSize(_model.GetRandomSize());

            int iconIndex = _model.GetIconIndex();
            Sprite iconSprite = iconIndex >= 0 ? _iconSprites[iconIndex] : null;
            
            baloonView.SetIcon(iconSprite);
            
            _activeIconSprites.Add(iconSprite);

            int baloonIndex = _model.GetBaloonSpriteIndex();
            
            baloonView.SetSprite(_baloonSprites[baloonIndex]);
            baloonView.SetSplatter(_splatterSprites[baloonIndex]);

            RectTransform baloonRect = baloon.GetComponent<RectTransform>();

            baloonRect.anchoredPosition = _startPosints[_model.GetPositionIndex()].anchoredPosition;

            _baloonViews.Add(baloonView);

            baloonView.Move();
        }

        private void OnPressBaloon(BaloonView baloonView)
        {
            int baloonIndex = _baloonViews.IndexOf(baloonView);

            int index = _iconSprites.IndexOf(_activeIconSprites[baloonIndex]);
            
            DestroyBaloon(baloonView);
            
            PressBaloonAction?.Invoke(index);
        }

        private void DestroyBaloon(BaloonView baloonView)
        {
            baloonView.PressAction -= OnPressBaloon;
            baloonView.EndMoveAction -= DestroyBaloon;
            
            int index = _baloonViews.IndexOf(baloonView);
            
            Destroy(_baloonViews[index].gameObject);
            
            _model.UpdateFinishBallonsCount();

            if (_model.IsEndGame)
            {
                AllBaloonsFinishedMoveAction?.Invoke();
            }
        }

        private IEnumerator StartSpawn()
        {
            while (_model.CanSpawnBaloon)
            {
                SpawnBaloon();

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}