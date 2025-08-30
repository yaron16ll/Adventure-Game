using DialogueEditor;
using UnityEngine;

public class HunterBehaviour : MonoBehaviour
{
    public NPCConversation firstConversation;
    public NPCConversation finalConversation;

    public GameObject playerObject;
    public GameObject missionBox2;
    public GameObject monster;
    public GameObject gun;
    public GameObject crossHair;

    private bool isFirstConversation = false;
    private Animator animator;
    private PlayerBehaviour playerBehaviour;
    public GameObject handGun;

    private bool conversationStarted = false;
    private bool firstDialogueDone = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (playerObject != null)
            playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();

        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    private void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || conversationStarted) return;

        conversationStarted = true;

        if (!firstDialogueDone)
        {
            firstDialogueDone = true;
            isFirstConversation = true;
            StartDialogue(firstConversation);

            return;
        }
        else if(GunShooting.isDragonKilled)
        {
            StartDialogue(finalConversation);
            isFirstConversation = false;
            handGun.SetActive(false);
            crossHair.SetActive(false);
            animator.SetInteger("State", 2);
        }
        else
        {
            conversationStarted = false;
        }
    }

    private void StartDialogue(NPCConversation convo)
    {
        if (convo == null) { conversationStarted = false; return; }

        if (playerBehaviour != null)
            playerBehaviour.enabled = false;
        missionBox2.SetActive(false);
        ConversationManager.Instance.StartConversation(convo);
        // אחרי השיחה הראשונה: בדיקה אם כל 80 התוקנו
        
    }

    private void OnConversationEnded()
    {
        if (isFirstConversation)
        {
            gun.GetComponent<BoxCollider>().enabled = true;

            playerBehaviour.enabled = true;
            monster.SetActive(true);
            IngredientsBehaviour.isActive = true;
            conversationStarted = false;
        }
        else
        {
            playerBehaviour.enabled = true;
        }
    }
}
