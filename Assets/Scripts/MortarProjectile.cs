﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The ordnance shot by the tanks
/// </summary>
public class MortarProjectile : MonoBehaviour {
    /// <summary>
    /// Who shot us
    /// </summary>
    public GameObject Creator;
	

	public float MortarSpeed = 3f;
    public float explosionLength = 1f;
    public GameObject explosionPrefab;

    private Vector3 InitialPos;
    private Vector3 Target;
    private Vector3 Direction;
	private float explosionTime;
    private float startTime = 0;
    private bool exploding = false;
    public List<Health> targetedHealth = new List<Health>();
    private Color color;

    /// <summary>
    /// Start the projectile moving.
    /// </summary>
    /// <param name="creator">Who's shooting</param>
    /// <param name="pos">Where to place the projectile</param>
    /// <param name="direction">Direction to move in (unit vector)</param>
    public void Init(GameObject creator, Vector3 pos, Vector3 target)
    {
        Creator = creator;
        transform.position = target;
        Target=target;
		explosionTime = Time.time + MortarSpeed;
        startTime = Time.time;
		// Destroy(GetComponent<CircleCollider2D>());
    }


    public void Start() {
        color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<PointEffector2D>().enabled = false;
    }

    internal void Update(){
        color.a =  (Time.time-startTime)/(MortarSpeed);
        GetComponent<SpriteRenderer>().color = color;
        //Debug.Log(GetComponent<SpriteRenderer>().color.a);
		if(Time.time > explosionTime && !exploding){
            GetComponent<PointEffector2D>().enabled = true;
            GameObject e = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            e.transform.localScale = e.transform.localScale * 1.25f; 
            
            exploding = true;
            foreach (Health health in targetedHealth) {
                if (health != null)
                    health.Damage(10f);
            }
        } else if(Time.time > (explosionTime + explosionLength)){
            Destroy(gameObject);
		}
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null) {
            if (!targetedHealth.Contains(other.GetComponent<Health>()))
            {
                targetedHealth.Add(other.GetComponent<Health>());
            }
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Health>() != null) {
            if (!targetedHealth.Contains(other.GetComponent<Health>()))
            {
                targetedHealth.Add(other.GetComponent<Health>());
            }
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null) {
            if (targetedHealth.Contains(other.GetComponent<Health>()))
            {
                targetedHealth.Remove(other.GetComponent<Health>());
            }
        }
    }
}
