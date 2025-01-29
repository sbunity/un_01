using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Models.CollectGame;
using Views.CollectGame;

namespace Controllers.CollectGame
{
    public class SpawnController : MonoBehaviour
    {
        public event Action<int> PressBaloonAction;

        [SerializeField] 
        private GameObject _baloonPrefab;
        [SerializeField] 
        private List<Sprite> _baloonSprites;
        [SerializeField] 
        private List<Sprite> _splatterSprites;
        [SerializeField] 
        private List<RectTransform> _startPosints;
        [SerializeField]
        private List<int> _prizes;
        
        private List<BaloonView> _baloonViews;
        private List<int> _activePrizes;
        private SpawnModel _model;
        
        public void Initialize()
        {
            _model = new SpawnModel(_prizes, _startPosints.Count, _baloonSprites.Count);

            _baloonViews = new List<BaloonView>();
            _activePrizes = new List<int>();
        }

        public void StartGame()
        {
            _model.CanSpawn = true;
            StartCoroutine(StartSpawn());
        }

        public void EndGame()
        {
            _model.CanSpawn = false;
            StopAllCoroutines();
        }

        private void SpawnBaloon()
        {
            GameObject baloon = Instantiate(_baloonPrefab, transform);

            BaloonView baloonView = baloon.GetComponent<BaloonView>();

            baloonView.PressAction += OnPressBaloon;
            baloonView.EndMoveAction += DestroyBaloon;
            
            baloonView.SetSize(_model.GetRandomSize());

            int prize = _model.GetRandomPrize();
            
            _activePrizes.Add(prize);
            
            baloonView.SetPrize(prize);

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

            int prizeIndex = _prizes.IndexOf(_activePrizes[baloonIndex]);
            
            DestroyBaloon(baloonView);
            
            PressBaloonAction?.Invoke(_prizes[prizeIndex]);
        }

        private void DestroyBaloon(BaloonView baloonView)
        {
            baloonView.PressAction -= OnPressBaloon;
            baloonView.EndMoveAction -= DestroyBaloon;
            
            int index = _baloonViews.IndexOf(baloonView);
            
            Destroy(_baloonViews[index].gameObject);
        }

        private IEnumerator StartSpawn()
        {
            while (_model.CanSpawn)
            {
                SpawnBaloon();

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}