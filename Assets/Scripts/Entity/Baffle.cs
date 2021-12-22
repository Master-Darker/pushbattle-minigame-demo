using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Baffle : MonoBehaviour
{
    public event UnityAction<string> onTrigger;
    private Rigidbody rb;
    private Vector3 position;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            switch (other.name)
            {
                case "BlueBorder":
                    onTrigger?.Invoke("Defeat");
                    break;
                case "RedBorder":
                    onTrigger?.Invoke("Victory");
                    break;
                default:
                    break;
            }
        }
    }

    public void Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = position;
    }
}
