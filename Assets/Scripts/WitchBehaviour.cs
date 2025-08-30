using DialogueEditor;
using UnityEngine;

public class WitchBehaviour : MonoBehaviour
{
    public NPCConversation firstConversation;    
    public NPCConversation finalConversation;   

    public GameObject playerObject;
    public GameObject InventorBar;
    public GameObject missionBox;
    public GameObject missionBox1;

    private Animator animator;
    private PlayerBehaviour playerBehaviour;

    private bool conversationStarted = false;
    private bool firstDialogueDone = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();
        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    private void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !conversationStarted)
        {
            conversationStarted = true;

            InventoryManager manager = FindObjectOfType<InventoryManager>();
            ItemType[] requiredItems = { ItemType.Garlic, ItemType.Sage, ItemType.Mushroom };

            if (!firstDialogueDone)
            {
                missionBox1.SetActive(false);
                firstDialogueDone = true;
                StartDialogue(firstConversation); 
            }
            else if (manager != null && manager.HasAtLeastTwoOfEach(requiredItems))
            {
                foreach (ItemType type in requiredItems)
                {
                    manager.RemoveItems(type, 3);
                }
                missionBox.SetActive(false);
                StartDialogue(finalConversation);  
            }
            else
            {
                Debug.Log("You still don't have 3 of each required item.");
                conversationStarted = false;
            }
        }
    }

    private void StartDialogue(NPCConversation convo)
    {
        if (convo != null && animator != null)
        {
            animator.SetInteger("State", 1);

            if (playerBehaviour != null)
                playerBehaviour.enabled = false;

            ConversationManager.Instance.StartConversation(convo);
        }
    }

    private void OnConversationEnded()
    {
        Debug.Log("Conversation ended");
        playerBehaviour.enabled = true;
        InventorBar.SetActive(true);
        animator.SetInteger("State", 0);
        IngredientsBehaviour.isActive = true;
        conversationStarted = false;
        missionBox.SetActive(true);    
    }
}
