using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanva : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void CloseDirectly()
    {
        gameObject.SetActive(false);
    }

    public void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);   
    }
}
