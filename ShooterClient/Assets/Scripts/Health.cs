using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthUI ui;
    private int max;
    private int current;
    public int Current { get { return current; } }

    public void SetMax(int max)
    {
        this.max = max;
        UpdateHP();
    }

    public void SetCurrent(int current)
    {
        this.current = current;
        UpdateHP();
    }

    public bool ApplyDamageAndDie(int damage)
    {
        current -= damage;
        UpdateHP();
        return current <= 0;
    }

    private void UpdateHP()
    {
        ui.UpdateHealth(max, current);
    }

}
