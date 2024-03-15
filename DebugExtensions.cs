using UnityEngine;

namespace WinterCrestal.Extensions.Debug
{
    public static class DebugExtensions
    {
        public static GUIStyle defaultStyle = ColoredBoxStyle(new RectOffset(5, 5, 5, 5), Color.black, Color.white);
        public static GUIStyle defaultAltStyle = ColoredBoxStyle(new RectOffset(5, 5, 5, 5), new Color(0, 0, 0, .5f), Color.white);

        public static Texture2D ColorTexture(int width, int height, Color color)
        {
            Color32[] pix = new Color32[width * height];
            for (int i = 0; i < pix.Length; ++i) pix[i] = color;
            Texture2D result = new(width, height);
            result.SetPixels32(pix);
            result.Apply();
            return result;
        }
        public static Texture2D ColorTexture(Color color)
        {
            return ColorTexture(1, 1, color);
        }

        public static GUIStyle ColoredBoxStyle(RectOffset offset, Color boxColor, Color textColor)
        {
            var style = new GUIStyle()
            {
                padding = offset
            };
            style.normal.textColor = textColor;
            style.normal.background = ColorTexture(1, 1, boxColor);
            return style;
        }

        public static Rect GetRectWRTWorldObject(Transform transform, Vector2 size, Vector2 offset)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            return new Rect(screenPosition.x + offset.x, Screen.height - screenPosition.y + offset.y, size.x, size.y);
        }


    }


}

