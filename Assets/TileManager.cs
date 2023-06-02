using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap plants;

    [SerializeField] private Tile Hidden;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var position in plants.cellBounds.allPositionsWithin){
            if (plants.HasTile(position))
            {
                Debug.Log(position);
                plants.SetTile(position,Hidden);
            }
        }
    }

    public bool IsInteractable(Vector3Int position){
        position = new Vector3Int(position.x-1, position.y-1,0);
        TileBase tile = plants.GetTile(position);
        Debug.Log(position);
        //Debug.Log(tile);

        if(tile != null){
            return true;
        }
        return false;
    }

    
}
