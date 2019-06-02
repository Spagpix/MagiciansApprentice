using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{

    public Transform Pivot;

    public LayerMask InteractableLayer; 
    public float Range = 1000;

    
    private void Update()
         
    {
        // If user presses fire button
        // Shoot a ray 
        // if we hit an object & it is Interactable     
        // Do what we want to do
       if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire Button Down");
           
      
            if (Physics.Raycast(transform.position, transform.forward, out var hit, Range, InteractableLayer))
            {
                Debug.Log("Hit: " + hit.transform.name);
                // TO DO : CHeck for interactable Component 
                hit.transform.position = Pivot.transform.position; 

            }
        }

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * Range);
    }
}

