using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Init(string name, int score)
    {
        _nameText.text = name;
        _scoreText.text = score.ToString();
    }
}