using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 20;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GameObject explosion;
    GameObject Tele;
    


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    SphereCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <SphereCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
        
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
     


        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
        Tele = GameObject.Find("TeleControl");
        Tele.GetComponent<TeleOnWin>().score();
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
     }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        
    }
}
