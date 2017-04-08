using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public Animator animator;
    public float framesPerSecond;

    private float animframes;

    public float runvel;
    public float stepvel;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private SpriteRenderer spriterend;
    private float basegravity;

    private float deadtime = 0;

	// Use this for initialization
	void Start ()
    {
        animframes = framesPerSecond / 60.0f;
        animator.SetFloat("frame", animframes);
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        spriterend = GetComponent<SpriteRenderer>();
        basegravity = rb2d.gravityScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //on press activate var
        //if on release var is active, tap occured
        //if var active after period hold occured

        if (animator.GetBool("dead"))
            deadtime += Time.deltaTime;
        else
            deadtime = 0;

        if (deadtime > 5.0f)
        {
            transform.position = new Vector3(-2.5f, 0, 0);
            animator.SetBool("dead", false);
        }

        animator.SetBool("flipX", spriterend.flipX);

        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetBool("dead", true);

        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);

        animator.SetBool("shift", Input.GetKey(KeyCode.LeftShift));
        
        animator.SetBool("kpR", Input.GetKey(KeyCode.RightArrow));
        animator.SetBool("kpL", Input.GetKey(KeyCode.LeftArrow));

        bool falling = !animator.GetBool("!falling");

        animator.SetBool("kpU", falling ? (asi.IsName("JumpUpRight") || asi.IsName("JumpUpLeft") ? Input.GetKey(KeyCode.UpArrow) : false) : Input.GetKey(KeyCode.UpArrow));
        animator.SetBool("kpD", Input.GetKey(KeyCode.DownArrow));
        
        Vector3 dir;
        if (asi.IsTag("right"))
            dir = Vector3.right;
        else if (asi.IsTag("left"))
            dir = Vector3.left;
        else
            dir = Vector3.zero;

        if (asi.IsName("JumpUpRight") || asi.IsName("JumpUpLeft"))
        {
            rb2d.gravityScale = 0;
            transform.position += Vector3.up * stepvel * 1.5f * Time.deltaTime;
        }
        else if (asi.IsName("LedgeClimbRight") || asi.IsName("LedgeClimbLeft"))
        {
            bc2d.isTrigger = true;
            rb2d.gravityScale = 0;
            transform.position += Vector3.up * stepvel * 1.75f * Time.deltaTime;
        }
        else if (asi.IsName("IdleRightJump") || asi.IsName("RunRightJump") || asi.IsName("IdleLeftJump") || asi.IsName("RunLeftJump"))
        {
            rb2d.gravityScale = 0;
            transform.position += Vector3.up * stepvel * Time.deltaTime;
            //transform.position += dir * stepvel / 2 * Time.deltaTime;
        }
        else
        {
            rb2d.gravityScale = basegravity;
            bc2d.isTrigger = false;
        }

        if (asi.IsName("StepLeft"))
        {
            if (animator.GetBool("lwalkable"))
                transform.position += dir * stepvel * Time.deltaTime;
        }
        else if (asi.IsName("StepRight"))
        {
            if (animator.GetBool("rwalkable"))
                transform.position += dir * stepvel * Time.deltaTime;
        }
        else
            transform.position += dir * runvel * Time.deltaTime;
    }
}
