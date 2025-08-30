using UnityEngine;

public class SnowZone : MonoBehaviour
  {
     public GameObject effect;


    // Start is called before the first frame update
    void Start()
    {
     }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             effect.gameObject.SetActive(true);
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             effect.gameObject.SetActive(false);
        }
    }
}
