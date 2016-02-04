using UnityEngine;
using System.Collections;

public class LowPolyLaserGun : MonoBehaviour
{
    public float timeBetweenBullets = 0.15f;
    GameObject beamHitParticles;
    ParticleSystem bhp;
    public float beamRotationSpeed = 400.0f;
    public float beamExtendSpeed = 10.0f;
    public float zScaleFactor = 20.0f;
    public float distanceToHitPoint;
    public float distanceToHitPoint2;
    public bool active = false;
    public bool magnetDetectionEnabled = true;
    float timer;
    public int damagePerShot = 20;
    int mirrorMask;


    void Awake()
    {
        CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
        // Make sure the particles system is the first child of THIS extendy beam cube.
        // And that the particle system does NOT Play on Awake!
        beamHitParticles = transform.parent.GetChild(1).gameObject; // So it doesn't scale the particles, made it a sibling.
        bhp = beamHitParticles.GetComponent<ParticleSystem>();
        mirrorMask = LayerMask.GetMask("Mirror");
    }

    void Update()
    {
        timer += Time.deltaTime;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (CardboardMagnetSensor.CheckIfWasClicked())
        {
            if (active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
        }

        if (Physics.Raycast(ray, out hit) && (active))
        {
            distanceToHitPoint = Vector3.Distance(transform.position, hit.point);

            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, (distanceToHitPoint * zScaleFactor)), (beamExtendSpeed * Time.deltaTime)); // Because distance-units != scale-units.

            beamHitParticles.transform.position = hit.point;
            //beamHitParticles.transform.position = Vector3.Lerp(beamHitParticles.transform.position, hit.point, 20*Time.deltaTime); 
            // ^ Ends up being weird because it has to travel to the hit point everytime since in the else I let it stay where it was.
            // If I could keep it on the end of the beam cube, extended or unextended that would work.
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                if (timer >= timeBetweenBullets)
                {
                    enemyHealth.TakeDamage(damagePerShot, hit.point);
                    timer = 0;
                }
                
            }
            //if (hit.collider.tag == "Mirrors")
            
        }
        
       /* if(Physics.Raycast(ray,out hit, mirrorMask))
        {
            
            distanceToHitPoint = Vector3.Distance(transform.position, hit.point);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, (distanceToHitPoint * zScaleFactor)), (beamExtendSpeed * Time.deltaTime)); // Because distance-units != scale-units.
            beamHitParticles.transform.position = hit.point;
            Vector3 reflector = Vector3.Reflect(ray.direction, hit.normal);


        }*/
        else
        {
            bhp.Stop(); // Oops we hit nothing, scale the laser back and stop the particles!
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, transform.localScale.z), (beamExtendSpeed * Time.deltaTime));
            // This is screwy but its a nice effect. It messed with the collider though I think... Seems to work, will keep it.
            //transform.localScale = Vector3.one; // Simply just makes it disappear essentially.
        }

        transform.Rotate(0, 0, (Time.deltaTime * beamRotationSpeed));
    }

    // Because the Z scale stays the same and has to travel back when the ray hits and this messed up the TriggerEnter.
    /*
    void ShrinkLaserXYbeforeZ()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, transform.localScale.z), (beamExtendSpeed * Time.deltaTime));

        if (transform.localScale.x == 0)
            transform.localScale = Vector3.zero;
    }
    */

    // With mouse clicking the laser, this is not reliable for making the particles appear... weird, seems fine on the normal lasers though.
    void OnTriggerEnter(Collider other)
    {
        bhp.Play();
    }

}
