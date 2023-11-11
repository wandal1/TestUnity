using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public GameObject FoodToGenerate;
    public GameObject SpotToSpawn;
    public float SpawnRate = 1;
    //float SpawnGap = 5;
    int MaxFoodAmount = 4;
    int CurrentFoodAmount = 0;
	public Vector2[] SpotsArray;
	List<GameObject> FoodArray;

	//TODO: Handle array of created Food, and select an empty spot that are free after Retrieving food

    // Start is called before the first frame update
    void Start()
    {
		FoodArray = new FoodArray[4];
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
			SpawnPos.x += SpotsArray[CurrentFoodAmount].x;
			SpawnPos.z += SpotsArray[CurrentFoodAmount].y;
	
			GameObject Food = Instantiate(FoodToGenerate, SpawnPos, Quaternion.Euler(0, 0, 0));
			FoodArray.Add(Food);
			CurrentFoodAmount++;
			StartFoodGeneration();
        }
    }
	
	int GetEmptySlotIndex()
	{
		for(int i = 0; i < MaxFoodAmount; i++)
		{
			if(FoodArray[i] == null)
			{
				return i;
			}
		}
		
		return -1;
	}
	
	public int GetCurrentFoodAmount()
	{
		return CurrentFoodAmount;
	}
	
	public GameObject RetrieveFood()
	{
		if(FoodArray.Num() > 0)
		{
			GameObject Food = FoodArray[CurrentFoodAmount];
			FoodArray.RemoveAt(CurrentFoodAmount);
			CurrentFoodAmount--;
			return Food;
		}
		return null;
	}
}
