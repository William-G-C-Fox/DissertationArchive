using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector3 pos = new Vector3();
    public static Vector3[,] roomCoords = new Vector3[4, 4];
    const float GenSpace = 1.28f;
    static int type;
    public int maxX = 4, maxY = 4;

    // Start is called before the first frame update
    void Start()
    {

        GenRooms2dA();
        GenerateTiles();


    }

    // Update is called once per frame
    void Update()
    {

    }


    /**GenRooms2dA
     * 
     * This method creates the individual rooms that are generated around the dungeon. 
     * This works by looping through a pre-defined array and taking the values inX and inY as the inital coords
     * Then generating a grid with +1.28 since each square size is 128px
     */
    public virtual void GenRooms2dA()
    {



        bool isFirstBl = true;
        float spaceY = 0;
        float firstX = pos.x;
        float firstY = pos.y;





        for (int y = 0; y < maxY; y++)
        {


            for (int x = 0; x < maxX; x++)
            {


                if (isFirstBl == true)
                {
                    //spaceY = inY;

                    //DrawCoords roomPart = new DrawCoords(firstX, firstY, true);
                    Vector3 temp = pos;
                    roomCoords[y, x] = temp;

                    isFirstBl = false;
                    spaceY = firstY;

                }
                else
                {

                    if (x == 0)
                    {
                        Vector3 prevPos = new Vector3();
                        Vector3 newPos = new Vector3();


                        prevPos = roomCoords[y - 1, 0];

                        newPos = new Vector3(firstX, spaceY);
                        roomCoords[y, x] = newPos;
                    }
                    else
                    {


                        Vector3 prevPos = new Vector3();
                        Vector3 newPos = new Vector3();

                        prevPos = roomCoords[y, x - 1];


                        prevPos.x = prevPos.x + GenSpace;
                        newPos = new Vector3(prevPos.x, spaceY, 0);
                        roomCoords[y, x] = newPos;
                    }




                }

            }
            spaceY = spaceY + GenSpace;
        }


        //ArrayToCon();
    }
    /**Generate tiles
     * 
     * This method loops through the array and calls AddRandToRooms
     * which instates the prefab
     * 
     */
    public virtual void GenerateTiles()
    {

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                //Debug.Log(roomCoords[y, x]);
                AddRandAndWalls(x, y, roomCoords[y, x]);


                //Debug.Log(val.getDraw());


            }
        }
    }



    /*
     * Since instaniated objects are clones, it is hard for the clone to be called and have the specific clone set.  
     * Therefore a static Room class is used to create a gameobject and then set the position of the clone
     */
    public static Room MakeRoomObj(Vector3 pos)
    {
        GameObject gmO = new GameObject();
        Room ret = gmO.AddComponent<Room>();

        //Debug.Log(type);
        ret.setPos(pos);




        return ret;
    }

    public void setPos(Vector3 nPos)
    {
        this.pos = nPos;
    }


    public Vector3 getPos()
    {
        return pos;
    }



    public Room retRoom()
    {
        return this;
    }


    //Goes through the roomcoords to find the corners and create walls in the corner
    public virtual void AddRandAndWalls(int x, int y, Vector3 pos)
    {
        Vector3 val = pos;
        Vector3 wall = pos;
        RandTile(val);
        switch (y)
        {
            case 0:
                if (x == 0)
                {
                    CornerWallCreater(false, false, val);

                }

                else if (x == 3)
                {
                    CornerWallCreater(true, false, val);

                }

                break;

            case 3:
                if (x == 0)
                {
                    CornerWallCreater(false, true, val);


                }
                else if (x == 3)
                {
                    CornerWallCreater(true, true, val);

                }
                break;

        }


    }
    //Randomly generates a tile floor from three of the assets created
    public void RandTile(Vector3 pos)
    {
        int rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                Instantiate(Resources.Load("SFloor"), pos, Quaternion.Euler(0f, 0f, 0f));
                break;
            case 2:
                Instantiate(Resources.Load("AFloor"), pos, Quaternion.Euler(0f, 0f, 0f));
                break;
            case 3:
                Instantiate(Resources.Load("TriFloor"), pos, Quaternion.Euler(0f, 0f, 0f));
                break;
        }

    }
    // Creates walls on the corner of the room
    public void CornerWallCreater(bool xDir, bool yDir, Vector3 pos)
    {
        float x = 0, y = 0;
        Vector3 wall = pos;
        if (xDir == true && yDir == true)
        {
            x = 0.68f;
            y = 0.96f;
        }
        else if (xDir == true && yDir == false)
        {
            x = 0.68f;
            y = -0.88f;
        }
        else if (xDir == false && yDir == true)
        {
            x = -0.68f;
            y = 0.96f;
        }
        else if (xDir == false && yDir == false)
        {
            x = -0.68f;
            y = -0.88f;
        }
        wall.x = wall.x + x;
        Instantiate(Resources.Load("WallTop"), wall, Quaternion.Euler(0f, 0f, 90f)); //For the top and bottom side of the room, a full horizontal wall is used
        wall = pos;
        wall.y = wall.y + y;
        Instantiate(Resources.Load("FullWall"), wall, Quaternion.Euler(0f, 0f, 0f)); //For the right and left side of the room, a thin vertical wall is used
    }




}


