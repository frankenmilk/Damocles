using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    private float Yvelocity;

    // For movement
    private float horizontal;
    [SerializeField] private float speed = 16f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    // For Wall Slide & Wall Jump
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    public GameObject Heart_1;
    public GameObject Heart;
    public GameObject Heart_2;
    public GameObject Heart_3;

    public GameObject TheMocle;
    public GameObject HammerSpace;
    public GameObject MetalPipe;
    public GameObject GlassSword;
    public GameObject HadesBident;
    public GameObject SwordOfJustice;
    public GameObject RiddlerCane;

    public GameObject Key;
    public GameObject Key2;
    public GameObject Key3;
    public GameObject Player;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject Gate;

    // For collision
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    // Player Damage Stuff
    private bool canMove = true;
    private float timeTime;

    int damage25 = 25;
    int damage50 = 50;
    //int damage50 = 50;

    public int die = 100;
    public bool isGrounded;

    [SerializeField] float knockBackForce;
    [SerializeField] float knockBackForceUp;


    // Start is called before the first frame update
    void Update()
    {

        if (Time.time >= timeTime + 1f)
        {
            canMove = true;
        }


        if (animator.GetBool("IsWallSliding") == true)
        {
            animator.SetBool("IsJumping", false);
        }

        if (rb.velocity.y < .01 && !IsWalled())
        {
            animator.SetBool("IsJumping", false);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        animator.SetFloat("Yvelocity", Mathf.Abs(rb.velocity.y));

        horizontal = Input.GetAxisRaw("Horizontal");

        if (canMove == true)
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                animator.SetBool("IsJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            KeyGrabber();
        }
        
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
        //Weapon Displayment
        if (PlayerStats.currentWeapon == "TheMocle")
        {
            GameObject.Destroy(HadesBident);
            GameObject.Destroy(HammerSpace);
            GameObject.Destroy(RiddlerCane);
            GameObject.Destroy(GlassSword);
            GameObject.Destroy(SwordOfJustice);
            GameObject.Destroy(MetalPipe);
        }
        if (PlayerStats.currentWeapon == "HadesBident")
        {
            GameObject.Destroy(TheMocle);
            GameObject.Destroy(HammerSpace);
            GameObject.Destroy(RiddlerCane);
            GameObject.Destroy(GlassSword);
            GameObject.Destroy(SwordOfJustice);
            GameObject.Destroy(MetalPipe);
        }
        if (PlayerStats.currentWeapon == " HammerSpace")
        {
            GameObject.Destroy(HadesBident);
            GameObject.Destroy(TheMocle);
            GameObject.Destroy(RiddlerCane);
            GameObject.Destroy(GlassSword);
            GameObject.Destroy(SwordOfJustice);
            GameObject.Destroy(MetalPipe);
        }
        if (PlayerStats.currentWeapon == "RiddlerCane")
        {
            GameObject.Destroy(HadesBident);
            GameObject.Destroy(HammerSpace);
            GameObject.Destroy(TheMocle);
            GameObject.Destroy(GlassSword);
            GameObject.Destroy(SwordOfJustice);
            GameObject.Destroy(MetalPipe);
        }
        if (PlayerStats.currentWeapon == "GlassSword")
        {
            GameObject.Destroy(HadesBident);
            GameObject.Destroy(HammerSpace);
            GameObject.Destroy(RiddlerCane);
            GameObject.Destroy(TheMocle);
            GameObject.Destroy(SwordOfJustice);
            GameObject.Destroy(MetalPipe);
        }
        if (PlayerStats.currentWeapon == "SwordOfJustic")
        {
            GameObject.Destroy(HadesBident);
            GameObject.Destroy(HammerSpace);
            GameObject.Destroy(RiddlerCane);
            GameObject.Destroy(GlassSword);
            GameObject.Destroy(TheMocle);
            GameObject.Destroy(MetalPipe);
        }
        if (PlayerStats.currentWeapon == "MetalPipe")
        {
            GameObject.Destroy(HadesBident);
            GameObject.Destroy(HammerSpace);
            GameObject.Destroy(RiddlerCane);
            GameObject.Destroy(GlassSword);
            GameObject.Destroy(SwordOfJustice);
            GameObject.Destroy(TheMocle);
        }
    }

    void FixedUpdate()
    {
        if (!isWallJumping && canMove == true)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        
    }

    private float Timer()
    {
        return Time.deltaTime;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            animator.SetBool("IsWallSliding", true);
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            animator.SetBool("IsWallSliding", false);
            isWallSliding = false;
        }
    }

    public void KeyGrabber()
    {
        if (30 < Player.transform.position.y && 97 < Player.transform.position.x && 101 > Player.transform.position.x && 33 > Player.transform.position.y)
        {
            GameObject.Destroy(Key);
            GameObject.Destroy(lock1);
        }
        if (-6 < Player.transform.position.y && 126 < Player.transform.position.x && 130 > Player.transform.position.x && -4 > Player.transform.position.y)
        {
            GameObject.Destroy(Key2);
            GameObject.Destroy(lock2);
        }
        if (-50 < Player.transform.position.y && 130 < Player.transform.position.x && 134 > Player.transform.position.x && -48 > Player.transform.position.y)
        {
            GameObject.Destroy(Key3);
            GameObject.Destroy(lock3);
        }
        if (lock1 == null && lock2 == null && lock3 == null)
        {
            GameObject.Destroy(Gate);
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x / 2, wallJumpingPower.y / 2);
            wallJumpingCounter = 0f;
            
            if(transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0, 180, 0);
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    // Player Damage area
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "enemy25" && PlayerStats.AegisActive == false)
        {
            TakeDamage(damage25);
        }
        else if (col.gameObject.tag == "enemy50" && PlayerStats.AegisActive == false)
        {
            TakeDamage(damage50);
        }

        if (col.gameObject.tag == "Lava")
        {
            Die();
        }
    }

    public void Knockback(string enemy)
    {
        Transform attacker = GetClosestDamageSource(enemy);
        Vector2 knockbackDirection = new Vector2(transform.position.x - attacker.transform.position.x, 0);
        rb.velocity = new Vector2(knockbackDirection.x, knockBackForceUp) * knockBackForce;
    }

    public Transform GetClosestDamageSource(string whichEnemy)
    {

        GameObject[] DamageSources = null; 

        if (whichEnemy == "25")
        {
            DamageSources = GameObject.FindGameObjectsWithTag("enemy25");
        }
        else if (whichEnemy == "50")
        {
            DamageSources = GameObject.FindGameObjectsWithTag("enemy50");
        }    
                
        
        float closestDistance = Mathf.Infinity;
        Transform currentClosestDamageSource = null;

        foreach (GameObject go in DamageSources)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                currentClosestDamageSource = go.transform;

            }

        }

        return currentClosestDamageSource;
    }

    public void TakeDamage(int damage)
    {
        canMove = false;
        timeTime = Time.time;
        if (damage == 25)
        {
            string enemy = "25";
            Knockback(enemy);
        } 
        else if (damage == 50)
        {
            string enemy = "50";
            Knockback(enemy);
        }
        

        PlayerStats.playerHealth -= damage;

        Debug.Log("Player Current Health is: " + PlayerStats.playerHealth);

        if (PlayerStats.playerHealth == 75)
        {
            GameObject.Destroy(Heart_3);
        }
        if (PlayerStats.playerHealth == 50)
        {
            GameObject.Destroy(Heart_2);
        }
        if (PlayerStats.playerHealth == 25)
        {
            GameObject.Destroy(Heart_1);
        }

        // Plays death Animation if current health reaches or goes below 0
        if (PlayerStats.playerHealth <= 0)
        {
            GameObject.Destroy(Heart);
            Die();
        }

    }

    void Die()
    {
        // Disables the Player

        GetComponent<Collider2D>().enabled = false;


    }
    
}
