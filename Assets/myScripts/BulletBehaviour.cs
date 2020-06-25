using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float bulletVelocity = 50f;
    private Transform firePoint;
    public GameObject impact;
    public float damage = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        Vector2 fireDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        bullet.velocity = fireDirection * 100f;
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject impactEffect = Instantiate(impact, transform.position, Quaternion.identity);
        Destroy(impactEffect, 0.12f);
        Destroy(gameObject);
    }
}
