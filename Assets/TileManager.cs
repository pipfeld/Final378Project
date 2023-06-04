using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap plants;

    [SerializeField] private Tilemap champion;
    private int stack = 0;

    [SerializeField] private Tile Hidden;
    [SerializeField] private Tile Build;

    
    [SerializeField] private GameObject shape;
    [SerializeField] private GameObject sprite;
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

    public bool IsChapmion(Vector3Int position)
    {
        position = new Vector3Int(position.x - 1, position.y - 1, 0);
        TileBase tile = champion.GetTile(position);
        Debug.Log(position);
        //Debug.Log(tile);

        if (tile != null)
        {
            return true;
        }
        return false;
    }

    public void SetInteracted(Vector3Int position){
        if(stack <7){
            position = new Vector3Int(-4,-2+stack,0);
            stack++;
            champion.SetTile(position,Build);
        }
        
        
    }

    public void SetChampion(Vector3Int position)
    {
        GameObject obj = Instantiate(shape, new Vector3(-1.5f, -1.5f, 0), Quaternion.identity);
        int temp = stack;
        for(int i =0; stack>i; stack--){
            position = new Vector3Int(-4, -3 + stack, 0);

            champion.SetTile(position, null);

            GameObject S = Instantiate(sprite, new Vector3(-1.5f, -1.5f + (temp-stack), 0), Quaternion.identity);
            S.transform.parent = obj.transform;
            obj.GetComponent<Champion>().sprites.Add(S);
            SpriteRenderer sr = S.GetComponent<SpriteRenderer>();
            sr.sprite = Build.sprite;
        }
        
        
        

    }

    
}
