using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float movX;

    Vector2 mov;
    private int speed = 5;
    [SerializeField]
    private bool grounded = false;
    private Rigidbody2D rb;
    public LayerMask mask;
    public bool jCooldown = true;
    [SerializeField]
    private GameObject bullet;
    private Transform bulletpos;
    private bool canShoot = true;
    private SpriteRenderer render;
    private bool flip = false;
    private bool cantakedamage = true;
    Animator anim;
    int health;
    bool death = false; 
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 6, true);
        QualitySettings.vSyncCount = 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
        bulletpos = this.gameObject.transform.GetChild(1).transform;
        render = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = this.gameObject.GetComponent<Animator>();
        health = 2;
        Debug.Log("All Working");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Revive();
        }
        if (death != true)
        {

            Movement();
            Jump();
            Attack();
        }
    }
    void Movement()
    {
        movX = Input.GetAxisRaw("Horizontal");
        mov = new Vector2(movX, 0) * (speed + 3) * Time.deltaTime;
        transform.Translate(mov);
        if (movX < 0)
        {
            render.flipX = true;
            flip = true;

        }
        else if (movX > 0)
        {
            render.flipX = false;
            flip = false;

        }
    }
    void Jump()
    {
        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            //Debug.Log("Jump");
            grounded = false;
            rb.AddForce(Vector2.up * 11.5f, ForceMode2D.Impulse);
            jCooldown = false;
            StartCoroutine(JumpCooldownCoRoutine());

        }
    }

    void CheckGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position, Vector2.down, 1.7f, mask);
        Debug.DrawRay(this.transform.position, Vector2.down * 1.7f, Color.red);
        if (ray.collider != null && jCooldown == true)
        {

            grounded = true;
            //Debug.Log(ray.collider.gameObject.name);


        }
        else
        {
            //Debug.Log("Not Touching");
            if (grounded == true)
            {
                StartCoroutine(CoyoteTimeCoRoutine(0.15f));

            }


        }

    }
    void Attack()
    {
        if (Input.GetMouseButton(0) && canShoot == true)
        {
            //Debug.Log("Shoot");
            canShoot = false;
            GameObject bulletobj;
            StartCoroutine(ShootCoolDownCoroutine());
            if (flip == true)
            {
                bulletobj = Instantiate(bullet, new Vector2(this.transform.position.x, bulletpos.transform.position.y), Quaternion.identity);
                bulletobj.GetComponent<Bullet>().flipX = true;
                Destroy(bulletobj, 5);
            }
            else if (flip == false)
            {

                bulletobj = Instantiate(bullet, new Vector2(this.transform.position.x, bulletpos.transform.position.y), Quaternion.identity);
                bulletobj.GetComponent<Bullet>().flipX = false;
                Destroy(bulletobj, 5);

            }

        }
    }
    void Kill()
    {
        death = true; 
        anim.SetTrigger("Death");
        Physics2D.IgnoreLayerCollision(8, 6, true);
        //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //this.gameObject.GetComponent<Rigidbody2D>().simulated = false;

    }
    void Revive()
    {
        death = false;
        health = 2;
        Physics2D.IgnoreLayerCollision(8, 6, false) ;
        //this.gameObject.GetComponent<BoxCollider2D>().enabled = true; 
        //this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void Damage()
    {
        if (cantakedamage == true)
        {
            cantakedamage = false;
            health--;
            if (health <= 0)
            {
                Kill();

            }
            anim.SetTrigger("Hit");
            StartCoroutine(DamageCoolDownCoRoutine());
        }

    }
    public bool IsDeath()
    {
        return death;
    }

    #region Coroutines

    IEnumerator JumpCooldownCoRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        jCooldown = true;

    }
    IEnumerator CoyoteTimeCoRoutine(float cooldown)
    {
        Debug.Log("Entered Coroutine");
        yield return new WaitForSeconds(cooldown);
        grounded = false;

    }
    IEnumerator ShootCoolDownCoroutine()
    {
        yield return new WaitForSeconds(1);
        canShoot = true;
    }
    IEnumerator DamageCoolDownCoRoutine()
    {
        yield return new WaitForSeconds(2);
        cantakedamage = true;
    }
    #endregion
}
