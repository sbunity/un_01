using UnityEngine;
using DG.Tweening;
using Prototype.SceneLoaderCore.Helpers;

namespace Prototype
{
    public class LogoScript : MonoBehaviour
    {
        private float time = 1;

        private void Start()
        {
            DOVirtual.DelayedCall(time, () => SceneLoader.Instance.SwitchToScene(SceneLoader.Instance.mainScene)).Play();
        }
    }
}
