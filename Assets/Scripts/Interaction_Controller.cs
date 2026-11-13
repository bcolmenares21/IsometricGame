using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class InteractionController : MonoBehaviour




{


    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private GameObject interactText;
    [SerializeField] private TextMeshProUGUI dialogue;




    private bool isPlayerInRange = false;




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
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactText.SetActive(true);
            Debug.Log("esta en rango");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactText.SetActive(false);
            Debug.Log("ya no esta en rango");
        }
    }


    private void PerformInteraction(InputAction.CallbackContext context)
    {
        if (isPlayerInRange)
        {
            Debug.Log("Interaccion con objeto");
            interactText.SetActive(false);
            gameObject.SetActive(false);
            dialogue.text = "Estoy muy triste, he perdido mis tres gemas preciosas. Tráemelas y te dejaré pasar";


        }
    }
}