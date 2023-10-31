using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level = 1;

    public GameObject[] rooms;

    private void Awake()
    {
        for(int i = 1; i <= rooms.Length; i++)
        {
            if(i <= level)
            {
                rooms[i-1].SetActive(true);
            }
        }
    }

}
