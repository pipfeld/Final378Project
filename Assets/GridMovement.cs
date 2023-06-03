using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private float timeToMove = 0.2f;
    private bool isMoving;
    private Vector3 origPos,targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A)&& !isMoving&& transform.position.x > -9)
            StartCoroutine(MovePlayer(Vector3.left));
        
        if (Input.GetKey(KeyCode.D)&& !isMoving && transform.position.x<-4)
            StartCoroutine(MovePlayer(Vector3.right));

        if(Input.GetKeyDown(KeyCode.Space)&& !isMoving){
            //Debug.Log("Here");
            Vector3Int position = new Vector3Int((int)transform.position.x,(int)transform.position.y,0);
            if(GameManager.instance.tileManager.IsInteractable(position)){
                GameManager.instance.tileManager.SetInteracted(position);
                
            }
            if (GameManager.instance.tileManager.IsChapmion(position))
            {
                GameManager.instance.tileManager.SetChampion(position);

            }
        }
    }
    private IEnumerator MovePlayer(Vector3 direction){
        isMoving = true;
        
        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos +direction;

        while(elapsedTime < timeToMove){
            transform.position = Vector3.Lerp(origPos,targetPos,(elapsedTime/timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
