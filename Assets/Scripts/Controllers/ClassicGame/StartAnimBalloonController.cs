using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using UnityEngine;

namespace Controllers.ClassicGame
{
    public class StartAnimBalloonController : MonoBehaviour
    {
        public event Action AnimationEndedAction;
        
        [SerializeField] 
        private List<RectTransform> _leftBalloons;
        [SerializeField] 
        private List<RectTransform> _centralBalloons;
        [SerializeField] 
        private List<RectTransform> _rightBalloons;
        [SerializeField] 
        private RectTransform _targetRect;

        public void StartAnim()
        {
            StartCoroutine(ShowAnim(_leftBalloons, 1));
            StartCoroutine(ShowAnim(_centralBalloons, 1.5f));
            StartCoroutine(ShowAnim(_rightBalloons, 2, true));
        }

        private void OnAnimEnded()
        {
            _leftBalloons.ForEach(x => Destroy(x.gameObject));
            _rightBalloons.ForEach(x => Destroy(x.gameObject));
            _centralBalloons.ForEach(x => Destroy(x.gameObject));
            
            AnimationEndedAction?.Invoke();
        }

        private IEnumerator ShowAnim(List<RectTransform> balloonRects, float speed, bool canSubscribe = false)
        {
            yield return new WaitForSeconds(1);
            
            for (int i = 0; i < balloonRects.Count; i++)
            {
                if (i < balloonRects.Count - 1)
                {
                    balloonRects[i].DOAnchorPosY(balloonRects[i].anchoredPosition.y + 600, speed);
                }
                else
                {
                    if (canSubscribe)
                    {
                        balloonRects[i].DOMoveY(_targetRect.position.y, speed/2).OnComplete(OnAnimEnded);
                    }
                    else
                    {
                        balloonRects[i].DOMoveY(_targetRect.position.y, speed/2);
                    }
                }

                yield return new WaitForSeconds(speed);
            }
        }
    }
}