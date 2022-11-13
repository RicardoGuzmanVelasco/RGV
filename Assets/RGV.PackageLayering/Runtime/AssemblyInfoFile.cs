using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGV.PackageLayering.Runtime
{
    public class AssemblyInfoFile
    {
        const char Separator = '.';

        public AssemblyInfoFile() : this("Unknown.Unknown.Unknown") { }

        public AssemblyInfoFile(string title)
        {
            Title = title;
        }

        public string Company => Title.Contains(Separator) ? Title.Split(Separator).First() : null;
        public string Product => Title.Split(Separator).Skip(1).FirstOrDefault();
        public string Title { get; }

        public Version Version { get; } = new Version(0, 1);
        public IReadOnlyList<string> Friends { get; private set; } = new List<string>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Usings());
            sb.AppendLine();
            sb.Append(AssemblyAttribute(nameof(Company), Company));
            sb.Append(AssemblyAttribute(nameof(Product), Product));
            sb.Append(AssemblyAttribute(nameof(Title), Title));
            sb.AppendLine();
            sb.Append(AssemblyAttribute(nameof(Version), Version));
            sb.AppendLine();
            sb.AppendJoin(Environment.NewLine, Friends.Select(InternalsVisibleTo));

            return sb.ToString();
        }

        #region Internal visibility (aka friendship)
        public void AppendFriend(string assemblyName)
        {
            AddFriend(string.Join(Separator, Title, assemblyName));
        }

        public void AddFriend(string assemblyName)
        {
            Friends = Friends.Concat(new[] { assemblyName }).ToList();
        }
        #endregion

        #region Formatting helpers
        static string AssemblyAttribute(string key, object value)
        {
            return value is null or ""
                ? string.Empty
                : $"[assembly: Assembly{key}(\"{value}\")]\r\n";
        }

        static string InternalsVisibleTo(object value)
        {
            return $"[assembly: InternalsVisibleTo(\"{value}\")]\r\n";
        }

        static string Usings()
        {
            return "using System.Reflection;\r\n" +
                   "using System.Runtime.CompilerServices;\r\n";
        }
        #endregion
    }
}