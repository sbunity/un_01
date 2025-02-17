using DG.Tweening;
using UnityEngine;

namespace Core
{
    public class CirclePreloder : Preloader
    {
        [SerializeField, Range(1.0f, 100.0f), Header("Angle")] private float angle = 100f;
        
        [SerializeField, Range(1.0f, 10.0f), Header("Speed")] private float speed = 3f;

        protected override void Play()
        {
            _tween = progress.DORotate(Vector3.back * angle, speed)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear).Play();
        }
    }
}
