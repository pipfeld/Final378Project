using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TileManager tileManager;

    public int health = 20;
    public int maxHealth = 20;
    [SerializeField] battlehealthbar healthBar;

    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }
        else{
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        tileManager = GetComponent<TileManager>();
    }
    
    private void Update(){
        healthBar.setHealthBar(health, maxHealth);
        if(health<=0){
            Debug.Log("you lost!");
        }
    }
}
