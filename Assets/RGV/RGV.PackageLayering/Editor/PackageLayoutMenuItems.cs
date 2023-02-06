using RGV.Extensions.Runtime.Infrastructure;
using RGV.PackageLayering.Runtime;
using UnityEditor;
using UnityEngine;
using static RGV.Extensions.Editor.EditorExtensions;
using static RGV.Extensions.Editor.EditorFolderExtensions;

namespace RGV.PackageLayering.Editor
{
    internal static class PackageLayoutMenuItems
    {
        const string Menu = "Assets/Create/RGV/Package Layout/";
        const string SubMenuCreate = Menu + "Create layout/";

        static FolderPath CurrentFolder => FindCurrentFolderFromProjectWindow();

        [MenuItem(Menu + "Clean folder", false, 0)]
        public static void Clean()
        {
            CleanFolder(CurrentFolder.ToString());
            Recompile();

            Debug.Log($"Cleaned folder {CurrentFolder}");
        }

        [MenuItem(SubMenuCreate + "Unity Package Layout", false, 0)]
        public static void CreateUnity()
        {
            var selectedLayout = PackageLayout.Unity(CurrentFolder);
            Create(selectedLayout);
        }

        [MenuItem(SubMenuCreate + "Minimum", false, 0)]
        public static void CreateMin()
        {
            var selectedLayout = PackageLayout.Min(CurrentFolder);
            Create(selectedLayout);
        }

        [MenuItem(SubMenuCreate + "Two layered (Domain <- Infrastructure)", false, 0)]
        public static void CreateTwoLayered()
        {
            var selectedLayout = PackageLayout.TwoLayered(CurrentFolder);
            Create(selectedLayout);
        }

        static void Create(PackageLayout selectedLayout)
        {
            if(!SafeCleanFolder(CurrentFolder.ToString()))
                return;

            new PackageLayoutCreator(CurrentFolder.Parent).CreateHierarchy(selectedLayout);
            Recompile();

            Debug.Log($"Created Unity package layout from root {CurrentFolder}");
        }
    }
}