using UnityEngine;

public class GunControl : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    PlayerController player;

    private void Start()
    {
        player = transform.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.Hit();
                player.shield = true;
            }
        }
    }
}
