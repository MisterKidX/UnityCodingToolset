using UnityEngine;

namespace DBD.MVI.Demo
{
    public class MVIDemoRunner : MonoBehaviour
    {
        public UnitModel Tank;
        public UnitModel Assassin;

        UnitInstance _tank;
        UnitInstance _assassin;

        private void Awake()
        {
            // create 2 units
            _tank = UnitInstance.Create(Tank);

            _assassin = UnitInstance.Create(Assassin);
            _tank.InstantiateView();
            _tank.Views[0].transform.position = new Vector3(-2, 0, 0);
            _assassin.InstantiateView();
            _assassin.Views[0].transform.position = new Vector3(2, 0, 0);

            // try create delete unit
            var tank = UnitInstance.Create(Tank);
            tank.InstantiateView();
            tank.Dispose();
        }

        bool _isTankTurn = true;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _tank != null && _assassin != null)
            {
                if (!_isTankTurn)
                {
                    _tank.CurrentHp -= _assassin.Model.Damage;

                    if (_tank.CurrentHp <= 0)
                    {
                        _tank.Dispose();
                    }
                }
                else
                {
                    _assassin.CurrentHp -= _tank.Model.Damage;

                    if (_assassin.CurrentHp <= 0)
                    {
                        _assassin.Dispose();
                    }
                }

                _isTankTurn = !_isTankTurn;
            }
        }
    }
}