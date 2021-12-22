using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public Props props;
    public BlueGun blueGun;
    public RedGun redGun;
    public Baffle baffle;
    public UIStart uiStart;
    public UIMain uiMain;
    public UIEnd uiEnd;
    private bool isGaming;
    private float timer;
    private int second;

    private void Start()
    {
        baffle.onTrigger += GameEnd;
        uiStart.onClick += GameStart;
        uiEnd.onClick += GameRestart;
        GameRestart();
    }

    public void GameStart()
    {
        props.OnStart();
        blueGun.OnStart();
        redGun.OnStart();
        uiStart.gameObject.SetActive(false);
        uiMain.gameObject.SetActive(true);
        second = 0;
        uiMain.SetText(second.ToString());
        timer = 0;
        isGaming = true;
    }

    public void GameEnd(string text)
    {
        isGaming = false;
        props.OnEnd();
        blueGun.OnEnd();
        redGun.OnEnd();
        uiEnd.gameObject.SetActive(true);
        uiEnd.SetText(text);
    }

    public void GameRestart()
    {
        uiStart.gameObject.SetActive(true);
        uiMain.gameObject.SetActive(false);
        uiEnd.gameObject.SetActive(false);
        baffle.Reset();
    }

    private void Update()
    {
        if (isGaming)
        {
            timer += Time.deltaTime;
            if (timer > second + 1)
            {
                second++;
                uiMain.SetText(second.ToString());
            }
        }
    }
}
