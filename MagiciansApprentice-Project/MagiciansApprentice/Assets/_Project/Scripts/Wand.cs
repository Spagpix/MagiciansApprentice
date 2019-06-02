using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public Transform Pivot;
    public Interactable LoadedItem; 

    public LayerMask InteractableLayer;
    public float Range = 1000;

    public bool IsAvailable = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Throw ();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(Physics.Raycast(transform.position, transform.forward, out var hit, Range, InteractableLayer))
            {
                Debug.Log("Hit: " + hit.transform.name);
                
                // TODO: Check for Interactable Component
                if(IsAvailable)
                    LoadItem(hit.transform.GetComponent<Interactable>());
            }
        }            
    }

    public void Throw()
    {
        var rb = LoadedItem.GetComponent<Rigidbody>();
        rb.velocity = Pivot.forward * 15;

        rb.useGravity = true;
        rb.isKinematic = false;
    }

    private void LoadItem(Interactable obj)
    {
        StartCoroutine(LoadItemRoutine(obj));
    }

    private IEnumerator LoadItemRoutine(Interactable obj)
    {
        yield return Move(obj.transform, Pivot);
        LoadedItem = obj;
    }

    private IEnumerator Move(Transform obj, Transform target)
    {
        IsAvailable = false;

        float timeElasped = 0;

        // Cache start position
        Vector3 startPosition = obj.position;

        // Start a loop and continue while timeElapsed is less 1 second 
        while (timeElasped < 1)
        {
            timeElasped += Time.deltaTime;

            var t = timeElasped;

            obj.position = Vector3.Lerp(startPosition, target.position, t);
            yield return null;
        }

        IsAvailable = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * Range);
    }
}