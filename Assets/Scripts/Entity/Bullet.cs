using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public float maxForce;
    [HideInInspector] public float force;
    public bool isCopy;
    public Gun gun;
    public float offsetX;
    public int index;

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        switch (tag)
        {
            case "Baffle":
                BaffleEnter(other);
                break;
            case "Wall":
                WallEnter();
                break;
            default:
                break;
        }
    }

    private void WallEnter()
    {
        var normal = new Vector3(0, transform.position.y, transform.position.z) - transform.position;
        var outDir = Vector3.Reflect(transform.forward, normal.normalized);
        transform.forward = outDir;
    }

    private void BaffleEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        Recycle();
    }

    public void Clone()
    {
        if (index % 2 == 0) offsetX += 0.4f;
        var x = offsetX;
        x *= Mathf.Pow(-1, index++);
        gun.CloneBullet(force / (index + 1), transform.position + Vector3.right * x, transform.rotation);
    }

    public void Recycle()
    {
        gun.RecycleBullet(this);
    }
}
