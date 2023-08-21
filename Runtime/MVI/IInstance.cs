using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public interface IInstance<TModel> where TModel : Model
{
    TModel Model { get; }
}

public abstract class Instance<TModel, TInstance> : ScriptableObject, IInstance<TModel> 
    where TModel : Model
    where TInstance : Instance<TModel, TInstance>
{
    public TModel Model { get; internal set; }
    public List<View<TInstance>> Views { get; private set; } = new List<View<TInstance>>();

    public void InstantiateView()
    {
        var inst = Instantiate(Model.DefaultViewPrefab);
        var view = inst.GetComponent<View<TInstance>>();
        view.Initialize((TInstance)this);
        Views.Add(view);
    }

    public static TInstance Create(TModel model)
    {
        var so = ScriptableObject.CreateInstance<TInstance>();
        so.Model = model;
        so.Initialize();
        return so;
    }

    public void Destroy()
    {
#if UNITY_EDITOR
        AssetDatabase.DeleteAsset(_path);
#endif

        foreach (var view in Views)
        {
            Destroy(view.gameObject);
        }

        DestroyImmediate(this);
    }

#if UNITY_EDITOR
    static int _editorId = 0;
    string _path;
#endif
    public virtual void Initialize()
    {
#if UNITY_EDITOR
        if (AssetDatabase.IsValidFolder("Assets/_MVI_GameState") == false)
            AssetDatabase.CreateFolder("Assets", "_MVI_GameState");

        _path = $"Assets/_MVI_GameState/{Model.name}_{_editorId++}.asset";
        AssetDatabase.CreateAsset(this, _path);
#endif
    }

#if UNITY_EDITOR
    private void OnDisable()
    {
        string[] gameState = { "Assets/_MVI_GameState" };
        foreach (var asset in AssetDatabase.FindAssets("", gameState))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(path);
        }
    }
#endif
}