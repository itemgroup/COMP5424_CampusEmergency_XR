using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace CampusEmergency.Editor
{
    public sealed class MetaRuntimeActionBindingsBuildProcessor : IPostprocessBuildWithReport
    {
        private const string BindingsFileName = "RuntimeActionBindings.json";

        public int callbackOrder => -1;

        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platformGroup != BuildTargetGroup.Standalone ||
                string.IsNullOrEmpty(report.summary.outputPath))
                return;

            string sourcePath = Path.Combine(Application.dataPath, "StreamingAssets", BindingsFileName);
            if (!File.Exists(sourcePath))
                return;

            string outputDirectory = Path.GetDirectoryName(Path.GetFullPath(report.summary.outputPath));
            string bindingsPath = Path.Combine(outputDirectory, BindingsFileName);
            File.Delete(bindingsPath);
        }
    }
}
