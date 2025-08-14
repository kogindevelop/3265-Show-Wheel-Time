using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockConfig", menuName = "Config/BlockConfig")]
public class BlockConfig : ScriptableObject
{
    public int ID;
    public WheelPrize WheelPrize;
    public string Name;
    public List<CardSetWithDifficulty> CardSets;
}

[Serializable]
public class CardSetWithDifficulty
{
    public Difficulty Difficulty;
    public List<CardConfig> Cards;
}

[Serializable]
public class Question
{
    public string Name;
    public List<Answer> Answers;
}
[Serializable]
public class Answer
{
    public string Text;
    public bool IsCorrect;
}
public enum Difficulty
{
    Novice,
    Expert,
    Master
}