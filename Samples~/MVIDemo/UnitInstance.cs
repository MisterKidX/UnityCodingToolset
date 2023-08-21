using System;
using UnityEditor;
using UnityEngine;

public class UnitInstance : Instance<UnitModel, UnitInstance>
{
    public float NormalizedHP => (float)CurrentHp / Model.MaxHp;

    private int _currentHp;
    public int CurrentHp
    {
        get => _currentHp; set
        {
            _currentHp = Math.Max(value, 0);
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        CurrentHp = Model.MaxHp;
    }
}
