using System.Collections.Generic;
using UnityEngine;
using GameLogic;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    /// <summary>
    /// Object CRUD (Create,Read,Update,Delete)
    /// TODO Select/Read,Edit/Update,Delete object.
    /// </summary>
    [SerializeField] private List<Agents> units;
    [SerializeField] private GameObject creationMenu;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float searchRange = 2.0f;
    [SerializeField] private GameObject selectionSphere;

    Vector3 newAgentPosition; 
    Quaternion newAgentRotation;
    private bool isClearArea = false;
    Vector3 searchCenter;

    void Update()
    {
        // if clearing activated show highlight sphere 
        if (isClearArea)
        {
            HighlightPointedArea();
            // on mouse click clear the highlighted area
            if (Input.GetMouseButtonDown(0))
            {
                ClearArea();
            }
        }
        // if Left Click create and Object
        else if (Input.GetMouseButtonDown(0))   
        {
            textMeshPro.text = "Lets Build";
            CallMenuAtRay();    
        }
        else
        {
            // View Selected Object
            ViewObjectAtRay();
        }
    }

    /// <summary>
    /// Launch Creatin menu if ray hit the floor and update position and rotations
    /// </summary>
    private void CallMenuAtRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            // Debug.DrawRay(Camera.main.transform.position, hit.point.normalized, Color.red, 3.0f);
            if(hit.collider.gameObject.GetComponent<Agents>())
            {
                //Draw an object if ray hit the floor.
                // Debug.Log("Hit a " + hit.collider.gameObject.GetComponent<Agents>());
                int unityType = UnityEngine.Random.Range(0, units.Count) ;
                if(hit.collider.gameObject.GetComponent<Agents>().unitType == UnitType.Floor)
                {
                    UpdateNewAgentPlacment(hit.point,Quaternion.identity);
                    ShowCreationMenu();
                }
            }
        }

    }

    private void UpdateNewAgentPlacment(Vector3 agentPos, Quaternion agentRot)
    {
        newAgentPosition = agentPos;
        newAgentRotation = agentRot;
    }

    /// <summary>
    /// Show Agent Stats
    /// </summary>
    private void ViewObjectAtRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            if(hit.collider.gameObject.GetComponent<Agents>())
            {
                //Show Agent Stats
                // Debug.Log("Hit a " + hit.collider.gameObject.GetComponent<Agents>());
                int unityType = UnityEngine.Random.Range(0, units.Count) ;
                if(hit.collider.gameObject.GetComponent<Agents>().unitType != UnitType.Floor)
                    hit.collider.gameObject.GetComponent<Agents>().ShowIcon(2.0f);
            }
        }

    }

    /// <summary>
    /// Show Agent Creation Menu
    /// </summary>
    private void ShowCreationMenu()
    {
        creationMenu.SetActive(true);
    }
    
    /// <summary>
    /// Hide Agent Creation Menu
    /// </summary>
    public void SpawnAgent(string agentType)
    {
        // Debug.Log("Spawning Agent");
        foreach (Agents agent in units)
        {
            if(agent.unitType == Agents.GetAgentFromString(agentType))
            {
                Instantiate(agent.gameObject, newAgentPosition, newAgentRotation);
            }
        }
        creationMenu.SetActive(false);
    }


    /// <summary>
    /// Activate the Clear Tool
    /// </summary>
    public void ActivateClearArea()
    {
        isClearArea = true;
    }
    
    /// <summary>
    /// Draw a sphere where Pointed
    /// </summary>
    /// <param name="drawCenter"></param>
    private void DrawSelectionSphere(Vector3 drawCenter)
    {
        GameObject tempSelection =  Instantiate(selectionSphere, drawCenter, Quaternion.identity);
        Destroy(tempSelection,0.01f);
        // selectionSphere.transform.localPosition = drawCenter;
    }

    /// <summary>
    /// Highlight Pointed Area
    /// </summary>
    public void HighlightPointedArea()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            if(hit.collider.gameObject.GetComponent<Agents>()!= null)
            {
                // update search center to where pointer placed
                searchCenter = hit.point;
                Debug.Log(hit.collider.gameObject.GetComponent<Agents>().GetAgentName() + " Highlight Selection at " + searchCenter);
                DrawSelectionSphere(searchCenter);
            }
        }
    }

    /// <summary>
    /// Clear the highlighted area
    /// </summary>
    private void ClearArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(searchCenter, searchRange);
        int i = 0;
        GameObject objectsToClear;
        while (i < hitColliders.Length)
        {
            objectsToClear = hitColliders[i].gameObject;
            // if search sphere collided with agent
            if(objectsToClear.GetComponent<Agents>() != null)
            {

                if((objectsToClear.GetComponent<Agents>().unitType != UnitType.Floor)
                    && (objectsToClear.GetComponent<Agents>().unitType != UnitType.Player))
                {
                    // Kills the game object in 5 seconds after loading the object
                    Destroy(objectsToClear, 2);
                }
            }
            i++;
        }
        isClearArea = false;
    }


}
