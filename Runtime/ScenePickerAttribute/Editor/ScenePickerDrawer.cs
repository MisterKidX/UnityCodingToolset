using UnityEngine;
using UnityEditor;

namespace DBD.UnityCodingTools.Editor
{
    [CustomPropertyDrawer(typeof(ScenePickerAttribute))]
    public class ScenePickerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var scenePicker = (ScenePickerAttribute)attribute;

            var oldScenePath = AssetDatabase.GUIDToAssetPath(property.stringValue);
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(oldScenePath);

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUI.ObjectField(position, property.displayName, oldScene, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                var guid = AssetDatabase.GUIDFromAssetPath(newPath);
                property.stringValue = guid.ToString();
            }
        }
    }
}