using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private float timeToMove = 0.2f;
    [SerializeField] private bool bounded = true;
    [SerializeField] private float minX = -9f;
    [SerializeField] private float maxX = -4f;

    private bool isMoving;
    private Vector3 origPos, targetPos;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !isMoving && CanMoveLeft()){
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("Run", true);
            StartCoroutine(MovePlayer(Vector3.left));
        }

        if (Input.GetKey(KeyCode.D) && !isMoving && CanMoveRight()){
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("Run", true);
            StartCoroutine(MovePlayer(Vector3.right));
        }

        if(Input.GetKeyDown(KeyCode.Space)&& !isMoving){
            //Debug.Log("Here");
            Vector3Int position = new Vector3Int((int)transform.position.x,(int)transform.position.y,0);
            if(GameManager.instance.tileManager.IsInteractable(position)){
                GameManager.instance.tileManager.SetInteracted(position);
                GetComponent<Animator>().SetTrigger("interact");
                
            }
            if (GameManager.instance.tileManager.IsChapmion(position))
            {
                GetComponent<Animator>().SetTrigger("interact");
                GameManager.instance.tileManager.SetChampion(position);

            }
        }
    }

    private bool CanMoveLeft()
    {
        return !bounded || transform.position.x > minX;
    }

    private bool CanMoveRight()
    {
        return !bounded || transform.position.x < maxX;
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
        GetComponent<Animator>().SetBool("Run", false);
    }
}
