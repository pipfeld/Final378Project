using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth,attack;
    private int health;
    public float speed;
    [SerializeField] 
    private battlehealthbar healthBar;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<battlehealthbar>();
        health = maxHealth;
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime *speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        GameObject temp = other.collider.gameObject;
        
        if (temp.CompareTag("Player"))
        {
            Debug.Log("here");
            health -= temp.GetComponent<Champion>().damage;
            healthBar.setHealthBar(health, maxHealth);
            GetComponent<Rigidbody2D>().velocity = new Vector2(4,0);
            if (health <= 0)
            { 
                Destroy(this.gameObject);
            }
        }

        if (temp.CompareTag("Gate"))
        {
            Debug.Log("Gate");
            health -= 2;
            gameManager.health -= attack;
            healthBar.setHealthBar(health, maxHealth);
            GetComponent<Rigidbody2D>().velocity = new Vector2(6, 0);
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
