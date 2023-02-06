using System.Collections.Generic;

namespace RGV.PackageLayering.Runtime
{
    internal class AsmdefDeserialization
    {
        public bool allowUnsafeCode;

        public bool autoReferenced = true;
        public List<string> defineConstraints = new List<string>();
        public List<string> excludePlatforms = new List<string>();

        public List<string> includePlatforms = new List<string>();
        public string name = "";
        public bool noEngineReferences;

        public bool overrideReferences;
        public List<string> precompiledReferences = new List<string>();

        public List<string> references = new List<string>();
        public string rootNamespace = "";

        public List<string> versionDefines = new List<string>();
    }
}