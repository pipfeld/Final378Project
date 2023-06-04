using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class RandomFractalTreeSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public int treeCount = 5;

    private void Start()
    {
        SpawnRandomFractalTrees();
    }

    private void SpawnRandomFractalTrees()
    {
        List<Vector3Int> occupiedPositions = GetOccupiedPositions();

        for (int i = 0; i < treeCount; i++)
        {
            if (occupiedPositions.Count == 0)
                break;

            int randomIndex = Random.Range(0, occupiedPositions.Count);
            Vector3Int randomPosition = occupiedPositions[randomIndex];
            Vector3 spawnPosition = tilemap.CellToWorld(randomPosition) + new Vector3(0.5f, 0.5f, 0f); // Adjusted spawn position

            SpawnFractalTree(spawnPosition);

            occupiedPositions.RemoveAt(randomIndex);
        }
    }

    private List<Vector3Int> GetOccupiedPositions()
    {
        List<Vector3Int> occupiedPositions = new List<Vector3Int>();

        BoundsInt bounds = tilemap.cellBounds;

        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile != null)
                {
                    Vector3Int abovePosition = tilePosition + new Vector3Int(0, 1, 0);
                    if (tilemap.GetTile(abovePosition) != null && tilemap.GetTile(tilePosition + new Vector3Int(0, -1, 0)) == null) // Check if tile above is occupied and there is no tile below
                    {
                        occupiedPositions.Add(tilePosition);
                    }
                }
            }
        }

        return occupiedPositions;
    }

    private void SpawnFractalTree(Vector3 spawnPosition)
    {
        GameObject fractalTreeObject = new GameObject("FractalTree");
        fractalTreeObject.transform.position = spawnPosition;

        FractalTree fractalTree = fractalTreeObject.AddComponent<FractalTree>();
        fractalTree.randomize = true;
        fractalTree.ScrambleFactors();
    }
}
