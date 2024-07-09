using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public static CreateMap instance { get; private set; }

    public GameObject cube;
    public GameObject plane;
    public GameObject block;

    public GameObject path;
    public GameObject corner;
    public GameObject bridge;

    public Transform _parent;

    List<Vector3> wallPos = new List<Vector3>();
    List<Vector3> blockPos = new List<Vector3>();
    List<Vector3> planePos = new List<Vector3>();
    List<Vector3> bridges = new List<Vector3>();

    [SerializeField] Vector3 playerPosition;

    string map =
        "0000000\n" +
        "000#000\n" +
        "000#000\n" +
        "0###000\n" +
        "0#0###0\n" +
        "0#000#0\n" +
        "0###0#0\n" +
        "000###0\n" +
        "000#000\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "nnnbnnn\n" +
        "0000000";


    void Start()
    {
        GetCubePos(map);
        Spawn(cube, plane);
        //Player.Instance.transform.position = blockPos[0];
    }

    void Spawn(GameObject cube, GameObject plane)
    {
        for (int i = 0; i < wallPos.Count; i++)
        {
            GameObject go = Instantiate(cube, wallPos[i], Quaternion.identity);
            go.transform.SetParent(_parent);
        }
        for (int i = 1; i < blockPos.Count; i++)
        {
            GameObject go = Instantiate(block, blockPos[i], Quaternion.identity);
            go.transform.SetParent(_parent);
        }
        for (int i = 0; i < planePos.Count; i++)
        {
            GameObject go = Instantiate(plane, planePos[i], Quaternion.identity);
            go.transform.SetParent(_parent);
        }
        for (int i = 0; i < bridges.Count; i++)
        {
            GameObject go = Instantiate(bridge, bridges[i], Quaternion.identity);
            go.transform.SetParent(_parent);
        }
    }

    void GetCubePos(string str)
    {
        string[] s = str.Split('\n');
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = 0; j < s[i].Length; j++)
            {
                if (s[i][j] == '0')
                {
                    wallPos.Add(new Vector3(j, 0, i));
                }
                if (s[i][j] == '#')
                {
                    blockPos.Add(new Vector3(j, 0.1f, i));
                    planePos.Add(new Vector3(j, 0, i));
                }
                if (s[i][j] == 'b')
                {
                    bridges.Add(new Vector3(j, 0, i));
                }
                
            }
        }
    }

}
