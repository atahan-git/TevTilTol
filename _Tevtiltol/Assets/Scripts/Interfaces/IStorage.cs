using UnityEngine;
using System.Collections;

public interface IStorage {

	//-------------------------------------------------------------------------------------------- OBJECT STORE VARIABLES

	//Object[] objectsInStore;


	//-------------------------------------------------------------------------------------------- OBJECT STORE FUNCTIONS
	//make an item class to make this better
	bool StoreObject (Object toStore);			//Stores the object and returns if successful


	Object[] AllObjects ();						//Returns all stored objects


	Object TakeObject (int objectListNumber); 	//takes the object with the list number (see "AllObjects") and returns the object if successful - null if not




	//-------------------------------------------------------------------------------------------- RESOURCE STORE VARIABLES

	//int wood;		-0
	//int trash;	-1
	//int iron;		-2


	//-------------------------------------------------------------------------------------------- RESOURCE STORE FUNCTIONS
	int StoreResource (int storeAmount, int storeId);	//Return how much of the resource is actually stored based on inventory space

	int TakeResource (int takeAmount, int takeId);		//Return how much of the resource is actually taken based on available amount


}
