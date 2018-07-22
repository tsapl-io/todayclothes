﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickableInputField : MonoBehaviour {
    [SerializeField]
    private InputField _targetInputField;

    [SerializeField]
    private Button _guardButton;

    public UnityEvent OnFocus = new UnityEvent();

    void Start()
    {
        _guardButton.onClick.AddListener(() =>
        {
            _targetInputField.ActivateInputField();
            OnFocus.Invoke();
        });
    }
}
