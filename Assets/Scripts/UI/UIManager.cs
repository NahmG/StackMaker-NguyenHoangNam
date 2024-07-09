using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Transform CanvaParentTF;

    [SerializeField] UICanva[] uiResources;

    private Dictionary<System.Type, UICanva> uiCanvaPrefabs = new Dictionary<System.Type, UICanva>();
    private Dictionary<System.Type, UICanva> uiCanvas = new Dictionary<System.Type, UICanva>();

    public T OpenUI<T>() where T : UICanva
    {
        UICanva canvas = GetUI<T>();

        canvas.Open();

        return canvas as T;
    }

    public void CloseUI<T>(float delayTime) where T : UICanva
    {
        if (IsOpened<T>())
        {
            GetUI<T>().Close(delayTime);
        }
    }

    public bool IsOpened<T>() where T : UICanva
    {
        return IsLoaded<T>() && uiCanvas[typeof(T)].gameObject.activeInHierarchy;
    }

    public bool IsLoaded<T>() where T : UICanva
    {
        System.Type type = typeof(T);
        return uiCanvas.ContainsKey(type) && uiCanvas[type] != null;
    }

    public T GetUI<T>() where T : UICanva
    {
        if (!IsLoaded<T>())
        {
            UICanva canva = Instantiate(GetUIPrefab<T>(), CanvaParentTF);
            uiCanvas[typeof(T)] = canva;
        }

        return uiCanvas[typeof(T)] as T;
    }

    public T GetUIPrefab<T>() where T : UICanva
    {
        if (!uiCanvaPrefabs.ContainsKey(typeof(T)))
        {
            uiResources = Resources.LoadAll<UICanva>("UI");

            for (int i = 0; i < uiResources.Length; i++)
            {
                if (uiResources[i] is T)
                {
                    uiCanvaPrefabs[typeof(T)] = uiResources[i];
                    break;
                }
            }
        }
        return uiCanvaPrefabs[typeof(T)] as T;
    }

}
