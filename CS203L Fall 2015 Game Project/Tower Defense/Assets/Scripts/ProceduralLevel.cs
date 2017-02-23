using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed 
{
    enum Direction {UP, DOWN, LEFT, RIGHT}; ///< screen side
    enum Tile {STRAIGHT, LEFT, RIGHT}; ///< orientation from direction

    public class BoardManager : MonoBehaviour
    {
        // Using Serializable allows us to embed a class with sub properties in the inspector.
        /// <summary>
        /// this is being saved for when tunnels are added to prevent there being too many tunnels
        /// </summary>
        [Serializable]
        public class Count
        {
            public int minimum;             //Minimum value for our Count class.
            public int maximum;             //Maximum value for our Count class.


            //Assignment constructor.
            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int columns = 8;                                         //Number of columns in our game board.
        public int rows = 8;                                            //Number of rows in our game board.
        public Count wallCount = new Count(5, 9);                       //Lower and upper limit for our random number of walls per level.
        public Count foodCount = new Count(1, 5);                       //Lower and upper limit for our random number of food items per level.
        public GameObject defendableObject;                             //Prefab of the defendable object
        public GameObject enemyGenerator;                               //Prefab of the Enemy generator
        public GameObject[] grassTiles;                                 //Array of floor prefabs.
        public GameObject[] turnTiles;                                  //Array of turn prefabs.
        public GameObject[] straightTiles;                              //Array of straight prefabs.

        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private List<Vector3> gridPositions = new List<Vector3>();      //A list of where tiles have not been placed
        private Vector3 endPosition;
        private Vector3 StartPosition;
        private Vector3 currentPosition;
        private Direction currentDirection;


        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            //Clear our list gridPositions.
            gridPositions.Clear();

            //Loop through x axis (columns).
            for (int x = 1; x < columns - 1; x++)
            {
                //Within each column, loop through y axis (rows).
                for (int y = 1; y < rows - 1; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }
        /// <summary>
        /// used to create a tile at a position on a grid
        /// has default values for from and up to allow this to accept the grass tiles without needing something complicated
        /// </summary>
        /// <param name="toInstantiate"></param>
        /// <param name="position"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /*void CreateTile(GameObject toInstantiate, Vector3 position, Direction from = UP, Tile to = STRAIGHT)
        {
            //Create the direction the tile is spun to

            
            //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
            GameObject instance = Instantiate(toInstantiate, position, Quaternion.identity) as GameObject;

            //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
            instance.transform.SetParent(boardHolder);

            gridPositions.Remove(position);
        }*/

        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -1; x < columns + 1; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -1; y < rows + 1; y++)
                {
                    // Check if we are at a terminus to place a straight tile to connect to
                    if ((x == StartPosition.x && y == StartPosition.y) || (x == endPosition.x && y == endPosition.y))
                    {
                        //TODO: this wants straight tiles at the position
                        //CreateTile(straightTiles[Random.Range(0, straightTiles.Length)], new Vector3(x, y, 0f), , );
                    }
                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    else if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        //CreateTile(grassTiles[Random.Range(0, grassTiles.Length)], new Vector3(x, y, 0f));
                    }
                }
            }
        }


        //RandomPosition returns a random position from our list gridPositions.
        Vector3 RandomPosition()
        {
            //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
            int randomIndex = Random.Range(0, gridPositions.Count);

            //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
            Vector3 randomPosition = gridPositions[randomIndex];

            //Remove the entry at randomIndex from the list so that it can't be re-used.
            gridPositions.RemoveAt(randomIndex);

            //Return the randomly selected Vector3 position.
            return randomPosition;
        }


        //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            //Choose a random number of objects to instantiate within the minimum and maximum limits
            int objectCount = Random.Range(minimum, maximum + 1);

            //Instantiate objects until the randomly chosen limit objectCount is reached
            for (int i = 0; i < objectCount; i++)
            {
                //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
                Vector3 randomPosition = RandomPosition();

                //Choose a random tile from tileArray and assign it to tileChoice
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }

        void PathTerminus(){
            int a = Random.Range(0, 4);
            int b;
            int x, y;
            int defX, defY;

            //Right side
            if (a == 0){
                x = 0;
                b = Random.Range(0, 9);
                y = b;


                Instantiate(enemyGenerator, new Vector3(x, y, 0), Quaternion.identity);
                //instantiate generator at x and y
                //then checks to see where y is placed and places it on the opposite side
                //ex. if ts -1 then it places the defendable object between 0 and 1

                if (y <= 3){
                    b = Random.Range(3, 9);
                    defY = b;
                    defX = 9;
                    //possibly have the object spawn in a quadrant of the grid? 

                    Instantiate(defendableObject, new Vector3(defX, defY, 0), Quaternion.identity);

                    //instantiate the defendable object at the other corner of the grid
                }

                if (y > 3){
                    b = Random.Range(0, 4);
                    defY = b;
                    defX = 8;

                    Instantiate(defendableObject, new Vector3(defX, defY, 0), Quaternion.identity);

                    //if the generator is put in the middle, have the defendable
                    //object spawn anywhere on the other side?
                }
            }

            //Top
            if (a == 1){
                y = 8;
                b = Random.Range(0, 9);
                x = b;

                //instantiate generator at x and y
                Instantiate(enemyGenerator, new Vector3(x, y, 0), Quaternion.identity);

                if (x <= 3){
                    b = Random.Range(3, 9);
                    defX = b;
                    defY = 0;
                    //possibly have the object spawn in a quadrant of the grid? 

                    Instantiate(defendableObject, new Vector3(defX, defY, 0), Quaternion.identity);

                    //instantiate the defendable object at the other corner of the grid
                }

                if (x > 3){
                    b = Random.Range(0, 4);
                    defX = b;
                    defY = 0;

                    Instantiate(defendableObject, new Vector3(defX, defY, 0), Quaternion.identity);

                    //if the generator is put in the middle, have the defendable
                    //object spawn anywhere on the other side?
                }
            }

            //Left side
            if (a == 2){
                x = 8;
                b = Random.Range(0, 9);
                y = b;

                Instantiate(enemyGenerator, new Vector3(x, y, 0), Quaternion.identity);

                if (y <= 3){
                    b = Random.Range(3, 9);
                    defX = b;
                    defY = 9;
                    //possibly have the object spawn in a quadrant of the grid? 

                    Instantiate(defendableObject, new Vector3(defX, defY, 0), Quaternion.identity);

                    //instantiate the defendable object at the other corner of the grid
                }

                if (y > 3){
                    b = Random.Range(0, 4);
                    defY = b;
                    defX = 0;

                    Instantiate(defendableObject, new Vector3(defX, defY, 0), Quaternion.identity);

                    //if the generator is put in the middle, have the defendable
                    //object spawn anywhere on the other side?
                }
            }

            //Bottom
            if (a == 3){
                y = 0;
                b = Random.Range(0, 9);
                x = b;

                Instantiate(enemyGenerator, new Vector3(x, y, 0), Quaternion.identity);

                if (x <= 3){
                    b = Random.Range(3, 9);
                    defY = b;
                    defX = 9;
                    //possibly have the object spawn in a quadrant of the grid? 

                    Instantiate(defendableObject, new Vector3(defX, defY, 0), Quaternion.identity);

                    //instantiate the defendable object at the other corner of the grid
                }

                if (x > 3){
                    b = Random.Range(0, 4);
                    defX = b;
                    defY = 9;

                    Instantiate(defendableObject, new Vector3(defX, defY, 0), Quaternion.identity);

                    //if the generator is put in the middle, have the defendable
                    //object spawn anywhere on the other side?
                }
            }
        }

        void CreatePath()
        {
            if (2 >= Vector3.Distance(endPosition, currentPosition))
            {
                //we are nearby the exit
                
                //complete the path to the exit

                //break;
            }
            List<Vector3> unusedPositions = new List<Vector3>();
            int usedPositions = 12;
            int pathsRemaining = 0;
            foreach (Vector3 pos in gridPositions)
            {
                if (2 >= Vector3.Distance(pos, currentPosition))
                {
                    usedPositions--;
                    unusedPositions.Add(pos);
                    if (1 >= Vector3.Distance(pos, currentPosition))
                    {
                        pathsRemaining++;
                    }
                }
            }
            /*
            if (usedPositions > 10)
                //error found
            { ; }
            */
            bool error = false;
            switch (pathsRemaining)
            {
                case 1:
                    // use the remaining path
                    break;
                case 2:
                    // this is a T with two output options
 
                    // if you can go straingt doesn't matter choose between

                    // if you cannot go straight...
 

                    break;
                case 3:
                    // all choices are open though not nessercairily equal
                    break;
                default:
                    Debug.Log("Path error");
                    break;
                    // if it is possible to backtrack do so here, if not force start over path generation
            }

            //if we get here the program is not at the exit
            // and henceforth more path is needed

            CreatePath();
        }

        void Fill()
        {
            foreach (Vector3 pos in gridPositions){
                //CreateTile(grassTiles[Random.Range(0, grassTiles.Length)], pos);
            }
        }

        //SetupScene initializes our level and calls the previous functions to lay out the game board
        // level is from the unity implimentation, and is left for when we decide to have the level generation change depending on what level it is.
        public void SetupScene(int level)
        {
            // create a list of all potential tile locations
            InitialiseList();
            
            //create the enterence and exit 
            PathTerminus();

            //Creates the outer walls and floor.
            BoardSetup();

            //Create the path
            CreatePath();

            //Fill gaps between the paths
            Fill();
        }
    }
}