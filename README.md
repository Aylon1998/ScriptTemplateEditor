# Unity Script Template Editor #

## Overview

The Unity Script Template Editor is a simple Unity editor tool that allows you to edit and set your own default script templates. This gives you the flexibility to start all your new scripts with a structure that you prefer.

## Installation

1. **Adjusting Permissions:** Before being able to modify the script template, you may need to adjust the permissions on the script template file.

   **Windows:**

   1. Locate your Unity installation directory. By default, this might be something like C:\Program Files\Unity\Hub\Editor\[UnityVersion]\. Navigate to Data\Resources\ScriptTemplates\.
   2. Right-click on the 81-C# Script-NewBehaviourScript.cs.txt file and select Properties. Switch to the Security tab.
   3. Click on the Edit button.
   4. Select your username from the list, then check the Full Control checkbox. Click Apply.
   5. You should now be able to modify this file with any text editor without needing to run it as an administrator.

   **macOS:**

   1. Open Terminal.
   2. Enter sudo chmod 666 [path-to-the-file], replacing [path-to-the-file] with the full path to the script template. This command makes the file readable and writable by all users.
   3. Enter your password when prompted.
   4. You should now be able to edit the file without needing additional permissions.

   **Note:** It's a good practice to backup the original 81-C# Script-NewBehaviourScript.cs.txt file before making any changes.

2. Ensure that the `ScriptTemplateEditor.cs` script is placed within a folder named `Editor` in your Unity project. Unity treats the `Editor` folder specially, and the script will not function correctly if placed elsewhere.

## Usage

1. Open the Tool: Go to Tools > Script Template Editor in the Unity Editor to open the Script Template Editor window.

2. Edit the Template: You can now edit the script template in the provided text area.

3. Save the Template: Click the Save Template button to save your current content as a template within your project. This will save it by default under - Assets/Resources/ScriptTemplate.txt (So make sure your project has a Resources folder).

4. Set as Default Script: Click the Set as Default Script button to set your edited template as Unity's default script template.

## Notes

1. Make sure to adjust permissions ! In order to successfully overwrite Unity's default script template, you need to adjust the user's permissions. This is because changing system files (which the default script templates are considered as) often requires elevated permissions.

2. In order for the tool to correctly replace script and namespace names, ensure your template includes the #SCRIPTNAME# and #AUTONAMESPACE# placeholders. When you create a new script in Unity, these placeholders will be replaced with the actual script name and appropriate namespace, respectively.

For example:

namespace #AUTONAMESPACE#
{
    public class #SCRIPTNAME# : MonoBehaviour
    {
        // Your script content here.
    }
}


## Troubleshooting

1. **Script Template Not Updating:**
    - Ensure that you have saved the template within the editor using the 'Save Template' button.
    - Make sure you have clicked on the 'Set as Default Script' button to set your edited template as Unity's default script template.
    - Verify that Unity is running as an administrator as elevated permissions are required to modify system files.

2. **Placeholders Not Replaced:**
    - Double-check that your script template includes the #SCRIPTNAME# and #AUTONAMESPACE# placeholders. These placeholders are crucial for the correct naming of your scripts and namespaces.

3. **Error Messages:**
    - If you encounter error messages, take note of the exact message. It might provide insights into what went wrong.
    - Verify that `ScriptTemplateEditor.cs` is placed within a folder named `Editor` in your Unity project.

4. **File Not Found:**
    - If you get a 'file not found' error, ensure that the template file is saved at the correct location `Assets/Resources/ScriptTemplate.txt` and that the folder structure exists.
    - Verify the file path to the script template in the Unity Editor console. It should match the path where your script template is saved.

5. **General:**
    - Close and reopen the Script Template Editor window, or restart Unity to ensure all changes are updated.
    - Check the Unity console for any other error or warning messages that might indicate what the issue is.
