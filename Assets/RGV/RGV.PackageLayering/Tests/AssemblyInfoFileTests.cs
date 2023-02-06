using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RGV.PackageLayering.Runtime;

namespace RGV.PackageLayering.Tests
{
    public class AssemblyInfoFileTests
    {
        [Test]
        public void DefaultVersion()
        {
            var sut = new AssemblyInfoFile();
            sut.ToString().Should().Contain("0.1");
        }

        [Test]
        public void NameFormat_ColonIsSeparator()
        {
            var sut = new AssemblyInfoFile("Comp.Prod.Tit");

            sut.ToString().Should().ContainAll("Comp", "Prod", "Tit");
        }

        [Test]
        public void AddFriend()
        {
            var sut = new AssemblyInfoFile();
            sut.AddFriend("Friend");
            sut.Friends.Should().Contain("Friend").And.HaveCount(1);
        }

        [Test]
        public void AppendFriend()
        {
            var sut = new AssemblyInfoFile();
            sut.AppendFriend("Friend");
            sut.Friends.Should().Contain(sut.Title + ".Friend").And.HaveCount(1);
        }

        [Test]
        public void AddingFriend_DiffersFromAppending_InTitlePrefix()
        {
            var sut = new AssemblyInfoFile();
            sut.AddFriend("Friend");
            sut.AppendFriend("Friend");

            sut.Friends.Last().Should()
                .EndWithEquivalent(sut.Friends.First())
                .And
                .StartWithEquivalent(sut.Title);
        }

        [Test]
        public void Friends_EmptyByDefault()
        {
            new AssemblyInfoFile().Friends.Should().BeEmpty();
        }

        [Test]
        public void Friendship_IncludedAsInternalVisibility()
        {
            var sut = new AssemblyInfoFile();
            sut.AddFriend("Friend");

            sut.ToString().Should().Contain("Friend");
        }

        [Test]
        public void Include_Usings()
        {
            var sut = new AssemblyInfoFile();

            sut.ToString().Should().Contain("using System.Reflection;");
            sut.ToString().Should().Contain("using System.Runtime.CompilerServices;");
        }
    }
}