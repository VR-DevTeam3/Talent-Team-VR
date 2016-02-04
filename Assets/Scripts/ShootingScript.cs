using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingScript : MonoBehaviour {

    public float fireRate;
    public bool beam;
    public float fieldOfView;
    public GameObject projectile;
    public List<GameObject> projectileSpawn;
    public GameObject target;
    public int damage;

    List<GameObject> m_lastProjectile = new List<GameObject>();
    float m_fireTimer = 0.0f;
	
	// Update is called once per frame
	void Update () {

        if (beam && m_lastProjectile.Count <= 0)
        {

			float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position));

            if ( angle < fieldOfView) {

                spawnProjectile();

            } 
        }
        else if (beam && m_lastProjectile.Count > 0)
        {

			float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position));
            if (angle > fieldOfView)
            {
                while(m_lastProjectile.Count > 0)
                {
                    Destroy(m_lastProjectile[0]);
                    m_lastProjectile.RemoveAt(0);
                }

            }
        }
        else
        {

            m_fireTimer += Time.deltaTime;
            
			if(m_fireTimer >= fireRate)
            {
				float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position));

                if (angle < fieldOfView)
                {

                    spawnProjectile();
                    m_fireTimer = 0.0f;
                }
                
            }

        }
	}

    void spawnProjectile()
    {

        if (!projectile)
        {
            return;
        }

		m_lastProjectile.Clear ();

        for (int i = 0; i < projectileSpawn.Count; i++){

            if (projectileSpawn[i])
            {

				if((target.transform.position - projectileSpawn [i].transform.position).magnitude > 15){

					return;

				}
                GameObject proj = Instantiate(projectile, projectileSpawn[i].transform.position, Quaternion.Euler(projectileSpawn[i].transform.forward)) as GameObject;
                Destroy(proj, 15f);
                Rigidbody rb = proj.GetComponent<Rigidbody> ();
				//proj.GetComponent<BaseProjectile>().FireProjectile(projectileSpawn[i], target, damage);

				proj.transform.position = projectileSpawn [i].transform.position;

				rb.velocity = (target.transform.position - projectileSpawn [i].transform.position) * 3;

				m_lastProjectile.Add(proj);


			}


				

		}

      }

  }

