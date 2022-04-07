using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;

    public bool bouncing  = false;
    public bool explosive = true;
    public float explosionRadius = 10f;                      

    float timer;                                    
    Ray shootRay = new Ray();                                   
    RaycastHit shootHit;    
    RaycastHit shootHitOpaqueObj;                         
    int shootableMask;                             
    int opaqueMask;
    ParticleSystem gunParticles;                    
    LineRenderer gunLine;                           
    AudioSource gunAudio;                           
    ParticleSystem explosionParticle;
    Light gunLight;                                 
    float effectsDisplayTime = 0.2f;                

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        opaqueMask = LayerMask.GetMask("Opaque");
        gunParticles = GetComponent<ParticleSystem>();
        explosionParticle = GameObject.FindGameObjectWithTag("ExplosionAnim").GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        explosionParticle.Stop();
    }

    void Shoot()
    {
        // Normal shoot
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        
        gunLine.SetPosition(0, transform.position);

        shootRay.origin    = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else if (Physics.Raycast(shootRay, out shootHitOpaqueObj, range, opaqueMask))
            gunLine.SetPosition(1, shootHitOpaqueObj.point);
        else
            gunLine.SetPosition(1, transform.position + transform.forward * range);
        

        // Explosive
        if (explosive) {
            explosionParticle.startSize = explosionRadius;
            bool directHit = Physics.Raycast(shootRay, out shootHit, range, shootableMask);
            bool explosion = Physics.Raycast(shootRay, out shootHitOpaqueObj, range, opaqueMask);

            Collider[] enemyWithinRadius = Physics.OverlapSphere(shootHit.point, explosionRadius, shootableMask);
            foreach (var enemy in enemyWithinRadius) {
                if (enemy.GetType() == typeof(CapsuleCollider)) {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    enemyHealth.TakeDamage(damagePerShot, transform.position);
                }
            }

            enemyWithinRadius = Physics.OverlapSphere(shootHitOpaqueObj.point, explosionRadius, shootableMask);
            foreach (var enemy in enemyWithinRadius) {
                if (enemy.GetType() == typeof(CapsuleCollider)) {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    enemyHealth.TakeDamage(damagePerShot, transform.position);
                }
            }
        
            if (directHit)
                explosionParticle.transform.position = shootHit.point;
            else if (explosion)
                explosionParticle.transform.position = shootHitOpaqueObj.point;

            if (directHit || explosion)
                explosionParticle.Play();
        }
    }
}