using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelSpinner : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform _wheel;
    [SerializeField] private QuestionController _questionController;

    [Header("Spin Settings")]
    [SerializeField] private float _spinDuration = 4f;
    [SerializeField] private float _minSpinAngle = 3 * 360f;

    [Header("Sectors")]
    [SerializeField] private List<WheelSector> _sectors;

    private bool _isSpinning;
    private float _finalZRotation;

    public event Action<WheelPrize> OnWheelPrize;

    private void Awake()
    {
        InitializeSectors();
    }

    public void StartSpin()
    {
        if (_isSpinning) return;

        var prize = DeterminePrize();
        var sector = _sectors.FirstOrDefault(s => s.prize == prize);
        if (sector == null) return;
        float randomAngleInSector = UnityEngine.Random.Range(sector.angleStart, sector.angleEnd);
        StartCoroutine(SpinWheel(prize, randomAngleInSector));
    }

    private IEnumerator SpinWheel(WheelPrize prize, float targetSectorAngle)
    {
        _isSpinning = true;

        float spinTime = 3f;
        float spinSpeed = -720f;
        float elapsed = 0f;

        while (elapsed < spinTime)
        {
            float dt = Time.deltaTime;
            elapsed += dt;
            _wheel.Rotate(0, 0, spinSpeed * dt);
            yield return null;
        }

        float currentAngle = Mathf.Repeat(_wheel.eulerAngles.z, 360f);
        float targetAngleNorm = Mathf.Repeat(targetSectorAngle, 360f);

        float delta = Mathf.DeltaAngle(currentAngle, targetAngleNorm);
        float spinDirection = Mathf.Sign(spinSpeed);

        if (!Mathf.Approximately(delta, 0f) && Mathf.Sign(delta) != spinDirection)
        {
            delta += 360f * spinDirection;
        }

        float finalTime = 2f;
        elapsed = 0f;
        float fromAngle = currentAngle;
        float toAngle = fromAngle + delta;

        while (elapsed < finalTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / finalTime);
            float angle = Mathf.Lerp(fromAngle, toAngle, t);
            _wheel.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }

        _wheel.rotation = Quaternion.Euler(0, 0, targetAngleNorm);
        _finalZRotation = targetAngleNorm;
        _isSpinning = false;

        OnWheelPrize?.Invoke(prize);
    }


    private WheelPrize DeterminePrize()
    {
        bool isQuestionPrize = UnityEngine.Random.value < 0.7f;

        if (isQuestionPrize)
        {
            var questionPrizes = new List<WheelPrize>
            {
                WheelPrize.DailyLifeSkills,
                WheelPrize.ScienceAroundUs,
                WheelPrize.ArtAndCulture,
                WheelPrize.WorldAndSociety,
                WheelPrize.LifeAndSelfDevelopment,
                WheelPrize.Random
            };

            Shuffle(questionPrizes);

            foreach (var p in questionPrizes)
            {
                if (_questionController.HasAvailableQuestions(p))
                {
                    return p;
                }
            }

            return WheelPrize.Random;
        }
        else
        {
            var specialPrizes = new List<WheelPrize> { WheelPrize.Heart, WheelPrize.Restart, WheelPrize.Random };
            return specialPrizes[UnityEngine.Random.Range(0, specialPrizes.Count)];
        }
    }

    private void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    private void InitializeSectors()
    {
        _sectors = new List<WheelSector>();

        float sectorSize = 45f;

        for (int i = 0; i < 8; i++)
        {
            _sectors.Add(new WheelSector
            {
                prize = (WheelPrize)i,
                angleStart = (i * sectorSize),
                angleEnd = ((i + 1) * sectorSize)
            });
        }
    }
}