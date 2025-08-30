using UnityEngine;

public class NextSceneEntry : MonoBehaviour
{
    public GameObject right;
    public GameObject left;
    public GameObject playerObject;

    private PlayerBehaviour playerBehaviour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        right.SetActive(true);
        left.SetActive(true);
        playerBehaviour.enabled = false;
    }
}
