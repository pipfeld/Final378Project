using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class RandomFractalTreeSpawner : MonoBehaviour
{
    [SerializeField] public Tilemap tilemap;
    [SerializeField] public int treeCount = 5;
    [SerializeField] public int granularity = 5;
    [SerializeField] public float groundLevel = -2.0f;
    [SerializeField] public float skipChance = 0.7f;

    private void Start()
    {
        SpawnRandomFractalTrees();
    }

    private void SpawnRandomFractalTrees()
    {
        List<Vector3> occupiedPositions = GetOccupiedPositions();

        for (int i = 0; i < treeCount; i++)
        {
            if (occupiedPositions.Count == 0)
                break;

            int randomIndex = Random.Range(0, occupiedPositions.Count);
            Vector3 randomPosition = occupiedPositions[randomIndex];
            Vector3 spawnPosition = tilemap.CellToWorld(Vector3Int.RoundToInt(randomPosition)) + new Vector3(0.0f, 0.0f, 0f); // Adjusted spawn position

            SpawnFractalTree(spawnPosition);

            occupiedPositions.RemoveAt(randomIndex);
        }
    }

    private List<Vector3> GetOccupiedPositions()
    {
        List<Vector3> occupiedPositions = new List<Vector3>();

        BoundsInt bounds = tilemap.cellBounds;

        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile != null)
                {
                    Vector3 spawnPosition = tilemap.CellToWorld(tilePosition) + new Vector3(Random.Range(-0.4f, 0.4f), 0.0f, 0f); // Adjusted spawn position
                    spawnPosition.y = groundLevel;
                    for (int i = 0; i < granularity; i++)
                    {
                        if (Random.value <= skipChance) // Skip adding position based on skipChance probability
                            continue;

                        float xOffset = Random.Range(-0.4f, 0.4f);
                        //float yOffset = Random.Range(-0.4f, 0.4f);
                        Vector3 offsetPosition = spawnPosition + new Vector3(xOffset, 0f, 0f);

                        if (!IsPositionOccupied(offsetPosition, occupiedPositions))
                            occupiedPositions.Add(offsetPosition);
                    }
                }
            }
        }

        return occupiedPositions;
    }

    private bool IsPositionOccupied(Vector3 position, List<Vector3> occupiedPositions)
    {
        // Check if any occupied position is within a certain threshold distance of the given position
        float thresholdDistance = 0.2f;

        foreach (Vector3 occupiedPosition in occupiedPositions)
        {
            if (Vector3.Distance(occupiedPosition, position) <= thresholdDistance)
                return true;
        }

        return false;
    }



    private void SpawnFractalTree(Vector3 spawnPosition)
    {
        GameObject fractalTreeObject = new GameObject("FractalTree");
        float xOffset = Random.Range(-0.2f, 0.2f);
        fractalTreeObject.transform.position = spawnPosition + new Vector3(xOffset, 0f, 0f);

        FractalTree fractalTree = fractalTreeObject.AddComponent<FractalTree>();
        fractalTree.randomize = true;
        fractalTree.ScrambleFactors();
    }
}
