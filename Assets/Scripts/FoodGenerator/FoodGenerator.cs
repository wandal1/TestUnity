using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public GameObject FoodToGenerate;
    public GameObject SpotToSpawn;
    public float SpawnRate = 1;
    public float SpawnGap = 5;
    public int MaxFoodAmount = 3;
    int CurrentFoodAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartFoodGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartFoodGeneration()
    {
        Invoke("GenerateFood", SpawnRate);
    }

    void GenerateFood()
    {
        if (CurrentFoodAmount < MaxFoodAmount)
        {
            Vector3 SpawnPos = SpotToSpawn.transform.position;
            SpawnPos.y += CurrentFoodAmount * SpawnGap;

            Instantiate(FoodToGenerate, SpawnPos, Quaternion.Euler(-90, 0, 0));
            CurrentFoodAmount++;
            StartFoodGeneration();
        }
    }
}
