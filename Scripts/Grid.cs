using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public GameObject tile;
	public int width = 12;
	public int height = 12;

	public Material[] materials;
	private Renderer rend;

	public int range = 12;
	public int margin = 2;

	public Int2D startTile = new Int2D ();
	public Int2D endTile = new Int2D ();

	public Tile startTileObject = new Tile();
	public Tile endTileObject = new Tile();

	public GameObject viking;



	public GameObject[,] grid = new GameObject[12, 12];

	void Awake()	{
		for (int x = 0; x < width; x++) {
			for(int z = 0; z < height; z++)	{
				GameObject gridPlane = (GameObject)Instantiate (tile);

				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x, 
					gridPlane.transform.position.y, gridPlane.transform.position.z + z);

				grid [x, z] = gridPlane;

				grid [x, z].GetComponent<Tile> ().setTileInfo (new Int2D (x, z), TileType.EMPTY, 0);
			}
		}//for

		MakeRandomTerminalTile ();
	}//Awake 

	void Update()	{
		
	}//Update

	public void MakeCross(int x, int y, int yRotation)	{
		grid [x, y].GetComponent<Renderer> ().sharedMaterial = materials [1];

		Vector3 rot = transform.rotation.eulerAngles;
		rot.y = yRotation;
		grid [x, y].GetComponent<Transform> ().rotation = Quaternion.Euler(rot);
		grid [x, y].GetComponent<Tile> ().setTileInfo (new Int2D (x, y), TileType.CROSS, yRotation);
	}//MakeCross

	public void MakeStraight(int x, int y, int yRotation)	{
		grid [x, y].GetComponent<Renderer> ().sharedMaterial = materials [2];


		// 0 && 180 - horizontal
		// 90 && 270 - vertical

		Vector3 rot = transform.rotation.eulerAngles;
		rot.y = yRotation;
		grid [x, y].GetComponent<Transform> ().rotation = Quaternion.Euler(rot);
		grid [x, y].GetComponent<Tile> ().setTileInfo (new Int2D (x, y), TileType.STRAIGHT, yRotation);
	}//

	public void MakeL(int x, int y, int yRotation)	{
		grid [x, y].GetComponent<Renderer> ().sharedMaterial = materials [3];

		Vector3 rot = transform.rotation.eulerAngles;

		//swap value of 180 and 0
		if (yRotation == 180)
			yRotation = 0;
		else if (yRotation == 0)
			yRotation = 180;
		rot.y = yRotation;

		grid [x, y].GetComponent<Transform> ().rotation = Quaternion.Euler(rot);
		grid [x, y].GetComponent<Tile> ().setTileInfo (new Int2D (x, y), TileType.L, yRotation);
	}//


	public void MakeRandomTerminalTile()	{

		startTile = SelectRandomTile (range, margin);
		endTile	= SelectRandomTile (range, margin);

		//print (startTile.XYOut ());
		//print (endTile.XYOut ());

		while (startTile.y >= height)
			startTile = SelectRandomTile (range, margin);

		while (startTile == endTile) {
			endTile = SelectRandomTile (range, margin);
		}//while

		int m = Random.Range (1, 4);
		Int2D[] tile = SelectRandomTile (m);
		startTile = tile [0];
		endTile = tile [1];

		grid [startTile.x, startTile.y].GetComponent<Renderer> ().sharedMaterial = materials [4];
		grid [endTile.x, endTile.y].GetComponent<Renderer> ().sharedMaterial = materials [5];

		// set start tile info
		grid[startTile.x, startTile.y].GetComponent<Tile>().setTileInfo(new Int2D(startTile.x, startTile.y), TileType.START, 0);
		startTileObject = grid [startTile.x, startTile.y].GetComponent<Tile> ();



		// set end tile info
		grid[endTile.x, endTile.y].GetComponent<Tile>().setTileInfo(new Int2D(endTile.x, endTile.y), TileType.END, 0);
		endTileObject = grid [endTile.x, endTile.y].GetComponent<Tile> ();


		viking = Instantiate (viking) as GameObject;
		viking.transform.position = grid [startTile.x, startTile.y].transform.position;
	}//MakeRandomTerminalTile

	public GameObject GetTile(int x, int y)	{
		return grid [x, y];
	}//getTile


	private Int2D[] SelectRandomTile()	{
		return SelectRandomTile (1);
	}//SelectRandom()

	private Int2D[] SelectRandomTile(int margin)	{
		// margin means it will be the gap of the random generated tiles from the edge of the board
		Int2D[] tile = new Int2D[2];
		tile[0] = new Int2D();
		tile[1] = new Int2D();

		List<Int2D> dNodes = new List<Int2D>();
		List<Int2D> hNodes = new List<Int2D>();
		List<Int2D> vNodes = new List<Int2D>();

		dNodes.Add(new Int2D(margin, margin));
		dNodes.Add(new Int2D(margin, height - (margin + 1)));
		dNodes.Add(new Int2D(width - (margin + 1), margin));
		dNodes.Add(new Int2D(width - (margin + 1), height - (margin + 1)));

		hNodes.Add(new Int2D((int)height/2, margin));
		hNodes.Add(new Int2D((int)width/2, width - (margin + 1)));

		vNodes.Add(new Int2D(margin, (int)height/2));
		vNodes.Add(new Int2D(width - (margin + 1), (int)height/2));


		switch(Random.Range (0, 3))	{
		case 0:// diagonal
			print("DIG");
			tile = RandomizeList(dNodes, tile);
			break;
		case 1:// horizontal
			tile = RandomizeList(hNodes, tile);
			break;
		case 2:// vertical
			tile = RandomizeList(vNodes, tile);
			break;
		}

		return tile;
	}//generateRandomTile2

	Int2D[] RandomizeList(List<Int2D> list, Int2D[] tileIn)	{
		int r1 = Random.Range(0, list.Count);
		int r2 = Random.Range(0, list.Count);
		while(r2 == r1)
			r2 = Random.Range(0, list.Count);
		tileIn[0] = list[r1];
		tileIn[1] = list[r2];
		return tileIn;
	}

	private Int2D SelectRandomTile(int range, int margin)	{
		Int2D tile = new Int2D ();

		while (true) {
			tile.x = Random.Range (0, range);
			tile.y = Random.Range (0, range);

			bool xRestrict = (tile.x >= margin && tile.x <= range - margin);
			bool yRestrict = (tile.y >= margin && tile.y <= range - margin);

			if(yRestrict && xRestrict)
				continue;
			else
				break;
		}//while

		return tile;
	}//generateRandomTile
		

}//Script
