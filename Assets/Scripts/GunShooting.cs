using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class GunShooting : MonoBehaviour
{
    public GameObject target;
    public GameObject aCamera;
    private AudioSource sound;
    private LineRenderer lineofFire;
    public GameObject startFire;
    public GameObject enemy, battleSound;
    public AudioClip clip1, clip2, clip3, clip4;
    public static bool isDragonKilled = false;
    public int amountOfHits = 0;
    public RawImage[] lives = new RawImage[5];
    private bool isIn;
    public GameObject dragonMission;
    public float targetTime = 20;
    public GameObject mission;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        lineofFire = GetComponent<LineRenderer>();


    }
  
    // Update is called once per frame
    void Update()
    {
        isIn = enemy.GetComponent<MonsterBehaviour>().isPlayerIn;
        if (isDragonKilled)
            targetTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
            {
                target.transform.position = hit.point;
                StartCoroutine(Fire());


                if (hit.collider.gameObject == enemy.gameObject && !isDragonKilled && isIn)
                {
                    amountOfHits++;
                    sound.PlayOneShot(clip2);
                    lives[amountOfHits - 1].gameObject.SetActive(false);
                }
                if (amountOfHits == 5 && !isDragonKilled)
                {


                    Animator enemyAnimator = enemy.GetComponent<Animator>();
                    enemyAnimator.SetInteger("State", 2);
                    dragonMission.gameObject.SetActive(false);
                    mission.gameObject.SetActive(true);
                    sound.PlayOneShot(clip3);
                    StartCoroutine(playSoundAfterFiveSeconds());
                    isDragonKilled = true;
                    battleSound.gameObject.SetActive(false);

                    enemy.GetComponent<AudioSource>().enabled = false;
                    enemy.GetComponent<SphereCollider>().enabled = false;
                }

            }
        }
     
    }

    IEnumerator Fire()
    {
        sound.PlayOneShot(clip1);
        // draw line of fire
        lineofFire.enabled = true;
        lineofFire.SetPosition(0, startFire.transform.position);
        lineofFire.SetPosition(1, target.transform.position);
        yield return new WaitForSeconds(0.02f);
        lineofFire.enabled = false;

    } 

    IEnumerator playSoundAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);
        sound.PlayOneShot(clip4);
    }
}
