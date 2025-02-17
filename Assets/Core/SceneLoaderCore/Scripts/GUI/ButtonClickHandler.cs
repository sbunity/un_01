using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{

	[RequireComponent ( typeof ( Button ) )]
	public class ButtonClickHandler :MonoBehaviour
	{
		private bool centerPivot = true;

		private Button _button;
		private RectTransform _rectTransform;
		
		protected virtual void Awake ()
		{
			_rectTransform = GetComponent<RectTransform> ( );
			_button = GetComponent<Button> ( );
			_button.onClick.AddListener ( OnButtonClick );
			var anim = GetComponent<Animator> ( );
			if (anim != null)
			{
				anim.updateMode = AnimatorUpdateMode.UnscaledTime;
			}
			if (centerPivot)
			{
				_rectTransform.CenterPivot ( );
			}
		}
		
		protected virtual void OnButtonClick ()
		{

		}
	}
}
