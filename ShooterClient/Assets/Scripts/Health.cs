using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthUI ui;
    private int max;
    private int current;

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

    public void ApplyDamage(int damage)
    {
        current -= damage;
        UpdateHP();
    }

    private void UpdateHP()
    {
        ui.UpdateHealth(max, current);
    }
}
