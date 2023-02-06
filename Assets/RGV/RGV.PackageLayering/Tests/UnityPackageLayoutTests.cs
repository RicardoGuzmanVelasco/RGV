using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RGV.Extensions.Runtime.Infrastructure;
using RGV.PackageLayering.Runtime;

namespace RGV.Extensions.Tests.Editor
{
    public class UnityPackageLayoutTests
    {
        [Test]
        public void UnityFolderStructure()
        {
            var sut = PackageLayout.Unity(new FolderPath("Root"));

            sut.RootFolder.Name.Should().Be("Root");
            sut.RootFolder.Should().NotBeOfType<AsmdefFolder>();

            sut.RootFolder.Children.Should().HaveCount(4);

            var tests = sut.RootFolder.GetChild("Tests");
            tests.Children.Count(f => f is AsmdefFolder).Should().Be(2);
        }

        [Test]
        public void IterateOverChildrenAsmdefs()
        {
            var sut = PackageLayout.Unity(new FolderPath("Root"));

            var asmdefs = sut.IterateOverChildrenHierarchy().OfType<AsmdefFolder>();
            asmdefs.Should().HaveCount(4);
            asmdefs.Count(f => f.Asmdef.Name.Contains("Editor")).Should().Be(2);
        }

        [Test]
        public void IterateOverChildrenAssemblyInfo()
        {
            var sut = PackageLayout.Unity(new FolderPath("Root"));

            var assemblyInfoFiles = sut.IterateOverChildrenHierarchy().OfType<PropertiesFolder>();
            assemblyInfoFiles.Should().HaveCount(1);
        }

        [Test]
        public void IterateOverRootHierarchy()
        {
            var sut = PackageLayout.Unity(new FolderPath("Root"));

            var expected = sut.IterateOverChildrenHierarchy().Count() + 1;
            sut.IterateOverRootHierarchy().Should().HaveCount(expected);
        }

        [Test]
        public void RuntimeTestsFolder_IncludesAllPath()
        {
            var sut = PackageLayout.Unity(new FolderPath("Root"));

            sut.RootFolder.GetChild("Tests").GetChild("Runtime").RelativePath.Should()
                .ContainInOrder("Tests", "Runtime");
        }

        [Test]
        public void RuntimeTests_References_Runtime()
        {
            var sut = PackageLayout.Unity(new FolderPath("Root"));

            var result = sut.RootFolder.GetChild("Tests").GetChild("Runtime") as AsmdefFolder;
            result.Should().NotBeNull();
            result!.Asmdef.References.Should().Contain("Root");
        }
    }
}