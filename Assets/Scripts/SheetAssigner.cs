using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetAssigner : MonoBehaviour
{
    [SerializeField] Texture2D[] sheetsNormal;
    [SerializeField] Texture2D[] sheetsBoss;
    [SerializeField] Texture2D[] sheetsItem;
    [SerializeField] Texture2D[] sheetsShop;
    [SerializeField] Texture2D[] sheetsStart;
    [SerializeField] GameObject RoomObj;

    public Vector2 roomDimensions = new Vector2(16 * 17, 16 * 9);
    public Vector2 gutterSize = new Vector2(16 * 9, 16 * 4);

    public void Assign(Room[,] rooms)
    {
        foreach (Room room in rooms)
        {
            if(room == null)
            {
                continue;
            }
            if (room.type == 0)
            {
                int index = Mathf.RoundToInt(Random.value * (sheetsNormal.Length - 1));
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(sheetsNormal[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
            if (room.type == 1)
            {
                int index = Mathf.RoundToInt(Random.value * (sheetsStart.Length - 1));
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(sheetsStart[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
            if (room.type == 2)
            {
                int index = Mathf.RoundToInt(Random.value * (sheetsItem.Length - 1));
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(sheetsItem[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
            if (room.type == 3)
            {
                int index = Mathf.RoundToInt(Random.value * (sheetsShop.Length - 1));
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(sheetsShop[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
            if (room.type == 4)
            {
                int index = Mathf.RoundToInt(Random.value * (sheetsBoss.Length - 1));
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(sheetsBoss[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
        }
    }
}
