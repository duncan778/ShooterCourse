using TMPro;
using UnityEngine;

public class LossCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int enemyLoss;
    private int playerLoss;

    public void SetEnemyLoss(int value)
    {
        enemyLoss = value;
        UpdateText();
    }

    public void SetPlayerLoss(int value)
    {
        playerLoss = value;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = $"{playerLoss} : {enemyLoss}";
    }
}
