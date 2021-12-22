using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Splitor : MonoBehaviour
{
    public int multiple;
    public TMP_Text text;
    public MeshRenderer mr;
    public Material[] materials;
    public event UnityAction onInvisible;

    public void SetMultiple(int multiple, float x)
    {
        this.multiple = multiple;
        text.text = $"X{multiple}";
        mr.material = materials[multiple];
        var position = transform.position;
        position.x = x;
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bullet":
                BulletEnter(other);
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Bullet":
                BulletExit(other);
                break;
            case "Baffle":
                onInvisible?.Invoke();
                break;
            default:
                break;
        }
    }

    private void BulletExit(Collider other)
    {
        if (multiple <= 0) return;
        var bullet = other.GetComponent<Bullet>();
        if (bullet.isCopy) return;
        for (var i = 0; i < multiple - 1; i++)
        {
            bullet.Clone();
        }
    }

    private void BulletEnter(Collider other)
    {
        if (multiple <= 0) other.GetComponent<Bullet>().Recycle();
    }
}
