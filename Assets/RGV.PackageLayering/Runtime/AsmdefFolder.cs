using System.Linq;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace RGV.PackageLayering.Runtime
{
    public class AsmdefFolder : PackageLayoutFolder
    {
        public AsmdefFolder([NotNull] Asmdef asmdef, [NotNull] string folderName)
            : base(folderName)
        {
            Assert.IsNotNull(asmdef);
            Asmdef = asmdef;
        }

        public AsmdefFolder([NotNull] Asmdef asmdef)
            : this(asmdef, InferNameFromAsmdef(asmdef)) { }

        public Asmdef Asmdef { get; }

        public static AsmdefFolder Production(string context, string name)
        {
            var asmdef = new Asmdef(context + Asmdef.Separator + name);

            var result = new AsmdefFolder(asmdef, name);
            result.AddChild(new PropertiesFolder(result.Asmdef.Name));

            return result;
        }

        static string InferNameFromAsmdef(Asmdef asmdef)
        {
            return asmdef.NameSegments.Contains(AsmdefConstants.EditorPlatform)
                ? AsmdefConstants.EditorPlatform
                : AsmdefConstants.RuntimeFolder;
        }
    }
}