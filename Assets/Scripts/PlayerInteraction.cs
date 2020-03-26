using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;

public class PlayerInteraction : MonoBehaviour
{
    /// <summary>
    /// Object CRUD (Create,Read,Update,Delete)
    /// TODO Select/Read,Edit/Update,Delete object.
    /// </summary>
    [SerializeField] private Agents unit;

    void Update()
    {
        // if Left Click create and Object
        if (Input.GetMouseButtonDown(0))   
        {
            CreateObjectAtRay();    
        }
    }

    /// <summary>
    /// Draw an object if ray hit the floor
    /// </summary>
    private void CreateObjectAtRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            // Debug.DrawRay(Camera.main.transform.position, hit.point.normalized, Color.red, 3.0f);
            if(hit.collider.gameObject.GetComponent<Agents>())
            {
                //Draw an object if ray hit the floor.
                Debug.Log("Hit a " + hit.collider.gameObject.GetComponent<Agents>());
                if(hit.collider.gameObject.GetComponent<Agents>().unitType == UnitType.Floor)Instantiate(unit.gameObject,hit.point,Quaternion.identity);
            }
        }

    }

}
