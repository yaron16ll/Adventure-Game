using UnityEngine;

public class mountainHutBehaviour : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isPlayed = true;
    public GameObject missionBox1;
    public GameObject missionBox2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayed)
        {
            if (other.CompareTag("Player"))
            {
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                    missionBox1.SetActive(false);
                    missionBox2.SetActive(true);

                    isPlayed = false;

                }
            }
        }
    }
}
