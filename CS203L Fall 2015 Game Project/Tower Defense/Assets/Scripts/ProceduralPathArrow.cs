using UnityEngine;
using System.Collections;

public enum Direction
{
    Up = 0,
    Down,
    Left,
    Right
}

public class ProceduralPathArrow : MonoBehaviour
{
    public ProceduralPathArrow prevArrow;
    public ProceduralPathArrow nextArrow;
    public PossibleMoves PossibleMoves { get; set; }
    private Direction direction;
    public Direction Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
            switch (direction)
            {
                case Direction.Up:
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    break;

                case Direction.Down:
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;

                case Direction.Left:
                    transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                    break;

                case Direction.Right:
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    break;
            }
        }
    }
    public int column;
    public int row;
    public ProceduralPathArrow()
    {
        prevArrow = null;
        nextArrow = null;
        PossibleMoves = new PossibleMoves
        {
            Up = true,
            Down = true,
            Left = true,
            Right = true
        };
	}

    public bool RandomizePossibleDirection()
    {
        if (PossibleMoves.NoPossibleMoves)
        {
            return false;
        }
        int randomInt = 0;
        Direction randomDir = Direction.Up;
        bool fail = false;
        do
        {
            randomInt = Random.Range(0, 4);
            fail = false;
            switch (randomInt)
            {
                case 0:
                    randomDir = Direction.Up;
                    if (!PossibleMoves.Up)
                    {
                        fail = true;
                    }
                    break;

                case 1:
                    randomDir = Direction.Down;
                    if (!PossibleMoves.Down)
                    {
                        fail = true;
                    }
                    break;

                case 2:
                    randomDir = Direction.Left;
                    if (!PossibleMoves.Left)
                    {
                        fail = true;
                    }
                    break;

                case 3:
                    randomDir = Direction.Right;
                    if (!PossibleMoves.Right)
                    {
                        fail = true;
                    }
                    break;
            }
        } while (fail);
        Direction = randomDir;
        return true;
    }

    public void DisableDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                PossibleMoves.Up = false;
                break;

            case Direction.Down:
                PossibleMoves.Down = false;
                break;

            case Direction.Left:
                PossibleMoves.Left = false;
                break;

            case Direction.Right:
                PossibleMoves.Right = false;
                break;
        }
    }

    public void DisableFacingDirection()
    {
        switch (direction)
        {
            case Direction.Up:
                PossibleMoves.Up = false;
                break;

            case Direction.Down:
                PossibleMoves.Down = false;
                break;

            case Direction.Left:
                PossibleMoves.Left = false;
                break;

            case Direction.Right:
                PossibleMoves.Right = false;
                break;
        }
    }

    public void DisableOppositeDirection()
    {
        switch (direction)
        {
            case Direction.Up:
                PossibleMoves.Down = false;
                break;

            case Direction.Down:
                PossibleMoves.Up = false;
                break;

            case Direction.Left:
                PossibleMoves.Right = false;
                break;

            case Direction.Right:
                PossibleMoves.Left = false;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
