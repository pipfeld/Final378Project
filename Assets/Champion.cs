using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Champion : MonoBehaviour
{
    public Tile[] stack;
    public int health,maxHealth;
    public int damage;
    public List<GameObject> sprites;

    [SerializeField] 
    private battlehealthbar healthBar;



    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<battlehealthbar>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right *Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other) {
       
        GameObject temp = other.collider.gameObject;
        
        if(temp.CompareTag("Enemy")){
            Debug.Log("here");
            health -= temp.GetComponent<Enemy>().attack;
            healthBar.setHealthBar(health, maxHealth);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-3,0);
            if(health <=0){
                foreach(GameObject s in sprites){
                    Destroy(s);
                }
                Destroy(this.gameObject);
            }
        }
    }

}
