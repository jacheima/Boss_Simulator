// Copyright (c) Pixel Crushers. All rights reserved.

using System.Collections.Generic;
using UnityEditor;

namespace PixelCrushers
{

    public static class MoreEditorUtility
    {

        /// <summary>
        /// Try to add a symbol to the project's Scripting Define Symbols for the current build target.
        /// </summary>
        public static void TryAddScriptingDefineSymbols(string newDefine)
        {
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (!string.IsNullOrEmpty(defines)) defines += ";";
            defines += newDefine;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, defines);
        }

        /// <summary>
        /// Try to remove a symbol from the project's Scripting Define Symbols for the current build target.
        /// </summary>
        public static void TryRemoveScriptingDefineSymbols(string define)
        {
            var symbols = new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';'));
            symbols.Remove(define);
            var defines = string.Join(";", symbols.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, defines);
        }

        /// <summary>
        /// Add or remove a scripting define symbol.
        /// </summary>
        public static void ToggleScriptingDefineSymbol(string define, bool value)
        {
            if (value == true) TryAddScriptingDefineSymbols(define);
            else TryRemoveScriptingDefineSymbols(define);
        }

        /// <summary>
        /// Checks if a symbol exists in the project's Scripting Define Symbols.
        /// </summary>
        public static bool DoesScriptingDefineSymbolExist(string define)
        {
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';');
            for (int i = 0; i < defines.Length; i++)
            {
                if (string.Equals(define, defines[i].Trim())) return true;
            }
            return false;
        }

    }
}
