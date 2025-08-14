using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    [SerializeField] private Image _heartImage;
    [SerializeField] private float _heartValue = 0;

    public float CurrentValue => _heartValue;

    public void SetHeartValue(float value = 1f)
    {
        _heartValue = Mathf.Clamp01(value);
        UpdateHp();
    }

    public bool IsHeartEmpty()
    {
        return _heartValue <= 0f;
    }

    private void UpdateHp()
    {
        _heartImage.fillAmount = _heartValue;
    }
}