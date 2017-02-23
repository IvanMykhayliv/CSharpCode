using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralLevelRand : MonoBehaviour {
    public int columns;
    public int rows;
    public int startingColumn;
    public int startingRow;
    public int endColumn;
    public int endRow;
    public Direction startingDirection;
    public GameObject arrowPrefab;
    //public Transform upperLeft;
    //public Transform bottomRight;
    private IList<ProceduralPathArrow> arrowList;
    private ProceduralPathArrow startingArrow;
    private ProceduralPathArrow currentArrow;

	void Start () {
        //To index...
        //row * rows + column
        arrowList = new List<ProceduralPathArrow>(columns * rows);

        //Make sure everything is initialized to null
        for (int i = 0; i < columns * rows; i++)
        {
            arrowList.Add(null);
        }

        startingArrow = CreateArrow(startingColumn, startingRow, startingDirection);
        currentArrow = startingArrow;

        StartCoroutine(GenerateRandomPath());
	}

    ProceduralPathArrow CreateArrow(int column, int row, Direction direction)
    {
        float x = column - (columns / 2f) + 0.5f;
        float y = row - (rows / 2f) + 0.5f;
        GameObject arrowObject = Instantiate(arrowPrefab, new Vector3(x, y , 0f), Quaternion.identity) as GameObject;
        ProceduralPathArrow arrow = arrowObject.GetComponent<ProceduralPathArrow>();
        arrow.Direction = direction;
        arrow.row = row;
        arrow.column = column;
        arrowList[row * rows + column] = arrow;
        return arrow;
    }

    void GetNextSpace(Direction dir, ref int column, ref int row)
    {
        switch (dir)
        {
            case Direction.Up:
                row++;
                break;

            case Direction.Down:
                row--;
                break;

            case Direction.Left:
                column--;
                break;

            case Direction.Right:
                column++;
                break;
        }
    }

    //Returns a random direction that is not the opposite current direction
    Direction RandomDirection(Direction dir)
    {
        int randomInt = 0;
        Direction oppositeDir = Direction.Up;
        switch (dir)
        {
            case Direction.Up:
                oppositeDir = Direction.Down;
                break;

            case Direction.Down:
                oppositeDir = Direction.Up;
                break;

            case Direction.Left:
                oppositeDir = Direction.Right;
                break;

            case Direction.Right:
                oppositeDir = Direction.Left;
                break;
        }
        Direction randomDir = Direction.Up;
        do
        {
            randomInt = Random.Range(0, 4);
            switch (randomInt)
            {
                case 0:
                    randomDir = Direction.Up;
                    break;

                case 1:
                    randomDir = Direction.Down;
                    break;

                case 2:
                    randomDir = Direction.Left;
                    break;

                case 3:
                    randomDir = Direction.Right;
                    break;
            }
        }
        while (randomDir == oppositeDir);
        return randomDir;
    }

    //TODO: Add Turn prefabs after paths are all finished
    //Sometimes arrows are created diagonally from the previous
    IEnumerator GenerateRandomPath()
    {
        yield return new WaitForSeconds(0.5f);
        int currentRow = startingArrow.row;
        int currentColumn = startingArrow.column;
        Direction currentDirection = startingArrow.Direction;

        while (true){
            //If the current arrow has no possible moves, delete it and return to the previous arrow, give the previous arrow a new possible direction
            while (currentArrow.PossibleMoves.NoPossibleMoves)
            {
                Debug.Log("This arrow has no possible moves!");
                //Should never happen
                if (currentArrow.prevArrow == null)
                {
                    break;
                }
                currentArrow.prevArrow.DisableFacingDirection();
                currentArrow.prevArrow.RandomizePossibleDirection();
                GameObject temp = currentArrow.gameObject;
                currentArrow = currentArrow.prevArrow;
                currentColumn = currentArrow.column;
                currentRow = currentArrow.row;
                currentArrow.nextArrow = null;
                Destroy(temp);
            }
            if (currentArrow.prevArrow == null && currentArrow.PossibleMoves.NoPossibleMoves)
            {
                Debug.Log("Could not construct the map!");
                break;
            }
            //Move in front of the current arrow, end if there is an edge
            //Check for arrow collisions
            //If there is an arrow in the way, use a different direction
            currentDirection = currentArrow.Direction;
            GetNextSpace(currentDirection, ref currentColumn, ref currentRow);
            //Edge of the map (Check BEFORE checking arrow collisions
            if (currentColumn < 0 || currentColumn > columns - 1 || currentRow < 0 || currentRow > rows - 1)
            {
                //Move back a space
                currentColumn = currentArrow.column;
                currentRow = currentArrow.row;
                currentArrow.DisableFacingDirection();
                currentArrow.RandomizePossibleDirection();
                Debug.Log("Hit the edge of the map!");
                continue;
            }
            if (arrowList[currentRow * rows + currentColumn] != null)
            {
                //Move back a space
                currentColumn = currentArrow.column;
                currentRow = currentArrow.row;
                currentArrow.DisableFacingDirection();
                currentArrow.RandomizePossibleDirection();
                Debug.Log("Arrow collision!");
                continue;
            }
            if (currentColumn == endColumn && currentRow == endRow)
            {
                Debug.Log("Reached the defendable object!");
                break;
            }
            //At this point, there should be an empty space in front of the current arrow
            //Create a new arrow in front of the current arrow
            ProceduralPathArrow nextArrow = CreateArrow(currentColumn, currentRow, currentDirection);
            nextArrow.DisableOppositeDirection();
            //Randomize the new arrow's direction
            currentDirection = RandomDirection(currentDirection);
            nextArrow.Direction = currentDirection;
            currentArrow.nextArrow = nextArrow;
            nextArrow.prevArrow = currentArrow;
            currentArrow = nextArrow;
        }
    }

    void Update()
    {
        
    }
}
