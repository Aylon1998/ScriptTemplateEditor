using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ScriptTemplateEditor : EditorWindow
{
    #region Fields

    private string templateContent = "";
    private string originalTemplateContent = "";  // Store the original template content here

    // For handling the scroll view in the editor window.
    private Vector2 scrollPosition;

    #endregion

    #region Unity Editor Methods

    [MenuItem("Tools/Script Template Editor")]
    public static void ShowWindow()
    {
        GetWindow<ScriptTemplateEditor>("Script Template Editor");
    }

    // Called when the window is opened or if the window already exists, when it gets focus.
    private void OnEnable()
    {
        // Fetching the current default script template.
        var scriptTemplatesPath = GetScriptTemplatesPath();
        if (File.Exists(scriptTemplatesPath))
        {
            templateContent = File.ReadAllText(scriptTemplatesPath);
            string backupPath = "Assets/Resources/OriginalScriptTemplate.txt";
            if (!File.Exists(backupPath))  // Check if backup already exists
            {
                File.WriteAllText(backupPath, templateContent);  // Back up the original template content
            }
            originalTemplateContent = File.ReadAllText(backupPath);  // Load the original template content from backup
        }
        else
        {
            Debug.LogError("Default script template not found.");
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Edit the script template below:", EditorStyles.boldLabel);

        // TextArea to edit the script template.
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));
        templateContent = EditorGUILayout.TextArea(templateContent, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();

        // Button to save the current content as a template within the project.
        if (GUILayout.Button("Save Template"))
        {
            SaveTemplate();
        }

        // Button to set the current content as the default Unity script template.
        if (GUILayout.Button("Set as Default Script"))
        {
            SetAsDefaultScript();
        }

        // Button to reset to the original Unity script template.
        if (GUILayout.Button("Reset to Default"))
        {
            ResetToDefault();
        }
    }

    #endregion

    #region Private Methods

    // Save the edited template in a designated path within the project.
    private void SaveTemplate()
    {
        string path = "Assets/Resources/ScriptTemplate.txt"; // Path where the edited template will be saved.
        File.WriteAllText(path, templateContent);
        AssetDatabase.Refresh();
    }

    // Set the edited template as the default Unity script template.
    private void SetAsDefaultScript()
    {
        var templatePath = "Assets/Resources/ScriptTemplate.txt";
        var scriptTemplatesPath = GetScriptTemplatesPath();

        UpdateScriptTemplate(templatePath, scriptTemplatesPath);
    }

    // Reset to the original Unity script template.
    private void ResetToDefault()
    {
        var backupPath = "Assets/Resources/OriginalScriptTemplate.txt";
        var scriptTemplatesPath = GetScriptTemplatesPath();

        UpdateScriptTemplate(backupPath, scriptTemplatesPath);

        // Update the text area with the original template content.
        templateContent = originalTemplateContent;

        Repaint();
    }

    // Update script template utility method
    private void UpdateScriptTemplate(string sourcePath, string destinationPath)
    {
        try
        {
            if (File.Exists(sourcePath))
            {
                var content = File.ReadAllText(sourcePath);
                File.WriteAllText(destinationPath, content);
                Debug.Log("Script template updated successfully.");
            }
            else
            {
                Debug.LogError("Template file does not exist.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Debug.LogError("Permission denied. Please refer to the README file provided for instructions on adjusting permissions, and try again.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to update script template: " + ex.Message);
        }
    }

    // Determine the path of the default Unity script template.
    private string GetScriptTemplatesPath()
    {
        var unityEditorPath = EditorApplication.applicationPath;

        // For Windows OS.
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            unityEditorPath = Directory.GetParent(unityEditorPath).FullName;
            return Path.Combine(unityEditorPath, "Data", "Resources", "ScriptTemplates", "81-C# Script-NewBehaviourScript.cs.txt");
        }
        // For macOS.
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            return Path.Combine(unityEditorPath, "Contents", "Resources", "ScriptTemplates", "81-C# Script-NewBehaviourScript.cs.txt");
        }

        return string.Empty;  // In case platform is neither Windows nor macOS.
    }

    #endregion
}