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

    private List<Crops> pieces;

    [SerializeField]
    private List<Tile> cropTiles;
    [SerializeField]
    private List<Crops> crops;
    
    void Start(){
        pieces = new List<Crops>();
    }

    

    public bool IsInteractable(Vector3Int position){
        position = new Vector3Int(position.x-1, position.y-1,0);
        TileBase tile = plants.GetTile(position);
        Debug.Log(tile);
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
        position = new Vector3Int(position.x-1, position.y-1,0);
        
        if(stack <7){
            
            int temp = cropTiles.IndexOf(plants.GetTile(position) as Tile);
            Debug.Log(crops[temp]);
            pieces.Add(crops[temp]);
            position = new Vector3Int(-4,-2+stack,0);
            stack++;
            champion.SetTile(position,crops[temp].sprite);
        }
        
        
    }

    public void SetChampion(Vector3Int position)
    {
        GameObject obj = Instantiate(shape, new Vector3(-1.5f, -1.5f, 0), Quaternion.identity);
        Champion stats = obj.GetComponent<Champion>();
        int temp = stack;
        for(int i =0; stack>i; stack--){
            position = new Vector3Int(-4, -3 + stack, 0);

            champion.SetTile(position, null);

            GameObject S = Instantiate(sprite, new Vector3(-1.5f, -1.5f + (temp-stack), 0), Quaternion.identity);
            S.transform.parent = obj.transform;
            stats.sprites.Add(S);
            SpriteRenderer sr = S.GetComponent<SpriteRenderer>();
            sr.sprite = pieces[stack-1].sprite.sprite;
            stats.damage += pieces[stack-1].damage;
            stats.health += pieces[stack-1].health;
            pieces.RemoveAt(stack-1);
        }
        
        obj.GetComponent<BoxCollider2D>().size = new Vector2(obj.GetComponent<BoxCollider2D>().size.x,temp);
        if(temp%2==1){
            obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, (temp) / 2);
        }else{
            obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, (temp) / 2-.5f);
        }
        
        

    }

    
}
