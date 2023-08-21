using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : View<UnitInstance>
{
    public override void Initialize(UnitInstance instance)
    {
        base.Initialize(instance);
    }

    private void Update()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.red, Color.blue, Instance.NormalizedHP);
    }
}
