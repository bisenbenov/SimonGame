using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ButtonsLight : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private ButtonsModel _buttonsModel;
    
    private List<Button> _queue = new();
    private int _lightUps;

    public event EventHandler OnQuessRight;
    public event EventHandler OnQuessWrong;

    private void Awake()
    {
        _lightUps = 1;

        EnableButtons(false);
    }

    private void Start()
    {
        StartCoroutine(LightUp());
    }

    private void EnableButtons(bool state)
    {
        foreach (var button in _buttons)
        {
            button.interactable = state;
        }
    }

    private IEnumerator LightUp()
    {
        EnableButtons(true);

        for (int i = 0; i < _lightUps; i++)
        {
            yield return new WaitForSeconds(1.2f);
            var button = _buttons[Random.Range(0, _lightUps - 1)];
            _queue.Add(button);
            StartCoroutine(LightButton(button));
        }

        yield break;
    }

    private IEnumerator LightButton(Button button)
    {
        var originalColor = button.GetComponent<Image>().color;
        button.GetComponent<Image>().color = Color.Lerp(originalColor, Color.white, .65f);

        yield return new WaitForSeconds(0.7f);
        button.GetComponent<Image>().color = originalColor;

        yield break;
    }

    public IEnumerator CompareQueues(IReadOnlyList<Button> userQueue)
    {
        for (int i = 0; i < userQueue.Count; i++)
        {
            if (_queue[i].Equals(userQueue[i]))
            {
                continue;
            }
            else
            {
                OnQuessWrong?.Invoke(this, EventArgs.Empty);
                EnableButtons(false);
                yield break;
            }
        }

        if (_queue.Count == userQueue.Count)
        {
            OnQuessRight?.Invoke(this, EventArgs.Empty);
            _lightUps++;
            
            _buttonsModel.ClearUserQueue();
            _queue.Clear();

            StartCoroutine(LightUp());
        }
    }
}
