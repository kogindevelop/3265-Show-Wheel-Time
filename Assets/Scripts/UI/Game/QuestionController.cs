using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private BlockSetup _blockSetup;

    private Dictionary<WheelPrize, HashSet<Question>> _usedQuestionsByPrize = new();
    private Dictionary<WheelPrize, List<Question>> _allQuestionsByPrize = new();

    public void InitializeQuestions(int level)
    {
        _allQuestionsByPrize.Clear();
        _usedQuestionsByPrize.Clear();

        foreach (var block in _blockSetup.Blocks)
        {
            if (!_allQuestionsByPrize.ContainsKey(block.WheelPrize))
            {
                _allQuestionsByPrize[block.WheelPrize] = new List<Question>();
                _usedQuestionsByPrize[block.WheelPrize] = new HashSet<Question>();
            }

            foreach (var set in block.CardSets)
            {
                if ((int)set.Difficulty < level)

                    foreach (var card in set.Cards)
                    {
                        if (card.Question != null)
                            _allQuestionsByPrize[block.WheelPrize].Add(card.Question);
                    }
            }
        }
    }

    public bool TryGetNextQuestion(WheelPrize prize, out Question question)
    {
        question = null;

        if (!_allQuestionsByPrize.ContainsKey(prize) || _allQuestionsByPrize[prize].Count == 0)
            return false;

        var all = _allQuestionsByPrize[prize];
        var used = _usedQuestionsByPrize[prize];
        var unused = all.Where(q => !used.Contains(q)).ToList();

        if (unused.Count == 0)
            return false;

        question = unused[UnityEngine.Random.Range(0, unused.Count)];
        used.Add(question);

        return true;
    }

    public bool HasAvailableQuestions(WheelPrize prize)
    {
        if (!_allQuestionsByPrize.ContainsKey(prize) || _allQuestionsByPrize[prize].Count == 0)
            return false;

        return _allQuestionsByPrize[prize].Any(q => !_usedQuestionsByPrize[prize].Contains(q));
    }

    public bool HasAnyQuestionsLeft()
    {
        foreach (var prize in _allQuestionsByPrize.Keys)
        {
            if (HasAvailableQuestions(prize))
                return true;
        }

        return false;
    }

    public bool TryGetRandomQuestion(out Question question)
    {
        question = null;

        var availablePrizes = _allQuestionsByPrize.Keys
            .Where(HasAvailableQuestions)
            .ToList();

        if (availablePrizes.Count == 0)
            return false;

        var randomPrize = availablePrizes[UnityEngine.Random.Range(0, availablePrizes.Count)];

        return TryGetNextQuestion(randomPrize, out question);
    }

    public int GetAllQuestionCount()
    {
        return _allQuestionsByPrize.Values.Sum(list => list.Count);
    }

    public int GetUsedQuestionCount()
    {
        return _usedQuestionsByPrize.Values.Sum(set => set.Count);
    }
}