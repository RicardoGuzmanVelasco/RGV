namespace RGV.PackageLayering.Runtime
{
    public class PropertiesFolder : PackageLayoutFolder
    {
        public PropertiesFolder(string context)
            : this(new AssemblyInfoFile(context))
        {
            File.AppendFriend("Tests");
            File.AppendFriend("Editor");
            File.AppendFriend("Editor.Tests");
        }

        public PropertiesFolder(AssemblyInfoFile file) : base("Properties")
        {
            File = file;
        }

        public AssemblyInfoFile File { get; }
    }
}