using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityScriptTemplates
{
    /// <summary>
    /// This file adds a menu item just below "Create->C# Script" for creating some different script templates.
    /// You can add your own templates by copy-pasting the code in this file and editing the paths.a
    /// </summary>
    public static class CustomScriptTemplates
    {
        private static string TemplatePathMonoBehaviourController =>
            Application.dataPath + "/Editor Default Resources/ScriptTemplates/MonoBehaviourTemplateScript.cs.txt";

        private static string TemplatePathSceneController =>
            Application.dataPath + "/Editor Default Resources/ScriptTemplates/SceneControllerScript.cs.txt";

        /// <summary>
        /// Path to the template file for a PageController.
        /// Using .cs.txt extension to match Unity's built-in templates, but this is _not_ neccessary.
        /// </summary>
        private static string TemplatePathPageController =>
            Application.dataPath + "/Editor Default Resources/ScriptTemplates/PageControllerScript.cs.txt";

        private static string TemplatePathPopupViewBase =>
            Application.dataPath + "/Editor Default Resources/ScriptTemplates/PopupViewBaseScript.cs.txt";

        private static string TemplatePathSubController =>
            Application.dataPath + "/Editor Default Resources/ScriptTemplates/SubControllerScript.cs.txt";


        /// <summary>
        /// ProjectWindowUtil.CreateScriptAsset is the method that makes the magic happen.
        /// It has two parameters:
        /// - templatePath, the absolute path to the template file.
        /// - destName, the suggested file name for the new asset.
        ///
        /// It seems like this method is usually called from c++; it's a private method, and nothing in ProjectWindowUtil calls it, but if you add a breakpoint
        /// in it when hitting Create-C# script, the breakpoint is hit, with no stack trace.
        /// </summary>
        private static MethodInfo CreateScriptAsset
        {
            get
            {
                var projectWindowUtilType = typeof(ProjectWindowUtil);
                return projectWindowUtilType.GetMethod ("CreateScriptAssetFromTemplateFile",
                    BindingFlags.Public | BindingFlags.Static);
            }
        }

//        static void CreateScriptAsset(string templatePath, string destName)
//        {
//            var templateFilename = Path.GetFileName(templatePath);
//            if (templateFilename.ToLower().Contains("editortest") || templateFilename.ToLower().Contains("editmode"))
//            {
//                var tempPath =  AssetDatabase.GetUniquePathNameAtSelectedPath(destName);
//                if (!tempPath.ToLower().Contains("/editor/"))
//                {
//                    tempPath = tempPath.Substring(0, tempPath.Length - destName.Length - 1);
//                    var editorDirPath = Path.Combine(tempPath, "Editor");
//                    if (!Directory.Exists(editorDirPath))
//                        AssetDatabase.CreateFolder(tempPath, "Editor");
//                    tempPath = Path.Combine(editorDirPath, destName);
//                    tempPath = tempPath.Replace("\\", "/");
//                }
//                destName = tempPath;
//            }
//
//            Texture2D icon = null;
//            switch (Path.GetExtension(destName))
//            {
//                case ".js":
//                    icon = EditorGUIUtility.IconContent("js Script Icon").image as Texture2D;
//                    break;
//                case ".cs":
//                    icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;
//                    break;
//                case ".boo":
//                    icon = EditorGUIUtility.IconContent("boo Script Icon").image as Texture2D;
//                    break;
//                case ".shader":
//                    icon = EditorGUIUtility.IconContent("Shader Icon").image as Texture2D;
//                    break;
//                case ".asmdef":
//                    icon = EditorGUIUtility.IconContent("AssemblyDefinitionAsset Icon").image as Texture2D;
//                    break;
//                default:
//                    icon = EditorGUIUtility.IconContent("TextAsset Icon").image as Texture2D;
//                    break;
//            }
//            StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<DoCreateScriptAsset>(), destName, icon, templatePath);
//        }


        #region MonoBehaviourController

        [MenuItem ("Assets/Create/Script Template/C# Script", priority = 81)]
        // Create/C# Script has priority 80, so this puts it just below that.
        public static void CreateMonoBehaviourController ()
        {
            CreateScriptAsset.Invoke (null, new object[] {TemplatePathMonoBehaviourController, "NewMonoBehaviour.cs"});
        }


        [MenuItem ("Assets/Create/Script Template/C# Script", true, priority = 81)]
        public static bool CreateMonoBehaviourValidate ()
        {
            return File.Exists (TemplatePathMonoBehaviourController);
        }

        #endregion


        #region SceneController

        [MenuItem ("Assets/Create/Script Template/SceneController", priority = 81)]
        // Create/C# Script has priority 80, so this puts it just below that.
        public static void CreateSceneController ()
        {
            CreateScriptAsset.Invoke (null, new object[] {TemplatePathSceneController, "NewSceneController.cs"});
        }


        [MenuItem ("Assets/Create/Script Template/SceneController", true, priority = 81)]
        public static bool CreateSceneControllerValidate ()
        {
            return File.Exists (TemplatePathSceneController) && CreateScriptAsset != null;
        }

        #endregion


        #region PageController

        [MenuItem ("Assets/Create/Script Template/PageController", priority = 81)]
        // Create/C# Script has priority 80, so this puts it just below that.
        public static void CreatePageController ()
        {
            CreateScriptAsset.Invoke (null, new object[] {TemplatePathPageController, "NewPageController.cs"});
        }


        [MenuItem ("Assets/Create/Script Template/PageController", true, priority = 81)]
        public static bool CreatePageControllerValidate ()
        {
            return File.Exists (TemplatePathPageController) && CreateScriptAsset != null;
        }

        #endregion


        #region PopupViewBase

        [MenuItem ("Assets/Create/Script Template/PopupViewBase", priority = 81)]
        // Create/C# Script has priority 80, so this puts it just below that.
        public static void CreatePopupViewBase ()
        {
            CreateScriptAsset.Invoke (null, new object[] {TemplatePathPopupViewBase, "NewPopupViewBase.cs"});
        }


        [MenuItem ("Assets/Create/Script Template/PopupViewBase", true, priority = 81)]
        public static bool CreatePopupViewBaseValidate ()
        {
            return File.Exists (TemplatePathPopupViewBase) && CreateScriptAsset != null;
        }

        #endregion


        #region SubController

        [MenuItem ("Assets/Create/Script Template/SubController", priority = 81)]
        // Create/C# Script has priority 80, so this puts it just below that.
        public static void CreateSubController ()
        {
            CreateScriptAsset.Invoke (null, new object[] {TemplatePathSubController, "NewSubController.cs"});
        }


        [MenuItem ("Assets/Create/Script Template/SubController", true, priority = 81)]
        public static bool CreateSubControllerValidate ()
        {
            return File.Exists (TemplatePathSubController) && CreateScriptAsset != null;
        }

        #endregion
    }
}