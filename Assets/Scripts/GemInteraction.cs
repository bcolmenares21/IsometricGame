using UnityEngine;
using UnityEngine.InputSystem;

public class GemInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GemManager gemManager;

    private bool isPlayerInRange;
    private bool collected;

    private void Awake()
    {
        interactText.SetActive(false);
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

        isPlayerInRange = false;
        interactText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactText.SetActive(true);
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
        if (!isPlayerInRange || collected)
            return;

        collected = true;

        gemManager.CollectGem();
        interactText.SetActive(false);
        gameObject.SetActive(false);
    }
}
