using System.Collections.Generic;
using UnityEngine;

public abstract class Model : ScriptableObject
{
    [field: SerializeField]
    internal GameObject DefaultViewPrefab { get; private set; }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (DefaultViewPrefab == null)
            return;

        if (CheckIfImplementsModel(DefaultViewPrefab))
            return;

        DefaultViewPrefab = null;
        Debug.LogError("View must implement IView<TModel> interface and derive from this model.");
    }

    private bool CheckIfImplementsModel(GameObject go)
    {
        var components = go.GetComponents<Component>();

        // This is cryptic, but basically gets all interfaces that are generic and have a generic argument that is the type of this model
        // so in essence, it checks if the view implements IView<IInstance<DerivedModel>>
        foreach (var component in components)
        {
            var interfaces = component.GetType().GetInterfaces();

            foreach (var @interface in interfaces)
            {
                foreach (var genericArgument in @interface.GetGenericArguments())
                {
                    var argumentInterfaces = genericArgument.GetInterfaces();

                    foreach (var argumentInterface in argumentInterfaces)
                    {
                        foreach (var y in argumentInterface.GetGenericArguments())
                        {
                            if (y == GetType())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

#endif
}
