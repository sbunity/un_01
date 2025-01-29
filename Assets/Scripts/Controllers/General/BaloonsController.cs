using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.General
{
    public class BaloonsController : MonoBehaviour
    {
        [SerializeField] 
        private Canvas _canvas;
        [SerializeField] 
        private List<VerticalMovementAnimController> _horizontalBaloonsAnimControllers;

        private void OnEnable()
        {
            float screenHalfHeight = _canvas.GetComponent<CanvasScaler>().referenceResolution.y / 2;
            
            _horizontalBaloonsAnimControllers.ForEach(x => x.SetCanvasWidth(screenHalfHeight));
        }

        private void Start()
        {
            _horizontalBaloonsAnimControllers.ForEach(x => x.StartMoving());
        }
    }
}