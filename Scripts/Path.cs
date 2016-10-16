using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	Grid grid;
	public List<Int2D> pathList = new List<Int2D>();
	Tile currentTile = new Tile ();

	private void Awake()	{
		grid = this.gameObject.GetComponent<Grid> ();
		pathList.Add (grid.startTile); //Int2D startNode
		currentTile = grid.startTileObject;
	}


	public void AnalyzePath (Tile inputTile)	{
		/*if(IsConnected(inputTile))	{
			pathList.Add(new Int2D(inputTile.tile.x, inputTile.tile.y));
			currentTile = inputTile;
		}*/



		/*
		// check adjacent down from start
		Tile downStart  = grid.GetTile(grid.startTile.x, grid.startTile.y - 1).GetComponent<Tile>();
		if(IsConnected(downStart))	{
			pathList.Add(new Int2D(downStart.tile.x, downStart.tile.y));
			currentTile = downStart;
			print ("From Start connected DOWN");
		}//if
		*/	

		pathList.Clear ();
		pathList.Add (grid.startTile); //Int2D startNode
		currentTile = grid.startTileObject;
		Tile up, down, left, right;
		bool connected = true;
		try {
			while (connected) {
				up = grid.GetTile(currentTile.tile.x, currentTile.tile.y - 1).GetComponent<Tile>();
				down = grid.GetTile(currentTile.tile.x, currentTile.tile.y + 1).GetComponent<Tile>();
				left = grid.GetTile(currentTile.tile.x - 1, currentTile.tile.y).GetComponent<Tile>();
				right = grid.GetTile(currentTile.tile.x + 1, currentTile.tile.y).GetComponent<Tile>();


				// check adjacent
				if (IsConnected (down) && !pathList.Contains(new Int2D(down.tile.x, down.tile.y))) {
					print("Connected DOWN");
					pathList.Add (new Int2D(down.tile.x, down.tile.y));
					currentTile = down;
					continue;
				}//if

				if (IsConnected (up) && !pathList.Contains(new Int2D(up.tile.x, up.tile.y))) {
					print("Connected UP");
					pathList.Add (new Int2D(up.tile.x, up.tile.y));
					currentTile = up;
					continue;
				}//if

				if (IsConnected (left) && !pathList.Contains(new Int2D(left.tile.x, left.tile.y))) {
					print("Connected LEFT");
					pathList.Add (new Int2D(left.tile.x, left.tile.y));
					currentTile = left;
					continue;
				}//if
				if (IsConnected (right) && !pathList.Contains(new Int2D(right.tile.x, right.tile.y))) {
					print("Connected RIGHT");
					pathList.Add (new Int2D(right.tile.x, right.tile.y));
					currentTile = right;
					continue;
				}//if

				connected = false;
				print("disconnected");

			}//while
		} catch (System.Exception ex) {
			
		}



		/*
		print ("END TILE: " + grid.endTileObject.tile.y);
		print ("INPUT TILE: " + inputTile.tile.y);
		print ("X END TILE: " + grid.endTileObject.tile.x);
		print ("X INPUT TILE: " + inputTile.tile.x);
		if (grid.endTileObject.tile.y == inputTile.tile.y + 1 && grid.endTileObject.tile.x == inputTile.tile.x) {
			print ("FOUND END TILE");
			if (IsConnected (grid.endTileObject)) {
				print ("CONNECTED");
				pathList.Add (new Int2D (grid.endTileObject.tile.x, grid.endTileObject.tile.y));
				currentTile = grid.endTileObject;
			}	
		}*/

	}//AnalyzePath

	public Int2D FindNextPath(Int2D inputTile)	{
		Tile up, down, left, right;
		up = grid.GetTile (inputTile.x, inputTile.y + 1).GetComponent<Tile>();
		down = grid.GetTile (inputTile.x, inputTile.y - 1).GetComponent<Tile>();
		left = grid.GetTile (inputTile.x + 1, inputTile.y).GetComponent<Tile>();
		right = grid.GetTile (inputTile.x - 1, inputTile.y).GetComponent<Tile>();

		if (IsConnected (up) && grid.GetTile (up.tile.x, up.tile.y).GetComponent<Tile> ().visited == false)	{
			return up.tile;
			print("Up");
		}
		else if (IsConnected (down) && grid.GetTile (down.tile.x, down.tile.y).GetComponent<Tile> ().visited == false)	{
			return down.tile;
			print("down");
		}
		else if (IsConnected (left) && grid.GetTile (left.tile.x, left.tile.y).GetComponent<Tile> ().visited == false)	{
			return left.tile;
			print("left");
		}
		else if (IsConnected (right) && grid.GetTile (right.tile.x, right.tile.y).GetComponent<Tile> ().visited == false)	{
			return right.tile;
			print("right");
		}

		print ("DISCONNECTED OR END!");
		return null;

	}


	bool IsConnected(Tile inputTile)	{
		int x = currentTile.tile.x;
		if (currentTile.tile.x == inputTile.tile.x &&
			currentTile.tile.y + 1 == inputTile.tile.y &&
			currentTile.HasNorth() && inputTile.HasDown()) // check adjacent up
			return true;
		
		if (currentTile.tile.x - 1 == inputTile.tile.x &&
			currentTile.tile.y == inputTile.tile.y &&
			currentTile.HasLeft() && inputTile.HasRight()) // check adjacent left
			return true;

		if (currentTile.tile.x == inputTile.tile.x &&
			currentTile.tile.y - 1 == inputTile.tile.y &&
			currentTile.HasDown() && inputTile.HasNorth()) // check adjacent down
			return true;

		if (currentTile.tile.x + 1 == inputTile.tile.x &&
			currentTile.tile.y == inputTile.tile.y &&
			currentTile.HasRight() && inputTile.HasLeft()) // check adjacent right
			return true;
		
		return false;
	}//IsConnected

	bool CheckTypeConnection(Tile inputTile)	{
		return false;
	}
}
