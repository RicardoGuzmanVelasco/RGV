using FluentAssertions;
using NUnit.Framework;
using RGV.PackageLayering.Runtime;

namespace RGV.PackageLayering.Tests
{
    public class AsmdefReferencesTests
    {
        [Test]
        public void TestsAsmdef_ReferencesTestDlls()
        {
            Asmdef.Tests().DllReferences.Should()
                .Contain("nunit.framework.dll")
                .And
                .Contain("NSubstitute.dll");
        }

        [Test]
        public void TestsAsmdef_OverrideDlls()
        {
            Asmdef.Tests().OverridesDlls.Should().BeTrue();
        }

        [Test]
        public void TestsAsmdef_ReferencesTestModules()
        {
            Asmdef.Tests().References.Should()
                .Contain("UnityEngine.TestRunner")
                .And
                .Contain("BoundfoxStudios.FluentAssertions");
        }

        [Test]
        public void TestsAsmdef_ConstraintsByUnityMacro()
        {
            Asmdef.Tests().DefineConstraints.Should().Contain("UNITY_INCLUDE_TESTS");
        }

        [Test]
        public void Asmdef_DependsOn_OtherAsmdef()
        {
            var sut = new Asmdef();
            sut.DependsOn(new Asmdef("DOC"));

            sut.References.Should().HaveCount(1).And.Contain("DOC");
        }
    }
}