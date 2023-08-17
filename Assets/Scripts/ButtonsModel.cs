using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonsModel : MonoBehaviour
{
    [SerializeField] private ButtonViewModel _buttonViewModel;
    [SerializeField] private ButtonsLight _buttonsLight;

    private List<Button> _userQueue = new();
    public IReadOnlyList<Button> userQueue => _userQueue;

    private void OnEnable()
    {
        _buttonViewModel.onButtonClicked += AddToQueue;
    }

    private void AddToQueue(object sender, Button button)
    {
        _userQueue.Add(button);
        StartCoroutine(_buttonsLight.CompareQueues(userQueue));
    }

    public void ClearUserQueue()
    {
        _userQueue.Clear();
    }

    private void OnDisable()
    {
        _buttonViewModel.onButtonClicked -= AddToQueue;
    }
}
