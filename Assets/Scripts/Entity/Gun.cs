using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    public bool isRun;
    public Transform bullets;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float startTime;
    public float repeatTime;
    public int poolSize;
    public int maxPoolSize;
    private ObjectPool<Bullet> pool;
    private List<Bullet> actives;
    private Quaternion originRotation;

    private void Awake()
    {
        actives = new List<Bullet>();
        pool = new ObjectPool<Bullet>(
            OnCreatePoolItem,
            OnGetPoolItem,
            OnReleaseItem,
            OnDestroyItem,
            true,
            poolSize,
            maxPoolSize);
        
    }

    protected virtual void Start()
    {
        originRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isRun) return;
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        
    }

    public void OnStart()
    {
        transform.rotation = originRotation;
        InvokeRepeating("Fire", startTime, repeatTime);
        isRun = true;
    }

    public void OnEnd()
    {
        CancelInvoke("Fire");
        isRun = false;
        for (var i = actives.Count - 1; i >= 0; i--)
        {
            actives[i].Recycle();
        }
    }

    protected void Fire()
    {
        ReloadBullet();
    }

    private void OnDestroyItem(Bullet obj)
    {
        Destroy(obj.gameObject);
    }

    private void OnReleaseItem(Bullet obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        actives.Remove(obj);
    }

    private void OnGetPoolItem(Bullet obj)
    {
        obj.force = obj.maxForce;
        actives.Add(obj);
    }
    private Bullet OnCreatePoolItem()
    {
        var go = Instantiate(bulletPrefab, bullets, true);
        go.SetActive(false);
        var bullet = go.GetComponent<Bullet>();
        bullet.gun = this;
        return bullet;
    }

    public void ReloadBullet()
    {
        var obj = pool.Get();
        obj.transform.position = firePoint.position;
        obj.transform.rotation = firePoint.rotation;
        obj.isCopy = false;
        obj.offsetX = 0;
        obj.index = 0;
        obj.gameObject.SetActive(true);
    }

    public void RecycleBullet(Bullet bullet)
    {
        pool.Release(bullet);
    }

    public void CloneBullet(float force, Vector3 position, Quaternion rotation)
    {
        var obj = pool.Get();
        obj.force = force;
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.isCopy = true;
        obj.gameObject.SetActive(true);
    }
}
