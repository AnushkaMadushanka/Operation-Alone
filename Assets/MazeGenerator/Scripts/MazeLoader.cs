using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public GameObject floor;
	public float size = 2f;
	public float height = 3f;
	public GameObject playerPrfab;

	private MazeCell[,] mazeCells;

	public GameObject enemySpawnPointHolder;
	public GameObject enemySpawnPointPrefab;
	public int spawnCount;

	// Use this for initialization
	public void GenerateMaze () {
		ClearMaze ();
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();

		Instantiate (playerPrfab, GameObject.Find ("Floor 0,0").transform.position, Quaternion.identity);
	}

	public void GenerateSpawnPoints(){
		var spawnPoints = enemySpawnPointHolder.GetComponentsInChildren<EnemySpawnPoint> ();
		foreach (var point in spawnPoints) {
			GameObject.DestroyImmediate (point.gameObject);
		}
		for(var i=0; i < spawnCount; i++){
			var spawnPoint = Instantiate (enemySpawnPointPrefab);
			var position = GameObject.Find ("Floor " + Random.Range (0, mazeColumns) + "," + Random.Range (0, mazeRows)).transform.position;
			position.y = -1.193f;
			spawnPoint.transform.position = position;
			spawnPoint.transform.parent = enemySpawnPointHolder.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				// For now, use the same wall object for the floor!
				mazeCells [r, c] .floor = Instantiate (floor, new Vector3 (r*size, -(height/2f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);
				mazeCells [r, c].floor.transform.parent = transform;
				mazeCells [r, c].floor.gameObject.isStatic = true;

				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
					mazeCells [r, c].westWall.transform.parent = transform;
					mazeCells [r, c].westWall.gameObject.isStatic = true;
				}

				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;
				mazeCells [r, c].eastWall.transform.parent = transform;
				mazeCells [r, c].eastWall.gameObject.isStatic = true;

				if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
					mazeCells [r, c].northWall.transform.parent = transform;
					mazeCells [r, c].northWall.gameObject.isStatic = true;

				}

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
				mazeCells [r, c].southWall.transform.parent = transform;
				mazeCells [r, c].southWall.gameObject.isStatic = true;

			}
		}
	}

	public void ClearMaze(){
		var tiles = transform.GetComponentsInChildren<BoxCollider> ();
		foreach (var tile in tiles) {
			GameObject.DestroyImmediate (tile.gameObject);
		}
	}
}
