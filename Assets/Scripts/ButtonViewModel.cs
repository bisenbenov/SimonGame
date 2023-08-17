using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonViewModel : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;

    public event EventHandler<Button> onButtonClicked;

    private void OnEnable()
    {
        foreach (var button in _buttons)
        {
            button.onClick.AddListener(() => OnClick(button));
        }
    }

    private void OnClick(Button button)
    {
        onButtonClicked?.Invoke(this, button);
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.onClick.RemoveListener(() => OnClick(button));
        }
    }
}
