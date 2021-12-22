using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStart : MonoBehaviour
{
    [SerializeField] private Button button;
    public event UnityAction onClick;

    private void Start()
    {
        button.onClick.AddListener(() => onClick?.Invoke());
    }
}
