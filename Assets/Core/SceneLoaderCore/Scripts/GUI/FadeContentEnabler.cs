using UnityEngine;

namespace Prototype.SceneLoaderCore
{
    public class FadeContentEnabler : MonoBehaviour
    {
        private GameObject[] _children;

        private void Awake()
        {
            _children = new GameObject[transform.childCount];
            for (var i = 0; i < _children.Length; i++)
            {
                _children[i] = transform.GetChild(i).gameObject;
            }
        }

        private void OnEnable()
        {
            EnableChildren(false);
        }

        private void EnableChildren(bool param)
        {
            foreach (var t in _children)
            {
                t.SetActive(param);
            }
        }
    }
}
