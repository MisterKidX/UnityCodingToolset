using UnityEngine;

namespace DBD.UnityCodingTools.Demo
{
    public class Rotator : MonoBehaviour
    {
        public float speed = 45;
        private void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
}