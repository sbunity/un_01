using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.General
{
    public class CloudsController : MonoBehaviour
    {
        [SerializeField] 
        private Canvas _canvas;
        [SerializeField] 
        private List<HorizontalMovementAnimController> _horizontalCloudAnimControllers;

        private void OnEnable()
        {
            float screenHalfWidth = _canvas.GetComponent<CanvasScaler>().referenceResolution.x / 2;
            
            _horizontalCloudAnimControllers.ForEach(x => x.SetCanvasWidth(screenHalfWidth));
        }

        private void Start()
        {
            _horizontalCloudAnimControllers.ForEach(x => x.StartMoving());
        }
    }
}