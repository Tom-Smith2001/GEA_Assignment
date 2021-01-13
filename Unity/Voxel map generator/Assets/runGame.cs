using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runGame : MonoBehaviour
{
	public GameObject terrain;
	public GameObject player;
	public GameObject chunk_prefab;
	public Material grass;
	public GameObject chunk_1;
	public GameObject amp_value;
	public GameObject freq_value;
	public GameObject size_value;
	public int mat_option = 1;
	public int size_option = 1;
	public GameObject go_text;
	public GameObject game_ui;

	void Start()
	{
		init();
	}

	public void init()
	{
		chunk_prefab.GetComponent<MeshRenderer>().material = grass;
		chunk_prefab.GetComponent<MeshGenerator>().world_size = 3;
		chunk_prefab.GetComponent<MeshGenerator>().amp = 10;
		chunk_prefab.GetComponent<MeshGenerator>().freq = 20;
		chunk_1.GetComponent<MeshRenderer>().material = grass;
		chunk_1.GetComponent<MeshGenerator>().world_size = 3;
		chunk_1.GetComponent<MeshGenerator>().amp = 10;
		chunk_1.GetComponent<MeshGenerator>().freq = 20;
		player.transform.position = new Vector3(0.0f, 100.0f, 0.0f);
		player.SetActive(false);
		terrain.SetActive(false);
		Cursor.visible = true;
		mat_option = 1;
		size_option = 1;
	}
	


	void Update()
	{

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		freq_value.GetComponent<Text>().text = "" + (chunk_prefab.GetComponent<MeshGenerator>().freq);
		amp_value.GetComponent<Text>().text = "" + (chunk_prefab.GetComponent<MeshGenerator>().amp);
		size_value.GetComponent<Text>().text = "" + (chunk_prefab.GetComponent<MeshGenerator>().world_size * 50) + " X " + (chunk_prefab.GetComponent<MeshGenerator>().world_size * 50);
	}


	public void run()
	{
		Cursor.visible = false;
		//go_text.GetComponent<Text>().text = "Generating...";
		player.SetActive(true);
		terrain.SetActive(true);
		chunk_prefab.GetComponent<MeshGenerator>().seed = Random.Range(0.0f,254.0f);
		//chunk_1.GetComponent<MeshGenerator>().seed = Random.Range(0.0f,254.0f);
//		if(chunk_1.GetComponent<MeshGenerator>().generated)
//		{
//			chunk_1.GetComponent<MeshGenerator>().gen_world();
//		}
		game_ui.SetActive(true);
		this.gameObject.SetActive(false);

	}
}
