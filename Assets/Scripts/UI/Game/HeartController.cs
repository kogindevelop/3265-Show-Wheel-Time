using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private List<HeartDisplay> _hearts;

    public event Action OnHeartBroken;

    private const int HealthPerHeart = 2;

    public void InitializeHearts(int totalHealthPoints)
    {
        int hpLeft = Mathf.Clamp(totalHealthPoints, 0, _hearts.Count * HealthPerHeart);

        foreach (var heart in _hearts)
        {
            if (hpLeft >= HealthPerHeart)
            {
                heart.SetHeartValue(1f);
                hpLeft -= HealthPerHeart;
            }
            else if (hpLeft == 1)
            {
                heart.SetHeartValue(0.5f);
                hpLeft = 0;
            }
            else
            {
                heart.SetHeartValue(0f);
            }
        }
    }

    public void ApplyDamage()
    {
        float damageAmount = 0.5f;
        for (int i = _hearts.Count - 1; i >= 0; i--)
        {
            if (_hearts[i].IsHeartEmpty()) continue;

            float current = _hearts[i].CurrentValue;
            float newValue = current - damageAmount;

            if (newValue <= 0f)
            {
                _hearts[i].SetHeartValue(0f);
                OnHeartBroken?.Invoke();
                damageAmount = -newValue;
            }
            else
            {
                _hearts[i].SetHeartValue(newValue);
                break;
            }

            if (damageAmount <= 0f) break;
        }
    }

    public void RestoreHeart()
    {
        foreach (HeartDisplay heart in _hearts)
        {
            if (heart.CurrentValue < 1)
            {
                heart.SetHeartValue(1f);
                break;
            }
        }
    }

    public bool AreAllHeartsEmpty()
    {
        foreach (var heart in _hearts)
        {
            if (!heart.IsHeartEmpty()) return false;
        }
        return true;
    }
}