using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _answerText;

    [SerializeField] private Image _panelImage;

    public Button Button;

    private Answer _answer;

    public void Init(Answer answer)
    {
        _answer = answer;
        _answerText.text = answer.Text;
        SetColor(Color.white);
    }

    public bool CheckIsCorrect()
    {
        return _answer.IsCorrect;
    }

    public void SetColor(Color color)
    {
        _panelImage.color = color;
    }
}