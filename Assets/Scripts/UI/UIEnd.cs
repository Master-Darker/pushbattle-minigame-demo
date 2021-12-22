using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEnd : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;
    public event UnityAction onClick;

    private void Start()
    {
        button.onClick.AddListener(() => onClick?.Invoke());
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }
}
