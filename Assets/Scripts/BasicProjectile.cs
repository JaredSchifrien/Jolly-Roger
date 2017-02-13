﻿using UnityEngine;

/// <summary>
/// The ordnance shot by the tanks
/// </summary>
public class BasicProjectile : MonoBehaviour {
    /// <summary>
    /// Who shot us
    /// </summary>
    public GameObject Creator;
    /// <summary>
    /// How fast to move
    /// </summary>
    public float Speed = .01f;
    public GameObject bpCrosshair;

    private Vector3 InitialPos;
    private Vector3 Target;
    private Vector3 Direction;

    private float EndTime;


    internal void Die() {
        Destroy(gameObject);
    }
    /// <summary>
    /// Do dammage if hitting player
    /// </summary>
    /// <param name="collision"></param>
    internal void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<ShipController>() != null || Time.time > EndTime)
        {
            Destroy(bpCrosshair);
            Destroy(gameObject);
        }        
    }

    /// <summary>
    /// Start the projectile moving.
    /// </summary>
    /// <param name="creator">Who's shooting</param>
    /// <param name="pos">Where to place the projectile</param>
    /// <param name="direction">Direction to move in (unit vector)</param>
    public void Init(GameObject creator, Vector3 pos, Vector3 target)
    {
        Creator = creator;
        transform.position = pos;
        Target=target;
        Direction = target-pos;
        GetComponent<Rigidbody2D>().velocity = Direction.normalized * Speed;

/*
        crossHairGO = new GameObject("Projectile crossHair");
        SpriteRenderer renderer = crossHairGO.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Assets/Sprites/bpCrosshair.png");
        crossHairGO.transform.position=target;
*/

        var go = Instantiate(bpCrosshair) ;
        go.transform.position=target;
    }

    internal void Update(){

        if(Vector3.Distance(transform.position,InitialPos)<Vector3.Distance(Target,InitialPos)){

            Destroy(bpCrosshair);
            Destroy(gameObject);
        }
    }
}
