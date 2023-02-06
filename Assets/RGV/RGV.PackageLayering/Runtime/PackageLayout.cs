using System.Collections.Generic;
using RGV.Extensions.Runtime.Infrastructure;
using static RGV.PackageLayering.Runtime.AsmdefConstants;

namespace RGV.PackageLayering.Runtime
{
    public class PackageLayout
    {
        PackageLayout(PackageLayoutFolder rootFolder)
        {
            RootFolder = rootFolder;
        }

        public PackageLayoutFolder RootFolder { get; }

        public static PackageLayout Min(FolderPath rootPath)
        {
            var root = rootPath.Leaf;

            var runtime = new AsmdefFolder(Asmdef.Runtime(root));
            runtime.AddChild(new PropertiesFolder(root));

            var rootFolder = new PackageLayoutFolder(root)
                .AddChild(runtime)
                .AddChild(new AsmdefFolder(Asmdef.Tests(root, runtime.Asmdef.Name), TestsFolder))
                .AddChild(PackageLayoutFolder.Docs());

            return new PackageLayout(rootFolder);
        }

        public static PackageLayout Unity(FolderPath rootPath)
        {
            var root = rootPath.Leaf;
            var rootFolder = new PackageLayoutFolder(root)
                .AddChild(new PackageLayoutFolder(TestsFolder)
                    .AddChild(new AsmdefFolder(Asmdef.EditorTests(root)))
                    .AddChild(new AsmdefFolder(Asmdef.RuntimeTests(root))))
                .AddChild(new AsmdefFolder(Asmdef.Editor(root)))
                .AddChild(new AsmdefFolder(Asmdef.Runtime(root))
                    .AddChild(new PropertiesFolder(root)))
                .AddChild(PackageLayoutFolder.Docs());

            return new PackageLayout(rootFolder);
        }

        public static PackageLayout TwoLayered(FolderPath rootPath)
        {
            var root = rootPath.Leaf;

            var domain = AsmdefFolder.Production(root, Domain);
            var infrastructure = AsmdefFolder.Production(root, Infrastructure);
            infrastructure.Asmdef.DependsOn(domain.Asmdef);

            var rootFolder = new PackageLayoutFolder(root)
                .AddChild(new PackageLayoutFolder(RuntimeFolder)
                    .AddChild(domain)
                    .AddChild(infrastructure))
                .AddChild(new PackageLayoutFolder(TestsFolder)
                    .AddChild(new AsmdefFolder(Asmdef.EditorTests(root).DependsOn(domain.Asmdef)))
                    .AddChild(new AsmdefFolder(Asmdef.RuntimeTests(root).DependsOn(infrastructure.Asmdef))))
                .AddChild(new AsmdefFolder(Asmdef.Editor(root)))
                .AddChild(PackageLayoutFolder.Docs());

            return new PackageLayout(rootFolder);
        }

        public IEnumerable<PackageLayoutFolder> IterateOverRootHierarchy()
        {
            yield return RootFolder;
            foreach(var folder in IterateOverChildrenHierarchy())
                yield return folder;
        }

        public IEnumerable<PackageLayoutFolder> IterateOverChildrenHierarchy()
        {
            return RootFolder.GetChildren();
        }
    }
}