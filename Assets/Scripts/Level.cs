using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int index;
    [SerializeField] Transform startPos;

    private void Start()
    {
       OnInit();
    }

    public void OnInit()
    {
        transform.position = startPos.position;
    }
}
