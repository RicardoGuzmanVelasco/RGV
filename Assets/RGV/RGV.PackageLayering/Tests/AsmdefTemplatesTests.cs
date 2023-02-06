using FluentAssertions;
using NUnit.Framework;
using RGV.PackageLayering.Runtime;

namespace RGV.PackageLayering.Tests
{
    public class AsmdefMergingTests
    {
        [Test]
        public void Merging_PlatformsAndReferences()
        {
            var sut = Asmdef.Editor().MergeWith(Asmdef.Tests());

            sut.IncludePlatforms.Should().BeEquivalentTo(Asmdef.Editor().IncludePlatforms);
            sut.References.Should().BeEquivalentTo(Asmdef.Tests().References);
            sut.DllReferences.Should().BeEquivalentTo(Asmdef.Tests().DllReferences);
            sut.OverridesDlls.Should().Be(Asmdef.Tests().OverridesDlls);
        }

        [Test]
        public void References_NotDuplicated()
        {
            var doc = new Asmdef("doc");
            var sut = new Asmdef().DependsOn(doc);

            sut.MergeWith(new Asmdef().DependsOn(doc)).References.Should().HaveCount(1);
        }

        [Test]
        public void Merging_Constraints()
        {
            new Asmdef().MergeWith(Asmdef.Tests()).DefineConstraints.Should().NotBeEmpty();
            Asmdef.Tests().MergeWith(new Asmdef()).DefineConstraints.Should().NotBeEmpty();
        }

        [Test]
        public void EditorTestsAsmdef_IsSameThanEditorMergedWithTests()
        {
            Asmdef.EditorTests().Should().BeEquivalentTo(Asmdef.Editor().MergeWith(Asmdef.Tests()));
        }

        [Test]
        public void RuntimeTestsAsmdef_NotSameThan_EditorTestsAsmdef()
        {
            Asmdef.RuntimeTests().Should().NotBeEquivalentTo(Asmdef.EditorTests());
        }

        [Test]
        public void TemplatesWithNames_UseContextsAsNamePrefixes()
        {
            Asmdef.Editor("Context").Name.Should().Be("Context.Editor");
            Asmdef.EditorTests("Context").Name.Should().Be("Context.Editor.Tests");

            Asmdef.Runtime("Context").Name.Should().Be("Context");
            Asmdef.RuntimeTests("Context").Name.Should().Be("Context.Tests");
        }

        [Test]
        public void TemplatesWithoutNames_HaveNotPrefixes()
        {
            Asmdef.Editor().Name.Should().Be("Editor");
            Asmdef.EditorTests().Name.Should().Be("Editor.Tests");

            Asmdef.Runtime().Name.Should().BeEmpty();
            Asmdef.RuntimeTests().Name.Should().Be("Tests");
        }
    }
}