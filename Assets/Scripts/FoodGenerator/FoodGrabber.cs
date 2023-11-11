using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGrabber : MonoBehaviour
{
	public GameObject FoodHolderSpot;
	
	int CurrentFoodHolded = 0;
	int MaxFoodHolded = 5;
	FoodGenerator CurrentFG;
	GameObject CurrentFoodObj;
	bool bRetrieveAnimStarted = false;
	public float RetrieveAnimDuration = 1;
	float CurrentRetrieveAnimTime = 0;
	Vector3 StartPosition;
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if(bRetrieveAnimStarted)
		{
			float Alpha = 1 / RetrieveAnimDuration * Time.deltaTime;
			print("Alpha " + Alpha);
			Vector3 interpolatedPosition = Vector3.Lerp(StartPosition, FoodHolderSpot.transform.position, Alpha);
			CurrentRetrieveAnimTime += Time.deltaTime;
			print("Anim time " + CurrentRetrieveAnimTime);
			CurrentFoodObj.transform.position = interpolatedPosition;
			
			if(CurrentRetrieveAnimTime >= RetrieveAnimDuration)
			{
				bRetrieveAnimStarted = false;
			}
		}
    }

    private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "FoodGenerator")
		{
			print("Trigger enter " + other.name);
			CurrentFG = other.GetComponent<FoodGenerator>();
			if(CurrentFG.GetCurrentFoodAmount() > 0 && CanGrabMoreFood())
			{
				TryGrabFood();
			}
		}
        
    }

    private void OnTriggerExit(Collider other)
    {
		if(other.tag == "FoodGenerator")
		{
			print("Trigger exit " + other.name);
		}
    }
	
	bool CanGrabMoreFood()
	{
		return CurrentFoodHolded < MaxFoodHolded;
	}
	
	void TryGrabFood()
	{
		CurrentFoodObj = CurrentFG.RetrieveFood();
		if(CurrentFoodObj != null)
		{
			bRetrieveAnimStarted = true;
			StartPosition = CurrentFoodObj.transform.position;
		}
	}
}
