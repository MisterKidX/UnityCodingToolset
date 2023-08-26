using UnityEngine;
using DBD.MVI;

namespace DBD.MVI.Demo
{
    [CreateAssetMenu(menuName = "Unit")]
    public class UnitModel : Model
    {
        public int MaxHp;
        public int Damage;
    }

}
