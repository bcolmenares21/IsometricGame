using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MonsterInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private GameObject interactText;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private GemManager gemManager;

    [SerializeField, TextArea]
    private string message =
        "Estoy muy triste, he perdido mis tres gemas preciosas. " +
        "Tráemelas y te dejaré pasar";

    private bool isPlayerInRange;
    private Coroutine dialogueCoroutine;

    private void Awake()
    {
        interactText.SetActive(false);
        dialogue.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (interactAction != null)
        {
            interactAction.action.Enable();
            interactAction.action.performed += PerformInteraction;
        }
    }

    private void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.action.performed -= PerformInteraction;
        }

        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }

        isPlayerInRange = false;
        interactText.SetActive(false);
        dialogue.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactText.SetActive(dialogueCoroutine == null);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactText.SetActive(false);
        }
    }

    private void PerformInteraction(InputAction.CallbackContext context)
    {
        if (!isPlayerInRange)
            return;

        // La primera conversación siempre presenta la misión.
        if (gemManager.HasSpokenToMonster && gemManager.HasAllGems)
        {
            gemManager.GoToGameOver();
            return;
        }

        if (dialogueCoroutine != null)
            return;

        gemManager.TalkToMonster();
        dialogueCoroutine = StartCoroutine(ShowDialogue());
    }

    private IEnumerator ShowDialogue()
    {
        interactText.SetActive(false);

        dialogue.text = message;
        dialogue.gameObject.SetActive(true);

        yield return new WaitForSeconds(10f);

        dialogue.gameObject.SetActive(false);
        dialogueCoroutine = null;

        interactText.SetActive(isPlayerInRange);
    }
}