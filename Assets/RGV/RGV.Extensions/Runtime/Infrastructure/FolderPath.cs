using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static RGV.DesignByContract.Runtime.Contract;

namespace RGV.Extensions.Runtime.Infrastructure
{
    public class FolderPath : IReadOnlyCollection<string>
    {
        static readonly char Separator = Path.AltDirectorySeparatorChar;

        readonly string[] pathHierarchy;

        public override string ToString()
        {
            return string.Join(Separator, pathHierarchy);
        }

        public FolderPath Concat(string other)
        {
            return Concat(new FolderPath(other));
        }

        public FolderPath Concat(FolderPath other)
        {
            return new FolderPath(pathHierarchy.Concat(other.pathHierarchy).ToArray());
        }

        #region Ctors
        FolderPath()
        {
            pathHierarchy = Array.Empty<string>();
        }

        public FolderPath(string path)
        {
            Require(path).Not.NullOrWhiteSpace();

            pathHierarchy = path.Trim(Separator).Split(Separator);
        }

        public FolderPath(params string[] pathHierarchy)
            : this(string.Join(Separator.ToString(), pathHierarchy)) { }
        #endregion

        #region Properties
        public string Root => pathHierarchy.First();
        public string Leaf => pathHierarchy.Last();

        public FolderPath Parent =>
            Count < 1
                ? Empty
                : new FolderPath(ToString()[..ToString().LastIndexOf(Separator)]);

        public static FolderPath Empty => new FolderPath();
        #endregion

        #region ICollection implementation
        public int Count => ToString().Count(c => c == Separator);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return pathHierarchy.AsEnumerable().GetEnumerator();
        }
        #endregion
    }
}