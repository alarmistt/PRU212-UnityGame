using System.Collections;
using UnityEngine;

public class HealthDuc : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100;
    public float currentHealth;

    public HealthbarDuc healthbar;
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealthSpecial(currentHealth, maxHealth);

        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (healthbar != null)
        {
            healthbar.SetHealthSpecial(currentHealth, maxHealth);
        }

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManagerDuc.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManagerDuc.instance.PlaySound(deathSound);
            }
        }
    }



    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);

        healthbar.SetHealth(currentHealth);
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }

    //Respawn
    public void Respawn()
    {
        dead = false;
        AddHealth(maxHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    //[SerializeField] private float startingHealth;
    //public float currentHealth { get; private set; }


    //private void Awake()
    //{
    //    currentHealth = startingHealth;
    //}

    //public void TakeDamage(float _damage)
    //{
    //    currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

    //    if (currentHealth > 0)
    //    {
    //        //player hurt
    //    }
    //    else
    //    {
    //        //player dead
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        TakeDamage(1);
    //    }
    //}
}
