using UnityEngine;

namespace RGV.Extensions.Runtime.Infrastructure
{
    public static class UnityVectorExtensions
    {
        #region With
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        public static Vector3Int With(this Vector3Int vector, int? x = null, int? y = null, int? z = null)
        {
            return new Vector3Int(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(x ?? vector.x, y ?? vector.y);
        }

        public static Vector2Int With(this Vector2Int vector, int? x = null, int? y = null)
        {
            return new Vector2Int(x ?? vector.x, y ?? vector.y);
        }

        public static Color With(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
        }
        #endregion

        #region Arithmetic
        public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
        }

        public static Vector3Int Add(this Vector3Int vector, int? x = null, int? y = null, int? z = null)
        {
            return new Vector3Int(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
        }

        public static Vector2 Add(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(vector.x + (x ?? 0), vector.y + (y ?? 0));
        }

        public static Vector2Int Add(this Vector2Int vector, int? x = null, int? y = null)
        {
            return new Vector2Int(vector.x + (x ?? 0), vector.y + (y ?? 0));
        }
        #endregion
    }
}