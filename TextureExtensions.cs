using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace WinterCrestal.Extensions
{
    public static class TextureExtensions
    {
        public enum AverageColorTechnique { MEAN, MEDIAN, MODE }

        public static void Blit(this Texture2D tex2D, RenderTexture rTex)
        {
            var previousRT = RenderTexture.active;
            RenderTexture.active = rTex;
            Graphics.Blit(tex2D, rTex);
            RenderTexture.active = previousRT;
        }

        public static Color GetAverageColor(this Texture2D tex2D, AverageColorTechnique technique = AverageColorTechnique.MEAN)
        {
            if (tex2D.isReadable)
            {
                var colors = tex2D.GetPixels();
                switch (technique)
                {
                    case AverageColorTechnique.MEDIAN: return MedianAverageColor(colors);
                    case AverageColorTechnique.MODE: return ModeAverageColor(colors);
                    default: return MeanAverageColor(colors);
                }
            }
            else
            {
                var rt = ToRenderTexture(tex2D);
                var tmp = ToTexture2D(rt);
                var colors = tmp.GetPixels();
                rt.Release();
                switch (technique)
                {
                    case AverageColorTechnique.MEDIAN: return MedianAverageColor(colors);
                    case AverageColorTechnique.MODE: return ModeAverageColor(colors);
                    default: return MeanAverageColor(colors);
                }
            }
        }

        public static Sprite ToSprite(this Texture2D tex2D)
        {
            return Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public static Sprite ToSprite(this RenderTexture rTex)
        {
            Texture2D tex2D = new(rTex.width, rTex.height, TextureFormat.RGBA32, false);
            RenderTexture.active = rTex;
            tex2D.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex2D.Apply();
            RenderTexture.active = null;
            return Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public static RenderTexture ToRenderTexture(this Texture2D tex2D, Vector2Int? size = null)
        {
            int w = tex2D.width;
            int h = tex2D.height;
            if (size != null)
            {
                w = (int)(size?.x);
                h = (int)(size?.y);
            }
            var previousRT = RenderTexture.active;
            RenderTexture renderTexture = new(w, h, 0);
            RenderTexture.active = renderTexture;
            Graphics.Blit(tex2D, renderTexture);
            RenderTexture.active = previousRT;
            return renderTexture;
        }

        public static Texture2D ToTexture2D(this RenderTexture rTex, Vector2Int? size = null)
        {
            int w = rTex.width;
            int h = rTex.height;
            if (size != null)
            {
                w = (int)(size?.x);
                h = (int)(size?.y);
            }
            var previousRT = RenderTexture.active;
            Texture2D tex = new(w, h, TextureFormat.RGBA32, false);
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            RenderTexture.active = previousRT;
            return tex;
        }

        private static Color MeanAverageColor(Color[] colors)
        {
            int totalPixels = colors.Length;
            float rSum = 0f, gSum = 0f, bSum = 0f;

            foreach (var pixel in colors)
            {
                rSum += pixel.r;
                gSum += pixel.g;
                bSum += pixel.b;
            }

            return new Color(rSum / totalPixels, gSum / totalPixels, bSum / totalPixels, 1f);
        }

        private static Color MedianAverageColor(Color[] colors)
        {
            Array.Sort(colors, (a, b) => (int)(a.grayscale - b.grayscale));

            int middleIndex = colors.Length / 2;
            return colors[middleIndex];
        }

        private static Color ModeAverageColor(Color[] colors)
        {
            Dictionary<Color, int> colorCounts = new();

            foreach (var pixel in colors)
            {
                if (colorCounts.ContainsKey(pixel))
                    colorCounts[pixel]++;
                else
                    colorCounts[pixel] = 1;
            }

            return colorCounts.OrderByDescending(kv => kv.Value).First().Key;
        }
    }

}
