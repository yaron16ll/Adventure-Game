using UnityEngine;


public class BridgeBehaviour : MonoBehaviour
{

    public bool isStatuePlaceEntered = false;

    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        isStatuePlaceEntered = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        isStatuePlaceEntered = false;
    }

    
}

 
 
