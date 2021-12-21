using UnityEngine;

namespace Assets.Utils
{
    public static class NewMonoBehaviour1  // ����� 13 (��8)
    {
        public static bool IsInLayer(this GameObject go, LayerMask layer)
        {
            return layer == (layer | 1 << go.layer);
        }

    }
}