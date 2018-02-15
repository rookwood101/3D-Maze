using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeArranger : MonoBehaviour {

	enum CubeFace {Right, Left, Top, Bottom, Front, Back};

	// Maze wall prefab (currently a cube)
	[SerializeField]
	private GameObject mazeWall;

	// The parent object that holds the entire maze cube.
	[SerializeField]
	private GameObject mazeParent;

	// The absolute value of the biggest/smallest x/y/z position any wall block can be in.
	// Equal to the big centre cube's width/2 + 0.5.
	// Currently the cube is width 8, allowing 10x10 walls. 
	private float mazeMax = 4.5f;

	// A made up maze.
	// TODO: procedural generation of mazes.
	// TODO: consider using a larger maze.
	private int[][] aMaze = new int[][] {
		new int[] {1, 1, 1, 1, 0, 0, 1, 1, 1, 1},
		new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 1},
		new int[] {1, 0, 1, 1, 0, 1, 0, 1, 1, 1},
		new int[] {1, 0, 0, 0, 0, 1, 1, 1, 0, 1},
		new int[] {0, 1, 0, 1, 1, 1, 0, 0, 0, 0},
		new int[] {0, 1, 0, 0, 0, 1, 1, 1, 0, 0},
		new int[] {1, 1, 1, 1, 0, 0, 0, 1, 0, 1},
		new int[] {1, 0, 0, 1, 0, 1, 0, 1, 0, 1},
		new int[] {1, 0, 0, 0, 0, 1, 0, 0, 0, 1},
		new int[] {1, 1, 1, 1, 0, 0, 1, 1, 1, 1},
	};

	// At the start, generate a maze for each face.
	void Start () {
		GenerateMazeForFace (CubeFace.Right);
		GenerateMazeForFace (CubeFace.Left);
		GenerateMazeForFace (CubeFace.Top);
		GenerateMazeForFace (CubeFace.Bottom);
		GenerateMazeForFace (CubeFace.Front);
		GenerateMazeForFace (CubeFace.Back);
	}

	// Places mazeWalls (cubes) in the pattern of the maze.
	// Looks up which maze to use for each face.
	// The maze is placed like so on each face:
	// s → w
	// ↓ w w
	// w w w
	void GenerateMazeForFace(CubeFace face) {
		// start: Where the first row, first column wall of the maze should go, for this face.
		// The location 's' in the diagram above.
		Vector3 start = Vector3.zero;
		// nextColumnDirection: Which direction is the next columnwise block in
		// The direction '→' in the diagram above.
		Vector3 nextColumnDirection = Vector3.zero;
		// nextColumnDirection: Which direction is the next columnwise block in
		// The direction '→' in the diagram above.
		Vector3 nextRowDirection = Vector3.zero;


        int[][] maze = GetMazeForFace (face);

		// Where in world space should the first column, first row wall be placed.
		// What direction in world space should the next column and next row be.
		// All CubeFace directions are relative to the starting camera (e.g.
		// CubeFace.Front is facing the camera).
		// The column 1, row 1 location for each face is chosen by imagining the big cube as this net:
		//
		//    Ba
		//    To
		// Le Fr Ri
		//    Bo
		//
		// The top left corner of each face on the net is then used as the start location, mapped to world space
		switch (face) {
		case CubeFace.Right:
			start = new Vector3 (mazeMax, mazeMax, -mazeMax);
			nextColumnDirection = Vector3.forward;
			nextRowDirection = Vector3.down;
			break;
		case CubeFace.Left:
			start = new Vector3 (-mazeMax, mazeMax, mazeMax);
			nextColumnDirection = Vector3.back;
			nextRowDirection = Vector3.down;
			break;
		case CubeFace.Top:
			start = new Vector3 (-mazeMax, mazeMax, mazeMax);
			nextColumnDirection = Vector3.right;
			nextRowDirection = Vector3.back;
			break;
		case CubeFace.Bottom:
			start = new Vector3 (-mazeMax, -mazeMax, -mazeMax);
			nextColumnDirection = Vector3.right;
			nextRowDirection = Vector3.forward;
			break;
		case CubeFace.Front:
			start = new Vector3 (-mazeMax, mazeMax, -mazeMax);
			nextColumnDirection = Vector3.right;
			nextRowDirection = Vector3.down;
			break;
		case CubeFace.Back:
			start = new Vector3 (-mazeMax, -mazeMax, mazeMax);
			nextColumnDirection = Vector3.right;
			nextRowDirection = Vector3.up;
			break;
		}

		for (int row = 0; row < maze.Length; row++) {
			for (int column = 0; column < maze [row].Length; column++) {
				if (maze [row] [column] == 1) {
					// If there should be a cube,
					// place it in the correct location as a child of the maze parent
					Instantiate (mazeWall, start + (column * nextColumnDirection) + (row * nextRowDirection), mazeWall.transform.rotation, mazeParent.transform);
				}
			}
		}
			
	}

	int[][] GetMazeForFace(CubeFace face) {
		// Currently each face of the cube uses the same maze
		// TODO: Calculate and lookup maze for each face.
		return aMaze;
	}

	void Update () {
		
	}
}
