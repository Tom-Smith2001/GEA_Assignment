using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

	//init chunk size
	public int x_blocks = 50;
	public int z_blocks = 50;
	public int world_size = 5;


	//perlin variables
	public float amp = 3.0f;
	public float freq = 6.0f;
	public float seed = 12;

	public bool generated = false;

//	bool gen_left = false;
//	bool gen_right = false;
//	bool gen_back = false;
//	bool gen_fwd = false;

	public GameObject player;
	public GameObject chunk;
	private GameObject clone;

//	void Awake()
//	{
//		gen_world();
//		generated = true;
//	}

	// Start is called before the first frame update
	public void Awake()
	{
		//seed = Random.Range(0,254);
		player = GameObject.FindGameObjectWithTag("Player");
		if(this.transform.position.x == 0 && this.transform.position.z == 0)
		{
//			Instantiate(chunk, new Vector3(this.transform.position.x + 25, 0.0f, this.transform.position.z), Quaternion.identity);
//			Instantiate(chunk, new Vector3(this.transform.position.x - 25, 0.0f, this.transform.position.z), Quaternion.identity);
//			Instantiate(chunk, new Vector3(this.transform.position.x, 0.0f, this.transform.position.z + 25), Quaternion.identity);
//			Instantiate(chunk, new Vector3(this.transform.position.x, 0.0f, this.transform.position.z - 25), Quaternion.identity);
//			Instantiate(chunk, new Vector3(this.transform.position.x + 25, 0.0f, this.transform.position.z - 25), Quaternion.identity);
//			Instantiate(chunk, new Vector3(this.transform.position.x + 25, 0.0f, this.transform.position.z + 25), Quaternion.identity);
//			Instantiate(chunk, new Vector3(this.transform.position.x - 25, 0.0f, this.transform.position.z -25), Quaternion.identity);
//			Instantiate(chunk, new Vector3(this.transform.position.x - 25, 0.0f, this.transform.position.z + 25), Quaternion.identity);



			for(int z = 0; z < world_size; z++)
			{
				for(int x = 0; x < world_size; x++ )
				{
					if(x > 0 || z > 0)
					{
						clone = Instantiate(chunk, new Vector3(this.transform.position.x + (x * 25), 0.0f, this.transform.position.z + (z * 25)), Quaternion.identity);
						clone.tag = "Chunk";
					
					}
				}
			}

		}
		if(!generated)
		{
			generate();
		}

		//Debug.Log("chunk at " + this.transform.position.x + "x, player at " + player.transform.position.x + "x");


	}

	// Update is called once per frame
//	void Update()
//	{
//		
//		if(generated && ((player.transform.position.x > this.transform.position.x + 75) 
//			|| (player.transform.position.x < this.transform.position.x - 75) 
//			|| (player.transform.position.z > this.transform.position.z + 75) 
//			|| (player.transform.position.z < this.transform.position.z - 75)))
//		{
//			this.transform.GetComponent<MeshRenderer>().enabled = false;
//			//Debug.Log("DELETING: " + this.transform.position.x + "x, player at " + player.transform.position.x + "x");
//		}
//		else
//		{
//			
//			this.transform.GetComponent<MeshRenderer>().enabled = true;
//			if(!gen_left)
//			{
//				Instantiate(chunk, new Vector3(this.transform.position.x + 25, 0.0f, this.transform.position.z), Quaternion.identity);
//				gen_left = true;
//			}
//			if(!gen_right)
//			{
//				Instantiate(chunk, new Vector3(this.transform.position.x - 25, 0.0f, this.transform.position.z), Quaternion.identity);
//				gen_right = true;
//			}
//			if(!gen_back)
//			{
//				Instantiate(chunk, new Vector3(this.transform.position.x, 0.0f, this.transform.position.z + 25), Quaternion.identity);
//				gen_back = true;
//			}
//			if(!gen_fwd)
//			{
//				Instantiate(chunk, new Vector3(this.transform.position.x, 0.0f, this.transform.position.z - 25), Quaternion.identity);
//				gen_fwd = true;
//			}
//
//		}
//	}

	void generate()
	{
		//Debug.Log("Hello");
		Vector3 block_size = new Vector3(1.0f,10.0f,1.0f);
		GameObject[] blocks = new GameObject[x_blocks * z_blocks];
		Vector3 my_pos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		int i = -1;
		for(int x = 0; x < x_blocks; x++)
		{
			for(int z = 0; z < z_blocks; z++)
			{
				//to iterate through an array of cubes as they generate
				i++;
				blocks[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				// sets a position to place each cube
				my_pos = this.transform.position;
				//Debug.Log("Block " + i + " pos: " + my_pos);
				my_pos.y = 0.0f;
				my_pos.x -= x_blocks/2 * block_size.x;
				my_pos.z -= z_blocks/2 * block_size.z;

				my_pos.x += x * block_size.x;
				my_pos.z += z * block_size.z;
				//seed = Random.Range(0,254);
				//perlin noise function to set the Y position of a cube, added an offset of 999999 so the value is always positive (negatives lead to a mirrored pattern)
				my_pos.y += Mathf.PerlinNoise((seed + 999999.0f + (this.transform.position.x + my_pos.x))/freq,(999999.0f + (this.transform.position.z + my_pos.z))/freq) * amp;
				//round the result so each cube has an integer vertical offset to the adjacent cubes
				my_pos.y = Mathf.Round(my_pos.y);

				//place the cube in the position
				blocks[i].transform.position = my_pos;
				blocks[i].transform.localScale = block_size;
				//makes the generator a parent to each block generated
				blocks[i].transform.parent = transform;
			}
		}
		//function to form a mesh from the voxel terrain of the cubes
		formMeshes();
		//now the mesh has been molded, destroy the cubes for performance
		for(i = 0; i < blocks.Length; i++)
		{
			blocks[i].transform.SetParent(null);
			Destroy(blocks[i]);
			//blocks[i].SetActive(false);
		}
		//generated = true;
		


	}
	void formMeshes()
	{
		//goes through every cube and stores its mesh filter in an array
		MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combined = new CombineInstance[filters.Length];

		for(int i = 0; i < filters.Length; i++)
		{
			combined[i].mesh = filters[i].sharedMesh;
			combined[i].transform = filters[i].transform.localToWorldMatrix;
			//filters[i].gameObject.SetActive(false);
		}

		if(this.gameObject.GetComponent<MeshFilter>() == null)
		{
			this.transform.gameObject.AddComponent<MeshFilter>();
		}

		this.transform.GetComponent<MeshFilter>().mesh = new Mesh();
		this.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combined,true);
		this.transform.GetComponent<MeshFilter>().mesh.RecalculateBounds();
		this.transform.GetComponent<MeshFilter>().mesh.RecalculateNormals();

		this.transform.gameObject.AddComponent<MeshCollider>();

		if(this.gameObject.GetComponent<MeshRenderer>() == null)
		{
			this.gameObject.AddComponent<MeshRenderer>();
		}

		this.transform.gameObject.SetActive(true);


	}
}
