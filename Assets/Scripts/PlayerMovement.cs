using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// WASD Inputs
    /// </summary>
    [SerializeField] private KeyCode moveForward = KeyCode.W;
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveBackward = KeyCode.S;
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private Transform body;
    [Range(0,10)]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float cellStep = 50.0f;

    void Update()
    {
        if(Input.GetKeyDown(moveForward))
        {
            LookAtDirection(new Vector3(0.0f, 0.0f, 0.0f));
            UpdatePlayerTarget(Vector3.forward);
        }
        if(Input.GetKeyDown(moveLeft))
        {
            LookAtDirection(new Vector3(0.0f, -90.0f, 0.0f));
            UpdatePlayerTarget(Vector3.left);

        }
        if(Input.GetKeyDown(moveBackward))
        {
            LookAtDirection(new Vector3(0.0f, 180.0f, 0.0f));
            UpdatePlayerTarget(Vector3.back);

        }
        if(Input.GetKeyDown(moveRight))
        {
            LookAtDirection(new Vector3(0.0f, 90.0f, 0.0f));
            UpdatePlayerTarget(Vector3.right);
        }

    }

    private void UpdatePlayerTarget(Vector3 direction)
    {
        
        Vector3 targetDirection = transform.position + (direction * cellStep);
        IEnumerator coroutine = MoveTowards(targetDirection);
        StartCoroutine(coroutine);
    }

    IEnumerator MoveTowards(Vector3 targetDirection)
    {
        // transform.Translate(direction * speed * cellStep * Time.deltaTime, Space.Self);
        // Move our position a step closer to the target.
        float step =  speed * Time.deltaTime; // calculate distance to move
        while(transform.position != targetDirection)
        {
            yield return new WaitForSeconds(0.001f);
            transform.position = Vector3.MoveTowards(transform.position, targetDirection, step);
        }
        yield return new WaitForSeconds(1.0f);
    } 

    private void LookAtDirection(Vector3 direction)
    {   
        body.localRotation = Quaternion.Euler(direction);
    }

}
