using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using RGV.Extensions.Runtime.Infrastructure;

namespace RGV.Extensions.Tests.Editor
{
    public class FolderPathTests
    {
        [Test]
        public void CannotBeEmpty()
        {
            Action act = () => new FolderPath("");
            act.Should().Throw<Exception>();
        }

        [Test]
        public void HasRoot()
        {
            var path = new FolderPath("Root/Folder");
            path.Root.Should().Be("Root");
        }

        [Test]
        public void HasLeaf()
        {
            var path = new FolderPath("Root/Folder");
            path.Leaf.Should().Be("Folder");
        }

        [Test]
        public void StartsWithRoot_EndsWithLeaf()
        {
            var sut = new FolderPath("Root/Folder");

            sut.ToString().Should().StartWith("Root");
            sut.ToString().Should().EndWith("Folder");
        }

        [Test]
        public void JustAFolder_RootIsAllPath()
        {
            var path = new FolderPath("Folder");
            path.Root.Should().Be(path.ToString());
        }

        [Test]
        public void JustAFolder_RootAndLeafAreTheSame()
        {
            var path = new FolderPath("Folder");
            path.Root.Should().Be(path.Leaf);
        }

        [Test]
        public void ToString_IncludesAllPath()
        {
            var path = new FolderPath("Root/Folder/SubFolder");
            path.ToString().Should().Be("Root/Folder/SubFolder");
        }

        [Test]
        public void Create_FromSplitPaths()
        {
            var path = new FolderPath("Root", "Folder", "SubFolder");
            path.ToString().Should().Be("Root/Folder/SubFolder");
        }

        [Test]
        public void Concat()
        {
            var path = new FolderPath("Root/Folder");
            var concat = path.Concat("SubFolder");
            concat.ToString().Should().Be("Root/Folder/SubFolder");
        }

        [Test]
        public void ParentHierarchy_ContainsAllButLeaf()
        {
            var path = new FolderPath("Root/Folder/SubFolder");
            path.Parent.Should().BeEquivalentTo("Root", "Folder");
        }

        [Test]
        public void ParentHierarchy_OfJustAFolder_IsEmpty()
        {
            var path = new FolderPath("Folder");
            path.Parent.Should().BeEmpty();
        }

        [Test]
        public void Empty()
        {
            FolderPath.Empty.Should().BeEmpty();
        }

        [Test]
        public void Hierarchy_IncludesRootAndLeaf()
        {
            var path = new FolderPath("Root/Folder/SubFolder");

            IEnumerable<string> result = path;
            result.Should().BeEquivalentTo("Root", "Folder", "SubFolder");
        }

        [Test]
        public void SplitSeparator_InAllPaths()
        {
            var path = new FolderPath("Root/SubRoot", "Folder/SubFolder");

            path.Root.Should().Be("Root");
            path.Leaf.Should().Be("SubFolder");
        }

        [TestCase("Root", 0)]
        [TestCase("Root/Folder", 1)]
        [TestCase("Root/Folder/Subfolder", 2)]
        public void DeepFromRoot(string path, int expected)
        {
            var sut = new FolderPath(path);
            sut.Count.Should().Be(expected);
        }

        [Test]
        public void Ignores_Separator_WhenItsFirstCharacter()
        {
            var path = new FolderPath("/Root/Folder");
            path.Root.Should().Be("Root");
            path.Leaf.Should().Be("Folder");
        }

        [TestCase("Root/Folder", "Root")]
        [TestCase("Root/Folder/Subfolder", "Root/Folder")]
        public void Parent(string path, string expected)
        {
            var sut = new FolderPath(path);
            sut.Parent.ToString().Should().Be(expected);
        }

        [Test]
        public void IterateOverPath()
        {
            var path = new FolderPath("A/B/C/D");
            IEnumerable<string> result = path;
            result.Should().BeEquivalentTo("A", "B", "C", "D");
        }
    }
}