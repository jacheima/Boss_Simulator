using UnityEditor;
using UnityEngine;

namespace FIMSpace.FEditor
{
    /// <summary>
    /// FM: Class with basic tools for working in Unity Editor level
    /// </summary>
    public static class FEditorBasicTools
    {
    }

    /// <summary>
    /// FM: Basic editor window to rename scene or project objects
    /// </summary>
    public class FEditor_RenameSelected : EditorWindow
    {
        [MenuItem("Window/FImpossibleGames/Basic Tools/Rename selected objects", false, 0)]
        static void Init()
        {
            FEditor_RenameSelected window = (FEditor_RenameSelected)EditorWindow.GetWindow(typeof(FEditor_RenameSelected));

            Vector2 windowSize = new Vector2(300f, 170f);

            window.minSize = windowSize;
            window.maxSize = windowSize;

            window.autoRepaintOnSceneChange = true;
            window.titleContent = new GUIContent("Rename Selected");
            window.Show();
        }

        static bool replaceMode = false;
        static bool iterationMode = false;
        static int iterationDigits = 1;
        static int iterationStartFrom = 0;
        static string iterationPrefix = " [";
        static string iterationSuffix = "]";
        static string toBeReplaced = "This will be replaced";
        static string targetName = "New name";

        void OnGUI()
        {
            replaceMode = GUILayout.Toggle(replaceMode, "Replace");

            if (replaceMode)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("To be replaced: ");
                toBeReplaced = EditorGUILayout.TextArea(toBeReplaced);
                GUILayout.EndHorizontal();
                GUILayout.Space(10f);
            }

            iterationMode = GUILayout.Toggle(iterationMode, "Add iteration numbers");

            if (iterationMode)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Number string: ");
                iterationDigits = EditorGUILayout.IntField(iterationDigits);
                if (iterationDigits < 1) iterationDigits = 1;

                GUILayout.Label("(" + FStringMethods.IntToString(1, iterationDigits) + ")");

                GUILayout.Label("Start from: ");
                iterationStartFrom = EditorGUILayout.IntField(iterationStartFrom);

                GUILayout.EndHorizontal();

                GUILayout.Space(10f);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Prefix: ");
                iterationPrefix = EditorGUILayout.TextArea(iterationPrefix);

                GUILayout.Label("Suffix: ");
                iterationSuffix = EditorGUILayout.TextArea(iterationSuffix);

                GUILayout.EndHorizontal();
                GUILayout.Space(10f);
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Target name: ");
            targetName = EditorGUILayout.TextArea(targetName);
            GUILayout.EndHorizontal();

            GUILayout.Space(10f);

            if (GUILayout.Button("Rename " + Selection.objects.Length + " objects!"))
            {
                RenameThem();
            }
        }

        static void RenameThem()
        {
            if (Selection.objects.Length == 0)
            {
                Debug.LogError("No selected objects!");
                return;
            }

            Object[] objects = Selection.objects;

            GameObject gameObjectCheck = objects[0] as GameObject;
            int i = iterationStartFrom;

            if (gameObjectCheck)
            {
                if (!replaceMode) foreach (GameObject o in objects)
                    {
                        o.name = targetName;
                        if (iterationMode) o.name += iterationPrefix + FStringMethods.IntToString(i, iterationDigits) + iterationSuffix;
                        i++;
                    }
                else
                    foreach (GameObject o in objects)
                    {
                        o.name = o.name.Replace(toBeReplaced, targetName);
                        if (iterationMode) o.name += iterationPrefix + FStringMethods.IntToString(i, iterationDigits) + iterationSuffix;
                        i++;
                    }
            }
            else
            {
                foreach (Object o in objects)
                {
                    string newName = o.name;

                    if (!replaceMode)
                    {
                        newName = targetName;

                        // We need to add iteration because there can be conflicts with same file names

                        if (!iterationMode)
                            newName += FStringMethods.IntToString(i, iterationDigits);
                        else
                            newName += iterationPrefix + FStringMethods.IntToString(i, iterationDigits) + iterationSuffix;

                        i++;
                    }
                    else
                    {
                        newName = newName.Replace(toBeReplaced, targetName);

                        if (iterationMode) newName += iterationPrefix + FStringMethods.IntToString(i, iterationDigits) + iterationSuffix;
                        i++;
                    }

                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(o), newName);
                }
            }
        }
    }
}
