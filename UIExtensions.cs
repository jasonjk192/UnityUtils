using UnityEngine;
using UnityEngine.UI;

namespace WinterCrestal.Extensions
{
    public static class UIExtensions
    {
        public static void SetPositionLocalToWorldObject(this GameObject uiObject, GameObject worldObject, Vector3? offset = null)
        {
            RectTransform rt = uiObject.GetComponent<RectTransform>();
            rt.position = Camera.main.WorldToScreenPoint(worldObject.transform.position);
            if (offset != null) rt.position += (Vector3)offset;
        }

        public static void SetPositionLocalToWorldObject(this RectTransform rt, GameObject worldObject, Vector3? offset = null)
        {
            rt.position = Camera.main.WorldToScreenPoint(worldObject.transform.position);
            if (offset != null) rt.position += (Vector3)offset;
        }

        public static void SetColor(this Image image, Color color, bool ignoreAlpha = false)
        {
            if (ignoreAlpha)
            {
                color.a = image.color.a;
                image.color = color;
            }
            else
                image.color = color;
        }

        public static void SetFade(this Image image, float fade)
        {
            var c = image.color;
            c.a = fade;
            image.color = c;
        }
    }

}
