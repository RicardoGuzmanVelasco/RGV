using UnityEditor;

namespace RGV.Extensions.Editor
{
    public static class EditorExtensions
    {
        public static void Recompile()
        {
            AssetDatabase.Refresh();
        }
    }
}