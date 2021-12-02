using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    [SerializeField] Vector2 worldSize = new Vector2(4, 4);

    Room[,] rooms;

    List<Vector2> takenPositions = new List<Vector2>();

    List<Vector2> secretPositions = new List<Vector2>();

    int gridSizeX;
    int gridSizeY;
    [SerializeField] int numberOfRooms = 20;

    [SerializeField] int endRoomMin;
    [SerializeField] int endRoomMax;

    MapSpriteSelector startRoom;
    MapSpriteSelector bossRoom;

    public GameObject roomWhiteObj;

    int secretCount = 0;

    bool bad = false;

    private void Start()
    {
        if(numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms();
        SetRoomDoors();
        DrawMap();
        //DelayHelper.DelayAction(this, AddSpecial, .01f);
    }

    private void CreateRooms()
    {
        //Setup
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;

        //Math Numbers
        float randomCompare = 0.2f;
        float randomCompareStart = 0.2f;
        float randomCompareEnd = 0.01f;

        //Add Rooms
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            //Grab new position
            checkPos = NewPosition();

            //Test new position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }
                while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                {
                    Debug.Log("Error: could not create with fewer neighbors than: " + NumberOfNeighbors(checkPos, takenPositions));
                }
            }

            //Finalize position
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);
        }
    }

    Vector2 NewPosition()
    {
        int x = 0;
        int y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);

            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y > gridSizeY || y < -gridSizeY);
        return checkingPos;
    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0;
        int inc = 0;
        int x = 0;
        int y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            }
            while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);

            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);

            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y > gridSizeY || y < -gridSizeY);
        if (inc >= 100)
        {
            Debug.Log("Error: could not find position with only one neighbor");
        }
        return checkingPos;
    }

    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;
        if(usedPositions.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }

    private void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (rooms[x,y] == null)
                {
                    continue;
                }

                Vector2 gridPosition = new Vector2(x, y);

                //Check Above
                if(y - 1 < 0)
                {
                    rooms[x, y].doorBot = false;
                }
                else
                {
                    rooms[x, y].doorBot = (rooms[x, y - 1] != null);
                }

                //Check Below
                if (y + 1 >= gridSizeY * 2)
                {
                    rooms[x, y].doorTop = false;
                }
                else
                {
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                }

                //Check Left
                if (x - 1 < 0)
                {
                    rooms[x, y].doorLeft = false;
                }
                else
                {
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                }

                //Check Right
                if (x + 1 >= gridSizeX * 2)
                {
                    rooms[x, y].doorRight = false;
                }
                else
                {
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                }
            }
        }
    }

    private void DrawMap()
    {
        int endRooms = 0;

        //Every location in the world grid
        foreach (Room room in rooms)
        {
            //Test if there is a room on this point of the grid
            if (room == null)
            {
                continue;
            }

            //Assign location values
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 10;
            drawPos.y *= 10;

            //Create minimap section 
            MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();

            //Assign type and adjacent rooms (for doors) to the minimap room from the data in the list
            mapper.type = room.type;
            mapper.up = room.doorTop;
            mapper.down = room.doorBot;
            mapper.left = room.doorLeft;
            mapper.right = room.doorRight;
            mapper.loc = drawPos;

            //Tests the door booleans and checks if there is only one door
            if (((mapper.up ? 1 : 0) + (mapper.down ? 1 : 0) + (mapper.left ? 1 : 0) + (mapper.right ? 1 : 0) == 1))
            {
                if (mapper.type != 1)
                {
                    endRooms++;
                }
            }
        }

        //Debug.Log("ENDROOMS: " + endRooms);

        //If not enough end rooms were generated, restart the generation script
        if (endRooms < endRoomMin || endRooms > endRoomMax)
        {
            Reset();
        }
        //Moves on to adding in the special room types
        else
        {
            DelayHelper.DelayAction(this, AddSpecial, .01f);
        }
    }

    private void TestMap()
    {
        foreach (MapSpriteSelector o in Object.FindObjectsOfType<MapSpriteSelector>())
        {
            Destroy(o.gameObject);
        }
        takenPositions.Clear();
        startRoom = null;
        bossRoom = null;
        rooms = null;
        bad = false;
        secretCount = 0;
    }

    private void AddSpecial()
    {
        float dist = 0;
        int count = 0;

        foreach (MapSpriteSelector o in Object.FindObjectsOfType<MapSpriteSelector>())
        {
            if (o.type == 1)
            {
                startRoom = o;
            }
        }

        foreach (MapSpriteSelector o in Object.FindObjectsOfType<MapSpriteSelector>())
        {
            if (((o.up ? 1 : 0) + (o.down ? 1 : 0) + (o.left ? 1 : 0) + (o.right ? 1 : 0) == 1))
            {
                if (o.type != 1)
                {
                    if (Vector2.Distance(startRoom.loc, o.loc) >= dist)
                    {
                        dist = Vector2.Distance(startRoom.loc, o.loc);
                        bossRoom = o;
                    }
                    o.type = 99;
                }
            }
        }

        bossRoom.type = 4;

        foreach (MapSpriteSelector o in Object.FindObjectsOfType<MapSpriteSelector>())
        {
            if (((o.up ? 1 : 0) + (o.down ? 1 : 0) + (o.left ? 1 : 0) + (o.right ? 1 : 0) == 1))
            {
                if (o.type != 1 && o.type != 4)
                {
                    if (o.type == 99 && count == 0)
                    {
                        o.type = 3;
                        count++;
                    }
                    else if (o.type == 99 && count == 1)
                    {
                        o.type = 2;
                        count++;
                    }
                    else if (o.type == 99 && count == 2)
                    {
                        o.type = 5;
                        count++;
                    }
                    else if (o.type == 99)
                    {
                        o.type = 0;
                        bossRoom.type = 4;
                    }
                }
            }
        }

        foreach (MapSpriteSelector o in Object.FindObjectsOfType<MapSpriteSelector>())
        {
            o.PickColor();
        }

        int i = Object.FindObjectsOfType<MapSpriteSelector>().Length - 1;
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue;
            }
            MapSpriteSelector mapper = Object.FindObjectsOfType<MapSpriteSelector>()[i];
            //Debug.Log(room.gridPos + " + " + mapper.loc);
            room.type = mapper.type;
            i--;
        }

        DelayHelper.DelayAction(this, BigTest, .05f);
       
    }

    private void Reset()
    {
        TestMap();
        CreateRooms();
        SetRoomDoors();
        DrawMap();
        //DelayHelper.DelayAction(this, AddSpecial, .01f); 
    }

    void BigTest()
    {
        foreach (MapSpriteSelector o in Object.FindObjectsOfType<MapSpriteSelector>())
        {
            if (o.transform.childCount != 1)
            {
                secretPositions.Insert(0, new Vector2(o.transform.GetChild(1).position.x, o.transform.GetChild(1).position.y));
                secretPositions.Insert(0, new Vector2(o.transform.GetChild(2).position.x, o.transform.GetChild(2).position.y));
                secretPositions.Insert(0, new Vector2(o.transform.GetChild(3).position.x, o.transform.GetChild(3).position.y));
                secretPositions.Insert(0, new Vector2(o.transform.GetChild(4).position.x, o.transform.GetChild(4).position.y));
            }
        }

        for (int x = 0; x < secretPositions.Count - 1; x++)
        {
            for (int y = 0; y < secretPositions.Count - 1; y++)
            {
                for (int z = 0; z < secretPositions.Count - 1; z++)
                {
                    if (secretCount == 0)
                    {
                        if (secretPositions[x] == secretPositions[y] && secretPositions[x] == secretPositions[z] && ((x != y) && (x != z) && (y != z)) && !takenPositions.Contains(secretPositions[x] / 10))
                        {
                            takenPositions.Insert(0, secretPositions[x]);

                            int secretX = ((int)(secretPositions[x].x / 10) + 8);
                            int secretY = ((int)(secretPositions[x].y / 10) + 8);

                            rooms[secretX, secretY] = new Room(secretPositions[x], 5);
                            MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, secretPositions[x], Quaternion.identity).GetComponent<MapSpriteSelector>();
                            rooms[secretX, secretY].type = 5;
                            rooms[secretX, secretY].gridPos = rooms[secretX, secretY].gridPos / 10;
                            mapper.type = 5;
                            if (takenPositions.Contains(mapper.transform.GetChild(1).position / 10))
                            {
                                rooms[secretX, secretY].doorTop = true;
                                mapper.up = true;
                            }
                            if (takenPositions.Contains(mapper.transform.GetChild(2).position / 10))
                            {
                                rooms[secretX, secretY].doorBot = true;
                                mapper.down = true;
                            }
                            if (takenPositions.Contains(mapper.transform.GetChild(3).position / 10))
                            {
                                rooms[secretX, secretY].doorLeft = true;
                                mapper.left = true;
                            }
                            if (takenPositions.Contains(mapper.transform.GetChild(4).position / 10))
                            {
                                rooms[secretX, secretY].doorRight = true;
                                mapper.right = true;
                            }
                            mapper.loc = secretPositions[x];
                            secretCount++;

                            for (int i = 1; i < 5; i++)
                            {
                                if (mapper.transform.childCount != 1)
                                {

                                    int mapperX = ((int)(mapper.transform.GetChild(i).position.x / 10) + 8);
                                    int mapperY = ((int)(mapper.transform.GetChild(i).position.y / 10) + 8);

                                    if (takenPositions.Contains(mapper.transform.GetChild(i).position / 10) && (rooms[mapperX, mapperY].type == 5))
                                    {
                                        bad = true;
                                        Debug.Log("SECRET NEXT TO SECRET");
                                    }

                                }
                            }   

                        }
                    }
                }
            }
        }


        if (secretCount < 1)
        {
            Reset();
        }
        else if (bad == true)
        {
            Reset();
        }
        else
        {
            GetComponent<SheetAssigner>().Assign(rooms);
        }

    }
}
