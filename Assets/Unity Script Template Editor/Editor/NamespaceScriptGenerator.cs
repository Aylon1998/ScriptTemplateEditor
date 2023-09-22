using System.IO;
using UnityEditor;

public class NamespaceScriptGenerator : AssetPostprocessor
{
    private const string NamespacePlaceholder = "#AUTONAMESPACE#";

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (var asset in importedAssets)
        {
            if (IsCSharpScript(asset))
            {
                ProcessScript(asset);
            }
        }
    }

    /// <summary>
    /// Checks if the provided asset is a C# script.
    /// </summary>
    private static bool IsCSharpScript(string asset)
    {
        return asset.EndsWith(".cs");
    }

    /// <summary>
    /// Replaces namespace placeholders in the script with the appropriate namespace.
    /// </summary>
    private static void ProcessScript(string asset)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), asset);
        var content = File.ReadAllText(fullPath);

        if (content.Contains(NamespacePlaceholder))
        {
            var namespaceName = ComputeNamespaceFromAssetPath(asset);
            content = content.Replace(NamespacePlaceholder, namespaceName);
            File.WriteAllText(fullPath, content);
            AssetDatabase.Refresh();
        }
    }

    /// <summary>
    /// Computes the namespace from the asset's directory path.
    /// </summary>
    private static string ComputeNamespaceFromAssetPath(string assetPath)
    {
        string directoryPath = Path.GetDirectoryName(assetPath);
        string namespaceName = directoryPath.Replace(" ", "_").Replace("/", ".").Replace("\\", ".");

        if (namespaceName.StartsWith("Assets."))
        {
            namespaceName = namespaceName.Substring(7);
        }

        return namespaceName;
    }
}
