using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private TextMeshProUGUI _topicText;

    public Button Button;

    public void Init(int num, string topic)
    {
        _numberText.text = $"Block {num}";
        _topicText.text = topic;
    }
}