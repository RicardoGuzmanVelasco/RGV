using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using static RGV.PackageLayering.Runtime.AsmdefConstants;

namespace RGV.PackageLayering.Runtime
{
    public class Asmdef
    {
        internal const string Separator = ".";

        readonly AsmdefDeserialization content = new AsmdefDeserialization();

        public Asmdef MergeWith(Asmdef other)
        {
            var target = new AsmdefDeserialization
            {
                name = $"{Name}{(Name.Length > 0 ? Separator : "")}{other.Name}"
            };

            var platformsIn = content.includePlatforms.Union(other.content.includePlatforms);
            target.includePlatforms.AddRange(platformsIn);

            var platformsOut = content.excludePlatforms.Union(other.content.excludePlatforms);
            target.excludePlatforms.AddRange(platformsOut);

            var references = content.references.Union(other.content.references);
            target.references.AddRange(references);

            var dlls = content.precompiledReferences.Union(other.content.precompiledReferences);
            target.precompiledReferences.AddRange(dlls);

            target.overrideReferences = content.overrideReferences || other.content.overrideReferences;

            var constraints = content.defineConstraints.Union(other.content.defineConstraints);
            target.defineConstraints.AddRange(constraints);

            return new Asmdef(target);
        }

        #region Formatting
        public override string ToString()
        {
            return JsonConvert.SerializeObject(content, Formatting.Indented);
        }
        #endregion


        #region Ctors
        public Asmdef() { }

        Asmdef([NotNull] AsmdefDeserialization content)
        {
            this.content = content;
        }

        public Asmdef([NotNull] string name)
        {
            content.name = name;
        }
        #endregion

        #region Properties (delegated)
        public string Name => content.name;
        public IEnumerable<string> NameSegments => content.name.Split(Separator);

        public IEnumerable<string> IncludePlatforms => content.includePlatforms;

        public void Include(params string[] platforms)
        {
            content.includePlatforms.AddRange(platforms);
        }

        public IEnumerable<string> ExcludePlatforms => content.excludePlatforms;

        public void Exclude(params string[] platforms)
        {
            content.excludePlatforms.AddRange(platforms);
        }

        public IEnumerable<string> References => content.references;

        public Asmdef DependsOn(Asmdef other)
        {
            content.references.Add(other.Name);
            return this;
        }

        public IEnumerable<string> DllReferences => content.precompiledReferences;

        public bool OverridesDlls => content.overrideReferences;

        public IEnumerable<string> DefineConstraints => content.defineConstraints;
        #endregion

        #region Factory methods
        public static Asmdef RuntimeTests([NotNull] string context = "")
        {
            return CreateTestsAsmdef(context, context);
        }

        public static Asmdef Runtime([NotNull] string context = "")
        {
            return new Asmdef(context);
        }

        public static Asmdef Editor([NotNull] string context = "")
        {
            return CreateEditorAsmdef(context);
        }

        public static Asmdef EditorTests([NotNull] string context = "")
        {
            return Editor(context).MergeWith(CreateTestsAsmdef("", context));
        }

        public static Asmdef Tests([NotNull] string context = "", string reference = "")
        {
            return CreateTestsAsmdef(context, reference);
        }
        #endregion

        #region Support methods
        static Asmdef CreateEditorAsmdef([NotNull] string context = "")
        {
            var prefix = context + (context.Length > 0 ? Separator : string.Empty);

            var target = new Asmdef(prefix + EditorPlatform);
            target.content.includePlatforms.Add(EditorPlatform);
            target.content.references.Add(context);
            return target;
        }

        static Asmdef CreateTestsAsmdef([NotNull] string context = "", string reference = "")
        {
            var prefix = context + (context.Length > 0 ? Separator : string.Empty);
            var target = new AsmdefDeserialization { name = prefix + TestsFolder };

            target.defineConstraints.Add(TestsConstraint);

            target.precompiledReferences.Add(NUnit);
            target.precompiledReferences.Add(NSubstitute);
            target.overrideReferences = true;

            target.references.Add(EngineTestRunner);
            target.references.Add(EditorTestRunner);
            target.references.Add(FluentAssertions);

            target.references.Add(reference);

            return new Asmdef(target);
        }
        #endregion
    }
}