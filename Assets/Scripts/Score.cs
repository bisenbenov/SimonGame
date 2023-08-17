using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ButtonsLight _buttonsLight;

    private int _score = 0;

    private void OnEnable()
    {
        _buttonsLight.OnQuessRight += ChangeScore;
        _buttonsLight.OnQuessWrong += SetDefault;
    }

    private void SetDefault(object sender, EventArgs e)
    {
        _score = 0;
        _scoreText.text = $"Score: {0}";
    }

    private void ChangeScore(object sender, EventArgs e)
    {
        _scoreText.text = $"Score: {++_score}";
    }

    private void OnDisable()
    {
        _buttonsLight.OnQuessRight -= ChangeScore;
        _buttonsLight.OnQuessWrong -= SetDefault;
    }
}
