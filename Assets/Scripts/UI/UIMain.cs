using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMain : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetText(string text)
    {
        this.text.text = $"Time: {text}";
    }
}
