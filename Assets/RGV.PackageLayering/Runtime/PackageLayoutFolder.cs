using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RGV.Extensions.Runtime.Infrastructure;
using static RGV.DesignByContract.Runtime.Contract;

namespace RGV.PackageLayering.Runtime
{
    public class PackageLayoutFolder
    {
        const char IndexIgnoredSuffix = '~';

        #region Ctors
        public PackageLayoutFolder([NotNull] string name)
        {
            AssertReservedChars(name);

            Name = name;
            IndexIgnored = name.EndsWith(IndexIgnoredSuffix);

            static void AssertReservedChars(string name)
            {
                Require(name.Count(c => c == IndexIgnoredSuffix)).Not.GreaterThan(1);
                Require(!name.Contains(IndexIgnoredSuffix) ||
                        name.LastIndexOf(IndexIgnoredSuffix) == name.Length - 1).True();
            }
        }
        #endregion

        #region Factory methods
        public static PackageLayoutFolder Docs()
        {
            return new PackageLayoutFolder("Documentation") { IndexIgnored = true };
        }
        #endregion

        /// <remarks> No CQRS, fluent API </remarks>
        public PackageLayoutFolder AddChild([NotNull] PackageLayoutFolder child)
        {
            Require(child).Not.Null();
            Require(HasChildWithName(child.Name)).False();

            Children = Children.Concat(new[] { child }).ToList();
            child.Parent = this;

            return this;
        }

        [CanBeNull]
        public PackageLayoutFolder GetChild(string childName)
        {
            return Children.SingleOrDefault(f => f.Name == childName);
        }

        public bool HasChildWithName(string name)
        {
            return GetChild(name) != null;
        }

        public IEnumerable<PackageLayoutFolder> GetChildren()
        {
            foreach(var child in Children)
            {
                yield return child;
                using var it = child.GetChildren().GetEnumerator();
                while(it.MoveNext())
                    yield return it.Current;
            }
        }

        public override string ToString() => $"{RelativePath}";

        #region Properties
        public string Name { get; private set; }
        public PackageLayoutFolder Parent { get; private set; }
        public IReadOnlyList<PackageLayoutFolder> Children { get; private set; } = new List<PackageLayoutFolder>();

        public bool IndexIgnored
        {
            get => Name.EndsWith(IndexIgnoredSuffix);
            set => Name = value ? Name + IndexIgnoredSuffix : Name.TrimEnd(IndexIgnoredSuffix);
        }

        public FolderPath RelativePath => new FolderPath(Parent?.RelativePath.ToString(), Name);
        #endregion
    }
}