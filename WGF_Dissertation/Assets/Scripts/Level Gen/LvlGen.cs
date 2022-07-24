using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlGen : MonoBehaviour
{
    List<Vector3> roomPos = new List<Vector3>();
    int xRand;
    int yRand;
    Vector3 plBossRoom;
    // Start is called before the first frame update
    void Start()
    {
        xRand = Random.Range(3, 6);
        yRand = Random.Range(3, 6);
        RoomGen(xRand, yRand);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RoomGen(int maxiX, int maxiY)
    {
        Vector3[,] roomArray = new Vector3[maxiX, maxiY];
        roomArray = InitalArray(roomArray, maxiX, maxiY);
        float GenGap = 8.96f;
        float prevX;
        float prevY;


        int maxMobSpawnRooms = maxiX + maxiY - 2;
        int currentMobSpawnRooms = 0;

        for (int y = 0; y < maxiY; y++)
        {
            for (int x = 0; x < maxiX; x++)
            {

                if (x == 0 && y == 0) // first room
                {

                    Room nRoom = Room.MakeRoomObj(Vector3.zero); //Creates a room
                    roomArray[x, y] = Vector3.zero;
                    prevX = 0;
                    prevY = 0;
                    roomPos.Add(Vector3.zero);
                    SpawnPassing(RoomType(ref currentMobSpawnRooms, maxMobSpawnRooms), roomArray[x, y]);

                }

                else
                {

                    if (x == 0) //Creates the first room on the x axis, so it increases the y-axis by the gen-space
                    {

                        Vector3 newPos = new Vector3();


                        Vector3 prevPos = roomArray[0, y - 1];
                        prevX = prevPos.x;
                        prevY = prevPos.y + GenGap;

                        newPos = new Vector3(prevX, prevY);
                        Room nRoom = Room.MakeRoomObj(newPos);
                        roomArray[x, y] = newPos;


                        roomPos.Add(newPos);
                        SpawnPassing(RoomType(ref currentMobSpawnRooms, maxMobSpawnRooms), roomArray[x, y]);
                        //nRoom.AddingSpawners();
                    }

                    else //Creates rooms along the x axis
                    {


                        Vector3 prevPos = roomArray[x - 1, y];
                        prevX = prevPos.x + GenGap;
                        prevY = prevPos.y;

                        Vector3 newPos = new Vector3(prevX, prevY);
                        Room nRoom = Room.MakeRoomObj(newPos);
                        roomArray[x, y] = newPos;

                        roomPos.Add(newPos);
                        SpawnPassing(RoomType(ref currentMobSpawnRooms, maxMobSpawnRooms), roomArray[x, y]);
                        //nRoom.AddingSpawners();


                    }
                }

                if (y == maxiY - 1 && x == maxiX - 1)
                {
                    Vector3 newPos = roomArray[x, y];
                    newPos.x = newPos.x + 15;
                    BossRoom bossRoom = BossRoom.MakeBossRoomObj(newPos); //creates the boss room
                    newPos.x = newPos.x + 1.92f;
                    plBossRoom = newPos;
                }
            }
        }





        for (int y = 0; y < maxiY; y++)
        {
            for (int x = 0; x < maxiX; x++)
            {
                float prev = 0;
                if (x == maxiX - 1)
                {

                    if (y == maxiY - 1) //top right room
                    {


                        continue;
                    }
                    else
                    {
                        float rand = RetRanInt(5);

                        if (rand == 1 && prev == 2 || prev == 1)
                        {
                            NoCorridorU(roomArray[x, y], roomArray[x, y + 1]);
                            continue;

                        }
                        else //On a 3,4,5 gen
                        {
                            CorridorCreationUp(roomArray[x, y], true); //X = max, Y = 0 to max
                            CorridorCreationUp(roomArray[x, y], false);
                        }
                        prev = rand;
                    }
                    //Far right room

                    continue;

                }
                else
                {

                    float rand = RetRanInt(5);


                    if (rand == 2 && prev == 1 || prev == 3)
                    {
                        NoCorridorR(roomArray[x, y], roomArray[x + 1, y]);
                        continue;
                    }
                    else
                    {

                        CorridorCreationR(roomArray[x, y], true); // X = min to max, y = 0 to max
                        CorridorCreationR(roomArray[x, y], false);

                    }
                    prev = rand;
                }

                if (y == maxiY - 1)
                {

                    continue;

                }
                else
                {
                    float rand = RetRanInt(4);

                    if (rand == 2 && prev == 1 || prev == 3)
                    {
                        NoCorridorU(roomArray[x, y], roomArray[x, y + 1]);
                        continue;
                    }
                    else
                    {

                        CorridorCreationUp(roomArray[x, y], true);  //X = min to max - 1, y = min to max
                        CorridorCreationUp(roomArray[x, y], false); // 
                    }
                    prev = rand;
                }


            }
        }
        //Handles all of the walls on the rooms on the edge of the level
        for (int y = 0; y < maxiY; y++)
        {
            for (int x = 0; x < maxiX; x++)
            {
                if (y == 0)
                {
                    FullWallsEdge(roomArray[x, y], false);
                    if (x == maxiX - 1)
                    {
                        TopWallsEdge(roomArray[x, y], true);
                    }
                    else if (x == 0)
                    {
                        TopWallsEdge(roomArray[x, y], false);
                    }
                }
                else if (y == maxiY - 1)
                {
                    FullWallsEdge(roomArray[x, y], true);
                    if (x == maxiX - 1)
                    {
                        TopWallsEdge(roomArray[x, y], true);
                    }
                    else if (x == 0)
                    {
                        TopWallsEdge(roomArray[x, y], false);
                    }
                }
                else if (x == 0)
                {
                    TopWallsEdge(roomArray[x, y], false);
                }
                else if (x == maxiX - 1)
                {
                    TopWallsEdge(roomArray[x, y], true);
                }
            }
        }

        //random player spawn
        int randomX = Random.Range(0, xRand);
        int randomY = Random.Range(0, yRand);
        Instantiate(Resources.Load("PlayerSpawner"), roomArray[randomX, randomY], Quaternion.Euler(0f, 0f, 0f));

        randomX = Random.Range(0, xRand);
        randomY = Random.Range(0, yRand);

        Vector3 portalPos = roomArray[randomX, randomY];
        portalPos.x = portalPos.x + 2f;
        portalPos.y = portalPos.y + 2f;

        GameObject bossportal = Instantiate(Resources.Load<GameObject>("BossPortal"), portalPos, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        bossportal.GetComponent<BossPortal>().SetTele(plBossRoom);
    }

    Vector3[,] InitalArray(Vector3[,] array, int maxiX, int maxiY) //Initialises the array
    {
        for (int y = 0; y < maxiY; y++)
        {
            for (int x = 0; x < maxiX; x++)
            {
                array[x, y] = new Vector3();
            }
        }
        return array;
    }
    /*
     * This method handles all of the right corridor creation.
     * Since the distance between rooms is hard coded, each room is three floor tiles away from each other
     */
    void CorridorCreationR(Vector3 val, bool lr)
    {
        Vector3 doorRLoc = val;

        if (lr == true)
        {

            doorRLoc.y = doorRLoc.y + 1.28f;
        }
        else
        {
            doorRLoc.y = doorRLoc.y + 2.56f;
        }

        doorRLoc.x = doorRLoc.x + 5.12f;


        for (int i = 0; i < 3; i++)
        {
            if (lr == true)
            {
                Instantiate(Resources.Load("CorrLeft"), doorRLoc, Quaternion.Euler(0f, 0f, 90f));
            }
            else
            {
                Instantiate(Resources.Load("CorrRight"), doorRLoc, Quaternion.Euler(0f, 0f, 90f));
            }

            doorRLoc.x = doorRLoc.x + 1.28f;
        }


    }
    /*
   * This method handles all of the up corridor creation.
   * Since the distance between rooms is hard coded, each room is three floor tiles away from each other
   */
    void CorridorCreationUp(Vector3 val, bool lr)
    {
        Vector3 doorUpLoc = val;
        if (lr == true)
        {

            doorUpLoc.x = doorUpLoc.x + 1.28f;
        }
        else
        {
            doorUpLoc.x = doorUpLoc.x + 2.56f;
        }

        doorUpLoc.y = doorUpLoc.y + 5.12f;

        for (int i = 0; i < 3; i++)
        {
            if (lr == true)
            {
                Instantiate(Resources.Load("CorrLeft"), doorUpLoc, Quaternion.Euler(0f, 0f, 0f));
            }
            else
            {
                Instantiate(Resources.Load("CorrRight"), doorUpLoc, Quaternion.Euler(0f, 0f, 0f));
            }
            doorUpLoc.y = doorUpLoc.y + 1.28f;
        }
    }

    float RetRanInt(float range)
    {
        float ret;

        ret = Mathf.Round(Random.Range(0, range));

        return ret;
    }

    void TopWallsEdge(Vector3 roomInPos, bool pos)
    {
        float posNeg = 0;
        if (pos == true)
        {
            posNeg = 4.52f;
        }
        else
        {
            posNeg = -0.68f;
        }

        Vector3 wall = roomInPos;
        wall.x = wall.x + posNeg;

        for (int counter = 0; counter < 2; counter++)
        {

            wall.y = wall.y + 1.28f;
            //Debug.Log(roomInPos);
            Instantiate(Resources.Load("WallTop"), wall, Quaternion.Euler(0f, 0f, 90f));

        }

    }

    void FullWallsEdge(Vector3 roomInPos, bool pos)
    {
        float posNeg = 0;
        if (pos == true)
        {
            posNeg = 4.8f;
        }
        else
        {
            posNeg = -0.88f;
        }

        Vector3 wall = roomInPos;
        wall.y = wall.y + posNeg;

        for (int counter = 0; counter < 2; counter++)
        {

            wall.x = wall.x + 1.28f;
            //Debug.Log(roomInPos);
            Instantiate(Resources.Load("FullWall"), wall, Quaternion.Euler(0f, 0f, 0f));

        }

    }
    //Creates walls were the corridor should have been for the right corridors
    void NoCorridorR(Vector3 fRoom, Vector3 sRoom)
    {
        fRoom.x = fRoom.x + 5.12f;
        fRoom.y = fRoom.y + 1.28f;
        Instantiate(Resources.Load("WallTop"), fRoom, Quaternion.Euler(0f, 0f, 90f));
        fRoom.y = fRoom.y + 1.28f;
        Instantiate(Resources.Load("WallTop"), fRoom, Quaternion.Euler(0f, 0f, 90f));

        sRoom.y = sRoom.y + 1.28f;
        Instantiate(Resources.Load("WallTop"), sRoom, Quaternion.Euler(0f, 0f, 90f));
        sRoom.y = sRoom.y + 1.28f;
        Instantiate(Resources.Load("WallTop"), sRoom, Quaternion.Euler(0f, 0f, 90f));
    }
    //Creates walls were the corridor should have been for the left corridors
    void NoCorridorU(Vector3 fRoom, Vector3 sRoom)
    {
        const float add = 4.8f;

        fRoom.y = fRoom.y + add;
        fRoom.x = fRoom.x + 1.28f;
        Instantiate(Resources.Load("FullWall"), fRoom, Quaternion.Euler(0f, 0f, 0f));
        fRoom.x = fRoom.x + 1.28f;
        Instantiate(Resources.Load("FullWall"), fRoom, Quaternion.Euler(0f, 0f, 0f));

        sRoom.y = sRoom.y - 0.88f;
        sRoom.x = sRoom.x + 1.28f;
        Instantiate(Resources.Load("FullWall"), sRoom, Quaternion.Euler(0f, 0f, 0f));
        sRoom.x = sRoom.x + 1.28f;
        Instantiate(Resources.Load("FullWall"), sRoom, Quaternion.Euler(0f, 0f, 0f));
    }

    int RandomRoomType(int min, int max)
    {
        int rand = Random.Range(min, max);
        return rand;
    }
    //This handles the monster spawning 
    int RoomType(ref int cur, int max)
    {
        int rand = RandomRoomType(0, 3);
        //Debug.Log("EIR rand: " + rand);
        if (rand == 0)
        {
            return rand;
        }

        else if (rand == 2 || rand == 1)
        {
            if (cur < max)
            {
                cur++;
                return rand;

            }
            else
            {
                rand = 0;
            }

        }
        return rand;
    }

    //Loads the monster spawner
    void MobInstant(Vector3 val)
    {
        Vector3 temp = val;
        GameObject mbI = Instantiate(Resources.Load<GameObject>("MinorMobSpawner"), temp, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
    }

    void SpawnPassing(int type, Vector3 position)
    {
        Vector3 instArg = position;
        if (type == 1 || type == 2)
        {
            GenMobSpawners(instArg);
        }

    }
    /*
     * Hard coded spawners for the monster spawners.
     * This method spreads the the monster spawners around the room in a specific pattern
     * different cofigurations are used to create different shapes
     */
    void GenMobSpawners(Vector3 initalPos)
    {
        int spawnMap = Random.Range(1, 4);

        if (spawnMap == 1)
        {
            initalPos.x = initalPos.x + 1.28f;
            initalPos.y = initalPos.y + 1.28f;
            MobInstant(initalPos);

            initalPos.y = initalPos.y + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);

            initalPos.y = initalPos.y - 1.28f;
            MobInstant(initalPos);
        }
        else if (spawnMap == 2)
        {
            initalPos.x = initalPos.x + 1.28f;
            initalPos.y = initalPos.y + 2.56f;
            MobInstant(initalPos);


            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);

            initalPos.y = initalPos.y + 1.28f;
            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x - 3.84f;
            MobInstant(initalPos);
        }
        else if (spawnMap == 3)
        {
            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x + 1.28f;
            initalPos.y = initalPos.y + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x - 3.84f;
            MobInstant(initalPos);

            initalPos.y = initalPos.y + 2.56f;
            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);
        }
        else
        {
            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);

            initalPos.y = initalPos.y + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x - 1.28f;
            initalPos.y = initalPos.y + 1.28f;
            MobInstant(initalPos);

            initalPos.y = initalPos.y + 1.28f;
            MobInstant(initalPos);

            initalPos.x = initalPos.x + 1.28f;
            MobInstant(initalPos);
        }
    }

}



