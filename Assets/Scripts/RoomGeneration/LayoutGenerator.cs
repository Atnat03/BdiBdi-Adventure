using System.Collections.Generic;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
    public List<RoomDataScriptable> availableRooms;
    public int gridSize = 4;
    public float roomSpacing = 10f;
    public Transform houseParent;

    private string[,] grid;
    private Vector2Int center;

    void Start()
    {
        GenerateLayout();
    }

    public void GenerateLayout()
    {
        grid = new string[gridSize, gridSize];
        center = new Vector2Int(gridSize / 2, gridSize / 2);
        Debug.ClearDeveloperConsole();

        // 1️⃣ Placer le salon au centre
        grid[center.x, center.y] = Room.Salon.ToString();

        // 2️⃣ Créer la liste dynamique des cases disponibles
        List<Vector2Int> availablePositions = new List<Vector2Int>();
        AddAdjacentPositions(center, availablePositions);

        // 3️⃣ Mélanger les pièces sauf le salon
        List<RoomDataScriptable> roomsToPlace = new List<RoomDataScriptable>(availableRooms);
        roomsToPlace.RemoveAll(r => r.type == Room.Salon);
        ShuffleList(roomsToPlace);

        // 4️⃣ Placer les autres pièces
        foreach (var room in roomsToPlace)
        {
            if (availablePositions.Count == 0)
            {
                Debug.LogWarning($"❌ Impossible de placer la pièce : {room.type}");
                continue;
            }

            // Choisir une case aléatoire parmi les disponibles
            int idx = Random.Range(0, availablePositions.Count);
            Vector2Int pos = availablePositions[idx];

            // Placer la pièce
            grid[pos.x, pos.y] = room.type.ToString();

            // Retirer la case utilisée
            availablePositions.RemoveAt(idx);

            // Ajouter ses nouvelles cases adjacentes (en haut, gauche, droite)
            AddAdjacentPositions(pos, availablePositions);
        }

        PrintGrid();
        InstantiateRooms();
        InstantiateDoors();
    }

    void AddAdjacentPositions(Vector2Int pos, List<Vector2Int> positions)
    {
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.left, Vector2Int.right };
        foreach (var dir in dirs)
        {
            Vector2Int newPos = pos + dir;
            if (newPos.x >= 0 && newPos.x < gridSize && newPos.y >= 0 && newPos.y < gridSize)
            {
                if (grid[newPos.x, newPos.y] == null && !positions.Contains(newPos))
                    positions.Add(newPos);
            }
        }
    }

    void InstantiateRooms()
    {
        foreach (var roomData in availableRooms)
        {
            if (roomData == null)
            {
                Debug.LogWarning("RoomData est null !");
                continue;
            }

            if (roomData.roomPrefab == null)
            {
                Debug.LogWarning($"Prefab manquant pour {roomData.type}");
                continue;
            }

            string name = roomData.type.ToString();

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (grid[x, y] == name)
                    {
                        Vector3 pos = new Vector3(x * roomSpacing, y * roomSpacing, 0);
                        GameObject instance = Instantiate(roomData.roomPrefab, pos, Quaternion.identity, houseParent);
                        instance.name = $"{name} ({x},{y})";

                        // Enregistrement dans RoomManager
                        RoomManager.instance.RegisterRoom(roomData.type, instance.transform);
                    }
                }
            }
        }
    }
    
void InstantiateDoors()
{
    for (int x = 0; x < gridSize; x++)
    {
        for (int y = 0; y < gridSize; y++)
        {
            string currentRoomName = grid[x, y];
            if (currentRoomName == null) continue;

            RoomDataScriptable currentRoomData = availableRooms.Find(r => r.type.ToString() == currentRoomName);
            if (currentRoomData?.doorPrefab == null || currentRoomData.doorPoints.Count == 0) continue;

            Transform roomTransform = RoomManager.instance.GetRoomTransform((Room)System.Enum.Parse(typeof(Room), currentRoomName));
            if (roomTransform == null) continue;

            // Directions possibles pour les voisins
            Vector2Int[] dirs = { Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.down };
            DoorDirection[] dirEnum = { DoorDirection.Up, DoorDirection.Left, DoorDirection.Right, DoorDirection.Down };

            for (int i = 0; i < dirs.Length; i++)
            {
                int nx = x + dirs[i].x;
                int ny = y + dirs[i].y;

                if (nx < 0 || nx >= gridSize || ny < 0 || ny >= gridSize) continue;

                string neighborRoomName = grid[nx, ny];
                if (neighborRoomName == null) continue;

                // Trouver le DoorPoint correspondant à la direction
                DoorPoint point = currentRoomData.doorPoints.Find(p => p.direction == dirEnum[i]);
                if (point == null)
                {
                    Debug.LogWarning($"Pas de DoorPoint pour {currentRoomName} direction {dirEnum[i]}");
                    continue;
                }

                // Instancier la porte comme enfant de la pièce
                GameObject doorInstance = Instantiate(currentRoomData.doorPrefab, roomTransform);
                doorInstance.transform.localPosition = point.position;
                doorInstance.transform.localRotation = Quaternion.identity;

                // Configurer la porte
                Door doorScript = doorInstance.GetComponent<Door>();
                Room neighborRoom = (Room)System.Enum.Parse(typeof(Room), neighborRoomName);
                doorScript.SetTargetRoom(neighborRoom, point.playerSpawnOffset);
            }
        }
    }
}

    void PrintGrid()
    {
        Debug.Log("=== Nouvelle Maison Générée ===");
        for (int y = gridSize - 1; y >= 0; y--)
        {
            string row = "";
            for (int x = 0; x < gridSize; x++)
                row += (grid[x, y] ?? "___") + " ";
            Debug.Log(row);
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }
}
