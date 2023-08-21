using UnityEngine;

public interface IView<TInstance>
{
    TInstance Instance { get; }
}

public abstract class View : MonoBehaviour
{

}

public abstract class View<TInstance> : View, IView<TInstance>
{
    public TInstance Instance { get; private set; }

    public virtual void Initialize(TInstance instance)
    {
        Instance = instance;
    }
}
