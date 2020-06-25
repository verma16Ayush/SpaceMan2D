using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    public float speed = 20f;
    public float rotSpeed = 200f;
    public GameObject exhaustParticlePrefab;
    public GameObject impactParticlePrefab;
    private Rigidbody2D m_Rb;
    public Transform exhaustPoint;
    public GameObject target;
    [HideInInspector]public GameObject exhaust; // to store the instance of exhaust effect for destroyinn purposes

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        m_Rb = GetComponent<Rigidbody2D>();
        exhaust = Instantiate(exhaustParticlePrefab, exhaustPoint.position, transform.rotation);
        exhaust.transform.parent = exhaustPoint;
        Invoke("Impact", 5f);
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            float rotateAmountSin = Vector3.Cross(direction, transform.up).z;
            float rotateAmount = Mathf.Asin(rotateAmountSin);
            m_Rb.angularVelocity = -1 * rotateAmount * rotSpeed;
            m_Rb.velocity = transform.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Damage(50);
        }

        else if(other.collider.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().Damage(50f);
        }
        
        Impact();
    }

    void Impact()
    {
        Destroy(gameObject);
        GameObject impactEffect = Instantiate(impactParticlePrefab, transform.position, transform.rotation);
        FindObjectOfType<Camera>().GetComponent<CameraShake>().Shake();
        Destroy(impactEffect, 5f);
        Destroy(exhaust, 5f);
    }
    
}
