using UnityEngine;

namespace DBD.UnityCodingTools.Demos
{
    public class ScenePickerAttributeTest : MonoBehaviour
    {
        [ScenePicker]
        public string scene;
        [ScenePicker]
        public string[] scenes;
    }
}