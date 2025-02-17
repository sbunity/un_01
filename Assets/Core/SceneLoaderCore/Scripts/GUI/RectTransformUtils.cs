using UnityEngine;

namespace Prototype
{
	public static class RectTransformUtils
	{
		internal static void CenterPivot (this RectTransform rt)
		{
			var oldPivot = rt.pivot;
			var newPivot = Vector2.one * 0.5f;
			rt.pivot = newPivot;
			var delta = newPivot - oldPivot;
			rt.anchoredPosition += MultiplyV2 ( new Vector2 [] { delta, rt.sizeDelta, rt.localScale } );
		}

		private static Vector2 MultiplyV2 (Vector2 [] items)
		{
			var res = Vector2.one;
			if (items != null)
			{
				for (var i = 0; i < items.Length; i++)
				{
					res.x *= items [i].x;
					res.y *= items [i].y;
				}
			}
			return res;
		}
	}
}
