using FluentAssertions;
using NUnit.Framework;
using RGV.PackageLayering.Runtime;

namespace RGV.PackageLayering.Tests
{
    public class AsmdefPlatformsTests
    {
        [Test]
        public void EditorAsmdef_JustEditorPlatform()
        {
            var sut = Asmdef.Editor();

            sut.IncludePlatforms.Should().Contain("Editor").And.HaveCount(1);
            sut.ExcludePlatforms.Should().BeEmpty();
        }

        [Test]
        public void NotEditorAsmdef_WithExcludedPlatform_JustHasExcludedPlatforms()
        {
            var sut = new Asmdef();
            sut.Exclude("Whatever");

            sut.IncludePlatforms.Should().BeEmpty();
            sut.ExcludePlatforms.Should().ContainSingle("Whatever");
        }

        [Test]
        public void NotEditorAsmdef_WithIncludedPlatform_ThenJustHasIncludedPlatforms()
        {
            var sut = new Asmdef();
            sut.Include("Whatever");

            sut.IncludePlatforms.Should().ContainSingle("Whatever");
            sut.ExcludePlatforms.Should().BeEmpty();
        }

        [Test]
        public void RuntimeAsmdef_HasAnyPlatform()
        {
            var sut = Asmdef.Runtime();

            sut.IncludePlatforms.Should().BeEmpty();
            sut.ExcludePlatforms.Should().BeEmpty();
        }
    }
}