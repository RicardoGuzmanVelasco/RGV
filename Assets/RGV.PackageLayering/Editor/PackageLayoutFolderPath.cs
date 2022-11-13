using System.IO;
using JetBrains.Annotations;
using RGV.Extensions.Runtime.Infrastructure;
using RGV.PackageLayering.Runtime;
using UnityEngine.Assertions;

namespace RGV.PackageLayering.Editor
{
    public class PackageLayoutFolderPath : FolderPath
    {
        public PackageLayoutFolderPath(FolderPath rootPath) : base(rootPath.ToString()) { }

        public void CreateAsmdef([NotNull] Asmdef asmdef)
        {
            CreateFile(asmdef.Name + ".asmdef", asmdef.ToString());
        }

        public void CreateAssemblyInfo(AssemblyInfoFile file)
        {
            CreateFile("AssemblyInfo.cs", file.ToString());
        }

        void CreateFile(string fileName, string content)
        {
            var filePath = base.Concat(fileName);
            File.WriteAllText(filePath.ToString(), content);
        }

        public PackageLayoutFolderPath CreateSubfolder(FolderPath subfolder)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(subfolder.ToString()));

            var path = Path.Combine(ToString(), subfolder.ToString());
            Directory.CreateDirectory(path);

            return new PackageLayoutFolderPath(new FolderPath(path));
        }
    }
}