using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Props : MonoBehaviour
{
    public GameObject prefab;
    public int height;
    public float width;
    public float startTime;
    public float repeatTime;
    public event UnityAction onReset;
    public int[] multiples;
    private List<Splitor> splitors = new List<Splitor>();

    private void Start()
    {
        height = Mathf.Abs(height) / 2;
        width = Mathf.Abs(width) / 2;
        InitSplitors();
        
    }

    public void OnStart()
    {
        SetSplitorsActive(true);
        InvokeRepeating("ResetSplitors", startTime, repeatTime);
    }

    public void OnEnd()
    {
        CancelInvoke("ResetSplitors");
        SetSplitorsActive(false);
    }

    public void SetSplitorsActive(bool value)
    {
        foreach (var item in splitors)
        {
            item.gameObject.SetActive(value);
        }
    }

    private void InitSplitors()
    {
        for (var z = -height; z < height; z += 2)
        {
            var point = new Vector3(0, 0, z);
            var go = Instantiate(prefab, point, Quaternion.identity);
            go.transform.SetParent(transform, true);
            var splitor = go.GetComponent<Splitor>();
            splitor.onInvisible += OnResetInvoke;
            splitor.gameObject.SetActive(false);
            splitors.Add(splitor);
        }
    }

    private void ResetSplitors()
    {
        for (var i = 0; i < splitors.Count; i++)
        {
            var multiple = multiples[Random.Range(0, multiples.Length)];
            var x = Random.Range(-width, width);
            splitors[i].SetMultiple(multiple, x);
        }
        OnResetInvoke();
    }

    public List<Splitor> GetSplitorsOnTheSide(float min, float max)
    {
        List<Splitor> list = new List<Splitor>();
        foreach (var item in splitors)
        {
            if (item.transform.position.z > min && item.transform.position.z < max)
            {
                list.Add(item.GetComponent<Splitor>());
            }
        }
        return list;
    }

    private void OnResetInvoke()
    {
        onReset?.Invoke();
    }
}
