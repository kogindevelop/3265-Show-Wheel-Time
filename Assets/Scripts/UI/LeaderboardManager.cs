using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _recordPrefab;

    private List<LeaderboardRecord> records;

    public void Init(string playerName, int playerScore)
    {
        records = new List<LeaderboardRecord>
    {
        new LeaderboardRecord("Alice", 1500),
        new LeaderboardRecord("Bob", 1300),
        new LeaderboardRecord("Charlie", 1200),
        new LeaderboardRecord("Diana", 1100),
        new LeaderboardRecord("Eve", 1000),
        new LeaderboardRecord("Frank", 900)
    };

        var playerRecord = new LeaderboardRecord(playerName, playerScore);
        records.Add(playerRecord);

        records.Sort((a, b) => b.Score.CompareTo(a.Score));

        foreach (var record in records)
        {
            Create(record);
        }
    }

    private void Create(LeaderboardRecord leaderboardRecord)
    {
        var name = leaderboardRecord.Name;
        var score = leaderboardRecord.Score;

        var blockDisplayPrefab = Instantiate(_recordPrefab, _container);
        var recordDisplay = blockDisplayPrefab.GetComponent<RecordDisplay>();
        recordDisplay.Init(name, score);
    }
}

public class LeaderboardRecord
{
    public string Name;
    public int Score;

    public LeaderboardRecord(string name, int score)
    {
        Name = name;
        Score = score;
    }
}