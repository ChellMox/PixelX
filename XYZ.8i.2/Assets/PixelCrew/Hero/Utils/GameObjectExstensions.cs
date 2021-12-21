using UnityEngine;

namespace Assets.Utils
{
    public static class NewMonoBehaviour1  // סענטל 13 (הח8)
    {
        public static bool IsInLayer(this GameObject go, LayerMask layer)
        {
            return layer == (layer | 1 << go.layer);
        }

    }
}