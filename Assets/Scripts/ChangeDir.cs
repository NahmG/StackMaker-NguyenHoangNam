using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDir : MonoBehaviour
{
    public List<Vector3> availableDir = new List<Vector3>();    
    [SerializeField] LayerMask wall;

    List<Vector3> dir = new()
    {
        Vector3.forward,
        Vector3.back,
        Vector3.right,
        Vector3.left,
    };

    private void OnEnable()
    {
        CheckAvailableDir();
    }

    private void Update()
    {
        foreach (Vector3 dir in dir)
        {
            Debug.DrawLine(transform.position, transform.position + dir * .8f, Color.black);
        }
    }

    void CheckAvailableDir()
    {
        foreach (Vector3 dir in dir)
        {
            Physics.Raycast(transform.position, dir, out RaycastHit hit, .8f, wall);
            if (hit.collider == null)
            {
                availableDir.Add(dir);
            }
            else
            {

            }
        }
        
    }
}
