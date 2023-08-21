using System;
using UnityEditor;

internal class MVIEditorTools : Editor
{
    static bool _initialized = false;
    [InitializeOnLoadMethod]
    public static void DeleteOnExitPlayMode()
    {
        if (_initialized) return;

        _initialized = true;
        EditorApplication.playModeStateChanged += DeleteGameState;
    }

    private static void DeleteGameState(PlayModeStateChange change)
    {
        if (change == PlayModeStateChange.EnteredEditMode)
        {
            string[] gameState = { "Assets/_MVI_GameState" };
            foreach (var asset in AssetDatabase.FindAssets("", gameState))
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
