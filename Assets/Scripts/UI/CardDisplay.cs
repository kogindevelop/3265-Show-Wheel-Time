using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private TextMeshProUGUI _nameText;

    public Button Button;

    public void Init(int num, string name)
    {
        _numberText.text = $"Card {num}";
        _nameText.text = name;
    }
}