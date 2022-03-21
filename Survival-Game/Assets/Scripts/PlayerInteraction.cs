using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    SphereCollider interactionArea;
    List<Collider> nearObjects;

    // Start is called before the first frame update
    void Start()
    {
        interactionArea = GetComponent<SphereCollider>();
        nearObjects = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) // Layer number should be equal to interactable layer
        {
            nearObjects.Add(other);
            Debug.Log(other.name + " added to nearObjects. List size is now " + nearObjects.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            nearObjects.Remove(other);
            Debug.Log(other.name + " removed from nearObjects. List size is now " + nearObjects.Count);  
        }
    }
}
