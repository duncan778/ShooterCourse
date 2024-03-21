using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform filledImage;
    [SerializeField] private float defaultWidth;

    private void OnValidate()
    {
        defaultWidth = filledImage.sizeDelta.x;
    }

    public void UpdateHealth(int max, int current)
    {
        float percent = (float)current / max;
        filledImage.sizeDelta = new Vector2(defaultWidth * percent, filledImage.sizeDelta.y);
    }
}
