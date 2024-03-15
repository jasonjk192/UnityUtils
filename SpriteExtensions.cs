using UnityEngine;
using static WinterCrestal.Extensions.TextureExtensions;

namespace WinterCrestal.Extensions
{
    public static class SpriteExtensions
    {
        public static void SetFade(this SpriteRenderer renderer, float fade)
        {
            Color color = renderer.color;
            color.a = fade;
            renderer.color = color;
        }

        public static Color GetAverageColor(this Sprite sprite, AverageColorTechnique technique = AverageColorTechnique.MEAN)
        {
            return sprite.texture.GetAverageColor(technique);
        }
    }
}