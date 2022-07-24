using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    const float GenSpace = 1.28f;

    new public Vector3[,] roomCoords = new Vector3[6, 6];

    new public int maxX = 6;
    new public int maxY = 6;
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
    //Same as room, the function wouldn't inherit so it was copied
    public override void GenRooms2dA()
    {

        bool isFirstBl = true;
        float spaceY = 0;
        float firstX = pos.x;
        float firstY = pos.y;



        //Creates a grid to create the room from the first point
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

    }
    //Similar to the inherited class but for the boss room
    public static BossRoom MakeBossRoomObj(Vector3 pos)
    {
        GameObject gmO = new GameObject();
        BossRoom ret = gmO.AddComponent<BossRoom>();

        ret.setPos(pos);



        return ret;
    }

    public override void GenerateTiles()
    {

        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 6; x++)
            {

                AddRandAndWalls(x, y, roomCoords[y, x]);



            }
        }
    }
    //
    public override void AddRandAndWalls(int x, int y, Vector3 pos)
    {
        Vector3 val = pos;
        Vector3 wall = pos;
        RandTile(val);
        switch (y)
        {
            case 0: //Bottom of the room
                if (x == 0)
                {
                    CornerWallCreater(false, false, val);

                }
                else if (x >= 1 && x < 5)
                {
                    BossWalls(true, false, val);

                }

                else if (x == 5)
                {
                    CornerWallCreater(true, false, val);

                }

                break;
            case 1: //Middle of the room
                if (x == 0)
                {
                    BossWalls(false, false, val);


                }


                else if (x == 5)
                {
                    BossWalls(false, true, val);


                }

                break;
            case 2: //Middle of the room
                if (x == 0)
                {
                    BossWalls(false, false, val);


                }

                else if (x == 5)
                {
                    BossWalls(false, true, val);


                }

                break;
            case 3: //Middle of the room

                if (x == 0)
                {
                    BossWalls(false, false, val);


                }


                else if (x == 5)
                {
                    BossWalls(false, true, val);


                }

                break;
            case 4: //Middle of the room
                if (x == 0)
                {
                    BossWalls(false, false, val);


                }
                if (x == 2)
                {
                    Instantiate(Resources.Load("BossSpawner"), pos, Quaternion.Euler(0f, 0f, 0f)); //Spawns the random boss
                }

                else if (x == 5)
                {
                    BossWalls(false, true, val);


                }

                break;
            case 5: //Top of the room
                if (x == 0)
                {
                    CornerWallCreater(false, true, val);

                }
                else if (x >= 1 && x < 5)
                {
                    EndOfLvl(val);

                }

                else if (x == 5)
                {
                    CornerWallCreater(true, true, val);

                }

                break;

        }

    }

    void BossWalls(bool full, bool leftOrUp, Vector3 pos)
    {
        Vector3 temp = pos;
        string prefabName;
        if (full)
        {
            prefabName = "FullWall";
            if (leftOrUp)
            {
                temp.y = temp.y + 0.96f;
            }
            else
            {
                temp.y = temp.y - 0.96f;
            }
        }
        else
        {
            prefabName = "TopWall";
            if (leftOrUp)
            {
                temp.x = temp.x + 0.68f;
            }
            else
            {
                temp.x = temp.x - 0.68f;
            }
        }
        if (prefabName == "FullWall")
        {
            Instantiate(Resources.Load("FullWall"), temp, Quaternion.Euler(0f, 0f, 0f));
        }
        else if (prefabName == "TopWall")
        {
            Instantiate(Resources.Load("WallTop"), temp, Quaternion.Euler(0f, 0f, 90f));
        }
    }

    void EndOfLvl(Vector3 pos)
    {
        Vector3 temp = pos;
        temp.y = temp.y + 0.96f;

        Instantiate(Resources.Load("EndOfLevel"), temp, Quaternion.Euler(0f, 0f, 0f));
    }
}
