using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem SmokeEfect;

    float timer;
    Rigidbody2D rigidbody2D;
    int direction = 1;
    Animator animator;
    bool broken = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!broken)
        {
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
        
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
 
        rigidbody2D.MovePosition(position);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyControler player = other.gameObject.GetComponent<RubyControler >();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    public void Fix()
    {
        SmokeEfect.Stop();
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        SmokeEfect.Stop();
    }
}