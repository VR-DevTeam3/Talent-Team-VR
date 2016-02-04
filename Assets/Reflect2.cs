
using UnityEngine;

public class Reflect2 : MonoBehaviour
{
GameObject beamHitParticles;
ParticleSystem bhp;
public int dist = 100; //max distance for beam to travel.
LineRenderer lr;
public string reftag = "reflect"; //tag it can reflect off.
int limit = 100; // max reflections
int verti = 1; //segment handler don't touch.
private bool iactive;
private Vector3 currot;
private Vector3 curpos;
public bool active = false;
public bool magnetDetectionEnabled = true;

float timer;
public int damagePerShot = 20;
public float timeBetweenBullets = 0.15f;
AudioSource gunAudio;
    public AudioClip hum;
    public AudioClip fire;
    void Start() {
        lr = GetComponent<LineRenderer>();
       // beamHitParticles = transform.parent.GetChild(1).gameObject; // So it doesn't scale the particles, made it a sibling.
       // bhp = beamHitParticles.GetComponent<ParticleSystem>();
        CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
        gunAudio = GetComponent<AudioSource>();

    }

    void Update() {
        timer += Time.deltaTime;
        if (CardboardMagnetSensor.CheckIfWasClicked())
        {
            if (active)
            {
                active = false;
                
            }
            else
            {
               
                active = true;
                //gunAudio.Play();
            }
        }

        lr.enabled = Input.GetButton("Fire1");
        if (Input.GetButtonDown("Fire1"))
        {
            gunAudio.clip = fire;
            gunAudio.Play();
            
        }
        if (Input.GetButton("Fire1") || Input.GetButtonUp("Fire1"))
        {
            
            DrawLaser();
        }
    }
    void DrawLaser() { 
        verti = 1;
        iactive = true;
        currot = transform.forward;
        curpos = transform.position;
        lr.SetVertexCount(1);
        lr.SetPosition(0, transform.position);

        while (iactive)
        {
            verti++;
            RaycastHit hit;
            lr.SetVertexCount(verti);
            if (Physics.Raycast(curpos, currot, out hit, dist))
            {
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
              
                //verti++;
                curpos = hit.point;
                currot = Vector3.Reflect(currot, hit.normal);
                lr.SetPosition(verti - 1, hit.point);
                if (hit.transform.gameObject.tag != reftag) {
                    iactive = false;
                }
            }
            else
            {
                //verti++;
                iactive = false;
                lr.SetPosition(verti - 1, curpos + 100 * currot);

            }
            if (verti > limit)
            {
                iactive = false;
            }


        }



    }
}
