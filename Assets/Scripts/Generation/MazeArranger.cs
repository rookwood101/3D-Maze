using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MazeArranger : MonoBehaviour
{

    enum CubeFace { Right, Left, Top, Bottom, Front, Back };

    enum Direction {Up = 1, Down = 2, Right = 4, Left = 8};
    
    // Maze wall prefab
    [SerializeField]
    private GameObject mazeWallTop;
    [SerializeField]
    private GameObject mazeWallRight;
    // Maze pickup prefab (currently a small sphere)
    [SerializeField]
    private GameObject mazePickup;
    // The parent object that holds the entire maze cube.
    [SerializeField]
    private GameObject mazeParent;

    // The absolute value of the biggest/smallest x/y/z position any wall block can be in.
    // Equal to the big centre cube's width/2 + 0.5.
    // Currently the cube is width 8, allowing 10x10 walls. 
    [SerializeField]
    private float mazeMax;
    [SerializeField]
    private int mazeGenerationWidth;
    [SerializeField]
    private int pickupsPerFace;
    [SerializeField]
    private float mazeWallHeight;

    private System.Random rnd;
    private Dictionary<CubeFace, RectInt> cubeFaceBounds;
    private Dictionary<CubeFace, int[,]> mazes;
    private Direction[] directions = {Direction.Up, Direction.Down, Direction.Right, Direction.Left};

    private Dictionary<Direction, Vector2Int> directionVectors;
    private Dictionary<Direction, Direction> oppositeDirections;

    private Dictionary<Direction, Direction> cw90Directions;
    private Dictionary<Direction, Direction> ccw90Directions;

    private LevelController levelController;

    // At the start, generate a maze for each face.
    void Start()
    {
        int w = mazeGenerationWidth;
        rnd = new System.Random();
        cubeFaceBounds = new Dictionary<CubeFace, RectInt>();
        cubeFaceBounds[CubeFace.Right] = new RectInt(w, 0, w, w);
        cubeFaceBounds[CubeFace.Left] = new RectInt(-w, 0, w, w);
        cubeFaceBounds[CubeFace.Top] = new RectInt(0, w, w, w);
        cubeFaceBounds[CubeFace.Bottom] = new RectInt(0, -w, w, w);
        cubeFaceBounds[CubeFace.Front] = new RectInt(0, 0, w, w);
        cubeFaceBounds[CubeFace.Back] = new RectInt(0, 2 * w, w, w);

        mazes = new Dictionary<CubeFace, int[,]>();
        mazes[CubeFace.Right] = new int[w, w];
        mazes[CubeFace.Left] = new int[w, w];
        mazes[CubeFace.Top] = new int[w, w];
        mazes[CubeFace.Bottom] = new int[w, w];
        mazes[CubeFace.Front] = new int[w, w];
        mazes[CubeFace.Back] = new int[w, w];

        directionVectors = new Dictionary<Direction, Vector2Int>();
        directionVectors[Direction.Up] = new Vector2Int(0, 1);
        directionVectors[Direction.Down] = new Vector2Int(0, -1);
        directionVectors[Direction.Right] = new Vector2Int(1, 0);
        directionVectors[Direction.Left] = new Vector2Int(-1, 0);
        oppositeDirections = new Dictionary<Direction, Direction>();
        oppositeDirections[Direction.Up] = Direction.Down;
        oppositeDirections[Direction.Down] = Direction.Up;
        oppositeDirections[Direction.Right] = Direction.Left;
        oppositeDirections[Direction.Left] = Direction.Right;
        cw90Directions = new Dictionary<Direction, Direction>();
        cw90Directions[Direction.Up] = Direction.Right;
        cw90Directions[Direction.Down] = Direction.Left;
        cw90Directions[Direction.Right] = Direction.Down;
        cw90Directions[Direction.Left] = Direction.Up;
        ccw90Directions = new Dictionary<Direction, Direction>();
        ccw90Directions[Direction.Up] = Direction.Left;
        ccw90Directions[Direction.Down] = Direction.Right;
        ccw90Directions[Direction.Right] = Direction.Up;
        ccw90Directions[Direction.Left] = Direction.Down;

        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        LevelController.GameMode gameMode = levelController.GetGameMode();
        if (gameMode == LevelController.GameMode.Endless
          ||gameMode == LevelController.GameMode.TimeTrial
          ||(gameMode == LevelController.GameMode.Tutorial && levelController.GetLevelCount() > 0)) {
            GenerateMazeFrom(Vector2Int.zero);
            ArrangeMazeForFace(CubeFace.Right);
            ArrangeMazeForFace(CubeFace.Left);
            ArrangeMazeForFace(CubeFace.Bottom);
            ArrangeMazeForFace(CubeFace.Front);
            ArrangeMazeForFace(CubeFace.Back);
            ArrangeMazeForFace(CubeFace.Top);
        } else if (gameMode == LevelController.GameMode.Tutorial
                 &&levelController.GetLevelCount() == 0) {
            GenerateTutorialMazeFrom(new Vector2Int(w/2, w + w/2));
            CreateTutorialBorderAndBase();
            ArrangeMazeForFace(CubeFace.Top);
        }
    }

    void CreateTutorialBorderAndBase() {
        // Flattens base cube to match the single generated maze
        Transform cube = GameObject.Find("Main Cube").GetComponent<Transform>();
        cube.localScale = new Vector3(cube.localScale.x, cube.localScale.y * 0.01f, cube.localScale.z);
        cube.localPosition = new Vector3(cube.localPosition.x, cube.localPosition.y + 4, cube.localPosition.z);

        // Creates a border around the top face
        Vector3 start = new Vector3(-mazeMax, mazeMax, -mazeMax);
        Vector3 nextColumnDirection = Vector3.right;
        Vector3 nextRowDirection = Vector3.forward;

        Vector3 wallHeightDirection = -Vector3.Cross(nextColumnDirection, nextRowDirection);
        Quaternion wallRotation = Quaternion.FromToRotation(Vector3.back, wallHeightDirection);
        for (float pos = -0.5f; pos < mazeGenerationWidth-0.5f; pos+=0.5f)
        {
            Instantiate(mazeWallTop, start + (pos * nextColumnDirection) + (-1 * nextRowDirection) + (mazeWallHeight * wallHeightDirection),  wallRotation * mazeWallTop.transform.rotation, mazeParent.transform);
            Instantiate(mazeWallRight, start + (-1 * nextColumnDirection) + (pos * nextRowDirection) + (mazeWallHeight * wallHeightDirection),  wallRotation * mazeWallRight.transform.rotation, mazeParent.transform);
        }
    }

    // Places mazeWalls (cubes) in the pattern of the maze.
    // Looks up which maze to use for each face.
    // The maze is placed like so on each face:
    // s → w
    // ↓ w w
    // w w w
    void ArrangeMazeForFace(CubeFace face)
    {
        // start: Where the first row, first column wall of the maze should go, for this face.
        // The location 's' in the diagram above.
        Vector3 start = Vector3.zero;
        // nextColumnDirection: Which direction is the next columnwise block in
        // The direction '→' in the diagram above.
        Vector3 nextColumnDirection = Vector3.zero;
        // nextColumnDirection: Which direction is the next columnwise block in
        // The direction '→' in the diagram above.
        Vector3 nextRowDirection = Vector3.zero;


        int[,] maze = mazes[face];

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
        // The bottom left corner of each face on the net is then used as the start location, mapped to world space
        switch (face)
        {
            case CubeFace.Right:
                start = new Vector3(mazeMax, -mazeMax, -mazeMax);
                nextColumnDirection = Vector3.forward;
                nextRowDirection = Vector3.up;
                break;
            case CubeFace.Left:
                start = new Vector3(-mazeMax, -mazeMax, mazeMax);
                nextColumnDirection = Vector3.back;
                nextRowDirection = Vector3.up;
                break;
            case CubeFace.Top:
                start = new Vector3(-mazeMax, mazeMax, -mazeMax);
                nextColumnDirection = Vector3.right;
                nextRowDirection = Vector3.forward;
                break;
            case CubeFace.Bottom:
                start = new Vector3(-mazeMax, -mazeMax, mazeMax);
                nextColumnDirection = Vector3.right;
                nextRowDirection = Vector3.back;
                break;
            case CubeFace.Front:
                start = new Vector3(-mazeMax, -mazeMax, -mazeMax);
                nextColumnDirection = Vector3.right;
                nextRowDirection = Vector3.up;
                break;
            case CubeFace.Back:
                start = new Vector3(-mazeMax, mazeMax, mazeMax);
                nextColumnDirection = Vector3.right;
                nextRowDirection = Vector3.down;
                break;
        }

        Vector3 wallHeightDirection = -Vector3.Cross(nextColumnDirection, nextRowDirection);
        Quaternion wallRotation = Quaternion.FromToRotation(Vector3.back, wallHeightDirection);
        
        for (int row = 0; row < maze.GetLength(0); row++)
        {
            for (int column = 0; column < maze.GetLength(1); column++)
            {
                if ((maze[row, column] & (int)Direction.Up) == 0)
                {
                    // If there is not a path downwards,
                    // place a wall at the bottom of this cell
                    Instantiate(mazeWallTop, start + (column * nextColumnDirection) + (row * nextRowDirection) + (mazeWallHeight * wallHeightDirection),  wallRotation * mazeWallTop.transform.rotation, mazeParent.transform);
                }
                if ((maze[row, column] & (int)Direction.Right) == 0)
                {
                    // If there is not a path rightwards
                    Instantiate(mazeWallRight, start + (column * nextColumnDirection) + (row * nextRowDirection) + (mazeWallHeight * wallHeightDirection), wallRotation * mazeWallRight.transform.rotation, mazeParent.transform);
                }
            }
        }
        for (int pickupNum = 0; pickupNum < pickupsPerFace; pickupNum++) {
            int row = rnd.Next(0, maze.GetLength(0) - 1);
            int column = rnd.Next(0, maze.GetLength(1) - 1);
            Instantiate(mazePickup, start + (column * nextColumnDirection) + (row * nextRowDirection) + (mazeWallHeight * wallHeightDirection),  wallRotation * mazePickup.transform.rotation, mazeParent.transform);
        }
    }

    void GenerateTutorialMazeFrom(Vector2Int startingPoint) {
        Shuffle(rnd, directions);
        foreach (Direction direction in directions) {
            Vector2Int newPosition = startingPoint + directionVectors[direction];
            if (newPosition.x >= 0 && newPosition.x < mazeGenerationWidth && newPosition.y >= mazeGenerationWidth && newPosition.y < 2*mazeGenerationWidth && !IsPositionVisited(newPosition)) {
                SetPositionBit(startingPoint, (int)direction);
                SetPositionBit(newPosition, (int)oppositeDirections[direction]);
                GenerateTutorialMazeFrom(newPosition);
            }
        }
    }

    // 0,0 is the top left of the front face
    void GenerateMazeFrom(Vector2Int startingPoint)
    {
        Shuffle(rnd, directions);
        foreach (Direction direction in directions)
        {
            Vector2Int newPosition = startingPoint + directionVectors[direction];
            KeyValuePair<Vector2Int,Direction> normalisedPositionDirection = NormalisePosition(newPosition, direction);
            Vector2Int pos = normalisedPositionDirection.Key;
            Direction dir = normalisedPositionDirection.Value;
            if (!IsPositionVisited(pos))
            {
                SetPositionBit(startingPoint, (int)direction);
                SetPositionBit(pos, (int)oppositeDirections[dir]);
                GenerateMazeFrom(pos);
            }
        }
    }

    bool IsPositionVisited(Vector2Int normalisedPosition)
    {
        return (GetPosition(normalisedPosition) != 0);
    }

    int GetPosition(Vector2Int normalisedPosition)
    {
        CubeFace face = GetFaceForPosition(normalisedPosition);
        RectInt faceRect = cubeFaceBounds[face];

        int localX = normalisedPosition.x - faceRect.xMin;
        int localY = normalisedPosition.y - faceRect.yMin;

        return mazes[face][localY, localX];
    }

    void SetPositionBit(Vector2Int normalisedPosition, int value)
    {
        CubeFace face = GetFaceForPosition(normalisedPosition);
        RectInt faceRect = cubeFaceBounds[face];

        int localX = normalisedPosition.x - faceRect.xMin;
        int localY = normalisedPosition.y - faceRect.yMin;

        mazes[face][localY, localX] |= value;
    }

    CubeFace GetFaceForPosition(Vector2Int normalisedPosition)
    {
        foreach (KeyValuePair<CubeFace, RectInt> entry in cubeFaceBounds)
        {
            if (entry.Value.Contains(normalisedPosition))
            {
                return entry.Key;
            }
        }

        throw new System.ArgumentOutOfRangeException("position is not within the bounds of the cube");
    }

    KeyValuePair<Vector2Int, Direction> NormalisePositionPass(KeyValuePair<Vector2Int, Direction> positionDirection)
    {
        Vector2Int position = positionDirection.Key;
        Direction direction = positionDirection.Value;

        int x = position.x;
        int y = position.y;
        int w = mazeGenerationWidth;
        Direction d = direction;
        int distanceFromEdge;
        int distanceAlongEdge;
        // 1
        if (x >= 2 * w && y >= 0 && y < w)
        {
            distanceFromEdge = x - 2 * w + 1;
            distanceAlongEdge = y;
            x = w - distanceFromEdge;
            y = 3 * w - 1 - distanceAlongEdge;
            d = oppositeDirections[d];

        }
        // 1r
        else if (x >= w && y >= 2 * w && y < 3 * w)
        {
            distanceFromEdge = x - w + 1;
            distanceAlongEdge = y - 2 * w;
            x = 2 * w - distanceFromEdge;
            y = w - 1 - distanceAlongEdge;
            d = oppositeDirections[d];
        }
        // 2
        else if (y >= w && x >= w && x < 2 * w)
        {
            distanceFromEdge = y - w + 1;
            distanceAlongEdge = x - w;
            x = w - distanceFromEdge;
            y = w + distanceAlongEdge;
            d = ccw90Directions[d];
        }
        // 2r
        else if (x >= w && y >= w && y < 2 * w)
        {
            distanceFromEdge = x - w + 1;
            distanceAlongEdge = y - w;
            x = w + distanceAlongEdge;
            y = w - distanceFromEdge;
            d = cw90Directions[d];
        }
        // 3
        else if (y < 0 && x >= w && x < 2 * w)
        {
            distanceFromEdge = -y;
            distanceAlongEdge = x - w;
            x = w - distanceFromEdge;
            y = -1 - distanceAlongEdge;
            d = cw90Directions[d];
        }
        // 3r
        else if (x >= w && y >= -w && y < 0)
        {
            distanceFromEdge = x - w + 1;
            distanceAlongEdge = y + w;
            x = w + distanceAlongEdge;
            y = -1 + distanceFromEdge;
            d = ccw90Directions[d];
        }
        // 4
        else if (y >= w && x >= -w && x < 0)
        {
            distanceFromEdge = y - w + 1;
            distanceAlongEdge = x + w;
            x = -1 + distanceFromEdge;
            y = 2 * w - 1 - distanceAlongEdge;
            d = cw90Directions[d];
        }
        // 4r
        else if (x < 0 && y >= w && y < 2 * w)
        {
            distanceFromEdge = -x;
            distanceAlongEdge = y - w;
            x = -1 - distanceAlongEdge;
            y = w - distanceFromEdge;
            d = ccw90Directions[d];
        }
        // 5
        else if (x < -w && y >= 0 && y < w)
        {
            distanceFromEdge = -x + w;
            distanceAlongEdge = y;
            x = -1 + distanceFromEdge;
            y = 3 * w - 1 - distanceAlongEdge;
            d = oppositeDirections[d];
        }
        // 5r
        else if (x < 0 && y >= 2 * w && y < 3 * w)
        {
            distanceFromEdge = -x;
            distanceAlongEdge = y - 2 * w;
            x = -w - 1 + distanceFromEdge;
            y = w - 1 - distanceAlongEdge;
            d = oppositeDirections[d];
        }
        // 6
        else if (y < 0 && x >= -w && x < 0)
        {
            distanceFromEdge = -y;
            distanceAlongEdge = x + w;
            x = -1 + distanceFromEdge;
            y = -w + distanceAlongEdge;
            d = ccw90Directions[d];
        }
        // 6r
        else if (x < 0 && y >= -w && y < 0)
        {
            distanceFromEdge = -x;
            distanceAlongEdge = y + w;
            x = -w + distanceAlongEdge;
            y = -1 + distanceFromEdge;
            d = cw90Directions[d];
        }
        // 7
        else if (y < -w && x >= 0 && x < w)
        {
            distanceFromEdge = -y + w;
            distanceAlongEdge = x;
            x = distanceAlongEdge;
            y = 3 * w - distanceFromEdge;
            //same direction
        }
        // 7r
        else if (y >= 3 * w && x >= 0 && x < w)
        {
            distanceFromEdge = y - 3 * w + 1;
            distanceAlongEdge = x;
            x = distanceAlongEdge;
            y = -w - 1 + distanceFromEdge;
            //same direction
        }
        Vector2Int newPosition = new Vector2Int(x, y);
        return new KeyValuePair<Vector2Int, Direction>(newPosition, d);
    }

    KeyValuePair<Vector2Int, Direction> NormalisePosition(Vector2Int position, Direction direction)
    {
        KeyValuePair<Vector2Int, Direction> normalisedPosition = new KeyValuePair<Vector2Int, Direction>(position, direction);
        KeyValuePair<Vector2Int, Direction> normalisedPosition2;
        do
        {
            normalisedPosition2 = normalisedPosition;
            normalisedPosition = NormalisePositionPass(normalisedPosition);
        } while (!(normalisedPosition.Key == normalisedPosition2.Key) || !(normalisedPosition.Value == normalisedPosition2.Value));
        return normalisedPosition;
    }

    public static void Shuffle<T>(System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public static void PrintMaze(int[,] maze, string fileName) {
        using(StreamWriter writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8)) {
            for (int i=maze.GetLength(0)-1; i>=0; i--) {
                for (int j=0; j<maze.GetLength(1); j++) {
                    if ((maze[i,j] & (int)Direction.Up) != 0) {
                        writer.Write('↑');
                    }
                    if ((maze[i,j] & (int)Direction.Down) != 0) {
                        writer.Write('↓');
                    }
                    if ((maze[i,j] & (int)Direction.Left) != 0) {
                        writer.Write('←');
                    }
                    if ((maze[i,j] & (int)Direction.Right) != 0) {
                        writer.Write('→');
                    }
                    writer.Write('\t');
                }
                writer.Write('\n');
            }
        }
    }
    public static string DirectionString(int direction) {
        string output = "";
        if ((direction & (int)Direction.Up) != 0) {
            output += '↑';
        }
        if ((direction & (int)Direction.Down) != 0) {
            output += '↓';
        }
        if ((direction & (int)Direction.Left) != 0) {
            output += '←';
        }
        if ((direction & (int)Direction.Right) != 0) {
            output += '→';
        }

        return output;
    }
}
