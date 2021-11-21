using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlle : MonoBehaviour
{

    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;//���ǹ����� �ٲٱ� �������� �ð�
    float timer; // Ÿ�̸��� ���� ���� ����
    int direction = 1; //���� 1 or -1
    bool broken = true;

    Rigidbody2D rigid;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigid.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigid.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;

        //optional if you added the fixed animation

        animator.SetTrigger("Fixed");

        smokeEffect.Stop();
    }
}
