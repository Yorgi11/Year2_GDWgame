using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage;
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GameObject BloodEffect;
    [SerializeField] private GameObject BulletHole;

    private Vector3 dir;

    private Vector3 lastPos;
    private RaycastHit ray;
    void Awake()
    {
        Destroy(gameObject, 3f);
        lastPos = transform.position;
    }
    void Update()
    {
        dir = transform.position - lastPos;
        if (Physics.Raycast(lastPos, dir.normalized, out ray, dir.magnitude))
        {
            if (ray.collider.gameObject.GetComponentInParent<Enemy>() != null)
            {
                ray.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(bulletDamage);
                //Instantiate(BloodEffect, ray.point, Quaternion.LookRotation(ray.normal));
                SpawnHitmarker();
            }
            if (ray.collider.gameObject.GetComponentInParent<StatsSystem>() != null) ray.collider.gameObject.GetComponentInParent<StatsSystem>().TakeDamage(bulletDamage);
            //if (ray.transform.CompareTag("LevelPart")) Instantiate(BulletHole, ray.point + ray.normal * 0.025f, Quaternion.LookRotation(ray.normal));
            Destroy(gameObject, 0.125f);
        }
        lastPos = transform.position;
    }

    private void SpawnHitmarker()
    {
        Transform temp = GameObject.FindGameObjectWithTag("HitMarker").transform;
        GameObject marker = GameObject.Instantiate(hitMarker, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, temp);
        Destroy(marker, 0.125f);
    }
}
