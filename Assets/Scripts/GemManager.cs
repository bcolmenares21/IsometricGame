using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GemManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private TextMeshProUGUI instructions;
    [SerializeField] private string gameOverSceneName = "GameOver";

    private int remainingGems = 3;

    public bool HasAllGems => remainingGems == 0;
    public bool HasSpokenToMonster { get; private set; }

    private void Start()
    {
        UpdateCounter();
        instructions.text = "Habla con el monstruo";
    }

    public void TalkToMonster()
    {
        HasSpokenToMonster = true;

        instructions.text = HasAllGems
            ? "Regresa con el monstruo"
            : "Busca las tres gemas";
    }

    public void CollectGem()
    {
        if (remainingGems <= 0)
            return;

        remainingGems--;
        UpdateCounter();

        if (HasAllGems)
        {
            instructions.text = "Regresa con el monstruo";
        }
    }

    private void UpdateCounter()
    {
        counterText.text = "X" + remainingGems;
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}