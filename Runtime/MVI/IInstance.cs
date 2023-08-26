using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DBD.MVI
{
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

        public void Dispose(bool destroyViews = true)
        {
            if (destroyViews)
            {
                foreach (var view in Views)
                {
                    Destroy(view.gameObject);
                }
            }

#if UNITY_EDITOR
            AssetDatabase.DeleteAsset(_path);
#else
        DestroyImmediate(this);
#endif
        }

#if UNITY_EDITOR
        static int _editorId = 0;
        string _path;
#endif
        protected virtual void Initialize()
        {
#if UNITY_EDITOR
            if (AssetDatabase.IsValidFolder("Assets/_MVI_GameState") == false)
                AssetDatabase.CreateFolder("Assets", "_MVI_GameState");

            _path = $"Assets/_MVI_GameState/{Model.name}_{_editorId++}.asset";
            AssetDatabase.CreateAsset(this, _path);
#endif
        }
    }
}