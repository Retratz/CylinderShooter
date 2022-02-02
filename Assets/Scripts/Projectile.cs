using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    public Color trailColor;
    private float speed = 10;
    private float damage = 1;

    private float lifetime = 3;
   
    /*for collision errors when enemy moving fast.
    Increase the value if the detection is not working while enemies are attacking*/
    private float skinWidth = .1f;
    
    private void Start()
    {
        //destroy bullets from hiearchy after lifetime expires
        Destroy(gameObject,lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 1f, collisionMask);
        //bullets spawn inside enemy now registers as a hit 
        if (initialCollisions.Length > 0)
        {
             OnHitOnject(initialCollisions[0],transform.position);
        }
        
        GetComponent<TrailRenderer>().material.SetColor("_tintColor",trailColor);
    }
    
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitOnject(hit.collider, hit.point);
        }
    }

    void OnHitOnject(Collider c, Vector3 hitPoint)
    {
        IDamagable damagableleObject = c.GetComponent<IDamagable>();
        if (damagableleObject != null)
        {
            damagableleObject.TakeHit(damage,hitPoint,transform.forward);
        }
        GameObject.Destroy(gameObject);
    }
    
}
