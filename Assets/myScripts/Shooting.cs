using UnityEngine;
public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 10f;
    public float damage = 10f;
    private bool hasFired = false;
    public GameObject muzzleFlashPrefab;
    private void Update()
    {
        if (Input.GetMouseButton(0) && !hasFired )
        {
            hasFired = true;
            shoot();
        }

        if (!Input.GetMouseButton(0))
        {
            hasFired = false;
        }
    }

    void shoot()
    {
        GameObject muzzleClone = Instantiate(muzzleFlashPrefab, transform.position, transform.rotation) ;
        muzzleClone.transform.parent = firePoint;
        float size = Random.Range(0.6f, 0.8f);
        muzzleClone.transform.localScale = new Vector3(size, size, 0);
        Destroy(muzzleClone, 0.03f);
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        // yield return new WaitForSeconds(wait);
    }
}
