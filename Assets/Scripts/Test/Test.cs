using Interfaces;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    float timer = 0;
    int i = 0;
    Transform transformZZ;
    private void Start()
    {
        Vector3[,] grid= new Vector3[3,3];

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j= 0; j < grid.Length; j++)
            {
                grid[i, j] = transformZZ.position;
            }
        }
    }
}
