using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
	public GameObject player;
	public Material grass;
	public Material stone;
	public Material sand;
	public GameObject chunk_1;
	public GameObject[] chunks;
	private int type;
	public GameObject chunk_prefab;
	public GameObject menu;
	public GameObject game_ui;
	public GameObject mat_button;


	void Update()
	{
		chunk_1.GetComponent<MeshGenerator>().freq = chunk_prefab.GetComponent<MeshGenerator>().freq ;
		chunk_1.GetComponent<MeshGenerator>().amp = chunk_prefab.GetComponent<MeshGenerator>().amp ;
		chunk_1.GetComponent<MeshGenerator>().world_size = chunk_prefab.GetComponent<MeshGenerator>().world_size ;
		if(game_ui.active)
		{
			if(Input.GetKeyDown("x"))
			{
//				chunks = GameObject.FindGameObjectsWithTag("Chunk");
//				foreach(GameObject chunk in chunks)
//				{
//					Debug.Log("destroying chunk");
//					Destroy(chunk);
//				}
//				//Destroy(chunk_1.GetComponent<MeshFilter>());
//				//Destroy(chunk_1.GetComponent<MeshCollider>());
//				player.SetActive(false);
//				Cursor.visible = true;
//				menu.SetActive(true);
//				menu.GetComponent<runGame>().init();
				SceneManager.LoadScene("Scene");

			}
		}
	}


	public void ampUp(int scale)
	{
		chunk_prefab.GetComponent<MeshGenerator>().amp += scale;
		if(chunk_prefab.GetComponent<MeshGenerator>().amp > 100)
		{
			chunk_prefab.GetComponent<MeshGenerator>().amp = 100;
		}
	}

	public void ampDown(int scale)
	{
		chunk_prefab.GetComponent<MeshGenerator>().amp -= scale;
		if(chunk_prefab.GetComponent<MeshGenerator>().amp < 1)
		{
			chunk_prefab.GetComponent<MeshGenerator>().amp = 1;
		}
	}

	public void freqUp(int scale)
	{
		chunk_prefab.GetComponent<MeshGenerator>().freq += scale;
		if(chunk_prefab.GetComponent<MeshGenerator>().freq > 400)
		{
			chunk_prefab.GetComponent<MeshGenerator>().freq = 400;
		}
	}

	public void freqDown(int scale)
	{
		chunk_prefab.GetComponent<MeshGenerator>().freq -= scale;
		if(chunk_prefab.GetComponent<MeshGenerator>().freq < 2)
		{
			chunk_prefab.GetComponent<MeshGenerator>().freq = 2;
		}
	}

	public void changeMat()
	{
		if(menu.GetComponent<runGame>().mat_option < 3)
		{
			menu.GetComponent<runGame>().mat_option += 1;
		}
		else
		{
			menu.GetComponent<runGame>().mat_option = 1;
		}
		if(menu.GetComponent<runGame>().mat_option == 1)
		{
			mat_button.GetComponent<Text>().text = "GrassLands";
			chunk_prefab.GetComponent<MeshRenderer>().material = grass;
			chunk_1.GetComponent<MeshRenderer>().material = grass;
		}
		if(menu.GetComponent<runGame>().mat_option == 2)
		{
			mat_button.GetComponent<Text>().text = "Mountains";
			chunk_prefab.GetComponent<MeshRenderer>().material = stone;
			chunk_1.GetComponent<MeshRenderer>().material = stone;
		}
		if(menu.GetComponent<runGame>().mat_option == 3)
		{
			mat_button.GetComponent<Text>().text = "Desert";
			chunk_prefab.GetComponent<MeshRenderer>().material = sand;
			chunk_1.GetComponent<MeshRenderer>().material = sand;
		}
	}

	public void sizeUp()
	{
		if(menu.GetComponent<runGame>().size_option < 3)
		{
			menu.GetComponent<runGame>().size_option += 1;
		}
		else
		{
			menu.GetComponent<runGame>().size_option = 1;
		}
		if(menu.GetComponent<runGame>().size_option == 1)
		{
			chunk_prefab.GetComponent<MeshGenerator>().world_size = 3;
		}
		if(menu.GetComponent<runGame>().size_option == 2)
		{
			chunk_prefab.GetComponent<MeshGenerator>().world_size = 4;
		}
		if(menu.GetComponent<runGame>().size_option == 3)
		{
			chunk_prefab.GetComponent<MeshGenerator>().world_size = 5;
		}
	}
	public void sizeDown()
	{
		if(menu.GetComponent<runGame>().size_option > 1)
		{
			menu.GetComponent<runGame>().size_option -= 1;
		}
		else
		{
			menu.GetComponent<runGame>().size_option = 3;
		}
		if(menu.GetComponent<runGame>().size_option == 1)
		{
			chunk_prefab.GetComponent<MeshGenerator>().world_size = 3;
		}
		if(menu.GetComponent<runGame>().size_option == 2)
		{
			chunk_prefab.GetComponent<MeshGenerator>().world_size = 4;
		}
		if(menu.GetComponent<runGame>().size_option == 3)
		{
			chunk_prefab.GetComponent<MeshGenerator>().world_size = 5;
		}
	}
	public void quit()
	{
		Application.Quit();
	}

}
