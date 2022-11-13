using System.IO;
using System.Linq;
using System.Reflection;
using RGV.Extensions.Runtime.Infrastructure;
using UnityEditor;

namespace RGV.Extensions.Editor
{
    public static class EditorFolderExtensions
    {
        public static FolderPath FindCurrentFolderFromProjectWindow()
        {
            var lastClickedObject = AssetDatabase.GetAssetPath(Selection.activeObject);
            if(!string.IsNullOrWhiteSpace(lastClickedObject))
                return new FolderPath(lastClickedObject);

            var findActiveFolderPath = typeof(ProjectWindowUtil).GetMethod("GetActiveFolderPath",
                BindingFlags.Static | BindingFlags.NonPublic);
            var currentActiveObject = findActiveFolderPath?.Invoke(null, null).ToString();

            return new FolderPath(currentActiveObject);
        }

        public static bool SafeCleanFolder(string path)
        {
            if(!Directory.EnumerateFileSystemEntries(path).Any())
                return true;

            if(EditorUtility.DisplayDialog("Folder is not empty!!!",
                   "Folder will be cleaned. Do you want to lose all its current data?",
                   "Yes", "No"))
                CleanFolder(path);
            else return false;

            return true;
        }

        public static void CleanFolder(string folderPath)
        {
            if(Directory.Exists(folderPath))
                Directory.Delete(folderPath, true);

            Directory.CreateDirectory(folderPath);
        }
    }
}