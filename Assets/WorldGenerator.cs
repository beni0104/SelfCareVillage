using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerator : MonoBehaviour
{

	public GameObject TerrainPrefab;
	public GameObject TerrainParent;

	public GameObject treePrefab_1;
	public GameObject treePrefab_2;
	public GameObject treePrefab_3;
	public GameObject treePrefab_4;
	public List<GameObject> treePrefabs;

	public GameObject rockPrefab_1;
	public GameObject rockPrefab_2;
	public List<GameObject> rockPrefabs;

	public GameObject grassPrefab_1;
	public GameObject grassPrefab_2;
	public List<GameObject> grassPrefabs;

	public GameObject mushroomPrefab_1;
	public GameObject mushroomPrefab_2;
	public GameObject mushroomPrefab_3;
	public GameObject mushroomPrefab_4;
	public List<GameObject> mushroomPrefabs;

	public GameObject flowerPrefab_1;
	public GameObject flowerPrefab_2;
	public GameObject flowerPrefab_3;
	public List<GameObject> flowerPrefabs;

	public int TerrainSize = 3;

	System.Random random = new System.Random();


	//public GameObject characterController;

	// allows us to see the maze generation from the scene view
	//public bool generateRoof = true;

	//public int NumberOfRooms;

	// this will determine whether we've placed the character controller
	private bool characterPlaced = false;

	// Use this for initialization
	void Start()
	{
		grassPrefabs.Add(grassPrefab_1);
		grassPrefabs.Add(grassPrefab_2);

		treePrefabs.Add(treePrefab_1);
		treePrefabs.Add(treePrefab_2);
		treePrefabs.Add(treePrefab_3);
		treePrefabs.Add(treePrefab_4);

		rockPrefabs.Add(rockPrefab_1);
		rockPrefabs.Add(rockPrefab_2);

		flowerPrefabs.Add(flowerPrefab_1);
		flowerPrefabs.Add(flowerPrefab_2);
		flowerPrefabs.Add(flowerPrefab_3);

		mushroomPrefabs.Add(mushroomPrefab_1);
		mushroomPrefabs.Add(mushroomPrefab_2);
		mushroomPrefabs.Add(mushroomPrefab_3);

		// generate terrain
		for (int i=0; i<TerrainSize; i++)
        {
			for (int j=0; j<TerrainSize; j++)
            {
				CreateChildPrefab(TerrainPrefab, TerrainParent, i * 19, 0, j*19);
			}
			
		}

		//object to generate
		// order: 1-grass, 2-flower, 3-mushroom, 4-tree, 5-rock
		int obj = 1;
		int quantity = 6;

		//generate random game objects
		for (int i = 1; i < (TerrainSize*TerrainSize) *19 - 1; i++)
		{
			for (int j = 1; j < (TerrainSize * TerrainSize) * 19 - 1; j++)
			{
				int n;
				if (quantity <= 0)
                {
					n = random.Next(25);
					if (n <= 6 && obj != 1)
					{
						//will generate grass
						obj = 1;
						quantity = random.Next(10);
					}
					else if (n <= 10 && obj != 2)
					{
						//will generate flowers
						obj = 2;
						quantity = random.Next(8);
					}
					else if (n <= 16 && obj != 3)
					{
						obj = 3;
						quantity = random.Next(6);
					}
					else if (n <= 20 && obj != 4)
					{
						obj = 4;
						quantity = random.Next(10);
					}
					else if(obj != 5)
					{
						obj = 5;
						quantity = random.Next(2);
					}
				}

				if (obj == 1)
                {
					n = random.Next(2);
					CreateChildPrefab(grassPrefabs[n], TerrainParent, random.Next(i-1, i+1), 2, random.Next(j - 1, j + 1));
					quantity--;
					j++;
				}
				else if (obj == 2)
                {
					n = random.Next(4);
					CreateChildPrefab(treePrefabs[n], TerrainParent, random.Next(i - 1, i + 1), 2, random.Next(j - 1, j + 1));
					quantity--;
					j++;
				}
				else if (obj == 3)
                {
					n = random.Next(2);
					CreateChildPrefab(rockPrefabs[n], TerrainParent, random.Next(i - 1, i + 1), 2, random.Next(j - 1, j + 1));
					quantity--;
					j += 5;
				}
				else if (obj == 4)
                {
					n = random.Next(4);
					CreateChildPrefab(mushroomPrefabs[n], TerrainParent, random.Next(i - 1, i + 1), 2, random.Next(j - 1, j + 1));
					quantity--;
					j += 2;
				}
				else if (obj == 5)
                {
					n = random.Next(2);
					CreateChildPrefab(flowerPrefabs[n], TerrainParent, random.Next(i - 1, i + 1), 2, random.Next(j - 1, j + 1));
					quantity--;
					j += 1;
				}
					
			}

		}

		//characterController.transform.SetPositionAndRotation(
		//new Vector3(0, 1, 0), Quaternion.identity
		//);
	}


	// allow us to instantiate something and immediately make it the child of this game object's
	// transform, so we can containerize everything. also allows us to avoid writing Quaternion.
	// identity all over the place, since we never spawn anything with rotation
	void CreateChildPrefab(GameObject prefab, GameObject parent, int x, int y, int z)
	{
		var myPrefab = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
		myPrefab.transform.parent = parent.transform;
	}
}
