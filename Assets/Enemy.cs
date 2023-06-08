using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health,attack;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
            if (health <= 0)
            {
                
                Destroy(this.gameObject);
            }
        }
    }
}
