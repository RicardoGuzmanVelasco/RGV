using RGV.Extensions.Runtime.Infrastructure;
using RGV.PackageLayering.Runtime;

namespace RGV.PackageLayering.Editor
{
    public class PackageLayoutCreator
    {
        public PackageLayoutCreator(FolderPath rootPath)
        {
            RootFolder = new PackageLayoutFolderPath(rootPath);
        }

        PackageLayoutFolderPath RootFolder { get; }

        public void CreateHierarchy(PackageLayout layout)
        {
            foreach(var folder in layout.IterateOverChildrenHierarchy())
                ProcessFolder(folder);
        }

        void ProcessFolder(PackageLayoutFolder folder)
        {
            var subFolder = RootFolder.CreateSubfolder(folder.RelativePath);

            switch(folder)
            {
                case AsmdefFolder f:
                    subFolder.CreateAsmdef(f.Asmdef);
                    break;
                case PropertiesFolder f:
                    subFolder.CreateAssemblyInfo(f.File);
                    break;
            }
        }
    }
}