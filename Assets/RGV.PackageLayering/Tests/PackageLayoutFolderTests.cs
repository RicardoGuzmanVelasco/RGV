using System;
using FluentAssertions;
using NUnit.Framework;
using RGV.PackageLayering.Runtime;

namespace RGV.Extensions.Tests.Editor
{
    public class PackageLayoutFolderTests
    {
        /// <summary> Regression test for invariant in #SSMNG-62. </summary>
        [Test]
        public void Subfolders_CannotHave_TheSameName()
        {
            var sut = new PackageLayoutFolder("Root").AddChild(new PackageLayoutFolder("Sub1"));

            Action act = () => sut.AddChild(new PackageLayoutFolder("Sub1"));

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Infer_FromEditor()
        {
            new AsmdefFolder(Asmdef.Editor()).Name.Should().Be("Editor");
            new AsmdefFolder(Asmdef.EditorTests()).Name.Should().Be("Editor");
        }

        [Test]
        public void Infer_FromRuntime()
        {
            new AsmdefFolder(Asmdef.Runtime()).Name.Should().Be("Runtime");
            new AsmdefFolder(Asmdef.RuntimeTests()).Name.Should().Be("Runtime");
        }

        [Test]
        public void Path()
        {
            var sut = new PackageLayoutFolder("Subfolder");
            new PackageLayoutFolder("Root/Folder").AddChild(sut);

            sut.RelativePath.ToString().Should().Be("Root/Folder/Subfolder");
        }

        [Test]
        public void Path_OnRoot_SameThanName()
        {
            var sut = new PackageLayoutFolder("Root");

            sut.RelativePath.ToString().Should().Be(sut.Name);
        }

        [Test]
        public void NotIndexed_EndsWithTilde()
        {
            var sut = new PackageLayoutFolder("Whatever")
            {
                IndexIgnored = true
            };

            sut.Name.Should().EndWith("~");
        }

        [Test]
        public void Indexed_ByName()
        {
            var sut = new PackageLayoutFolder("Whatever~");

            sut.IndexIgnored.Should().BeTrue();
        }

        [Test]
        public void GetChildByName()
        {
            var sut = new PackageLayoutFolder("Root")
                .AddChild(new PackageLayoutFolder("Child1"))
                .AddChild(new PackageLayoutFolder("Child2"));

            sut.GetChild("Child1").Should().NotBeNull();
            sut.ToString().Should().NotBe(sut.GetChild("Child2").ToString());
        }

        [Test]
        public void IterateOverChildren_DoesNotReturnsItself()
        {
            var sut = new PackageLayoutFolder("Root");
            sut.AddChild(new PackageLayoutFolder("Child1"));
            sut.AddChild(new PackageLayoutFolder("Child2"));

            sut.GetChildren().Should().NotContain(sut);
        }

        [Test]
        public void IterateOverChildren_ReturnsChildren()
        {
            var sut = new PackageLayoutFolder("Root");
            sut.AddChild(new PackageLayoutFolder("Child1"));
            sut.AddChild(new PackageLayoutFolder("Child2"));

            sut.GetChildren().Should().HaveCount(2);
        }

        [Test]
        public void IterateOverChildren_ReturnsInnerChildren()
        {
            var sut = new PackageLayoutFolder("Root");
            sut.AddChild(new PackageLayoutFolder("Child1")
                .AddChild(new PackageLayoutFolder("Subchild1")));
            sut.AddChild(new PackageLayoutFolder("Child2"));

            sut.GetChildren().Should().HaveCount(3);
        }
    }
}