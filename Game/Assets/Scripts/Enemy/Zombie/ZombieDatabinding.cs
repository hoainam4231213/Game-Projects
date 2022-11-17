using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDatabinding : MonoBehaviour
{
    public Animator animator;


    public bool Attack
    {
        set
        {
            if (value)
                animator.SetTrigger(Anim_key_attack);
        }
    }

    public int AttackIndex
    {
        set
        {
            animator.SetInteger(Anim_key_attackIndex, value);
        }
    }

    public float Speed
    {
        set
        {
            animator.SetFloat(Anim_key_speed, value);
        }
    }

    public bool Dead
    {
        set
        {
            if (value)
                animator.SetTrigger(Anim_key_dead);
        }
    }

    public int DeadIndex
    {
        set
        {
            animator.SetInteger(Anim_key_deadIndex, value);
        }
    }

    public bool Emerge
    {
        set
        {
            animator.Play("Emerge", 0, 0);
        }
    }

    private int Anim_key_speed;
    private int Anim_key_attack;
    private int Anim_key_dead;
    private int Anim_key_attackIndex;
    private int Anim_key_deadIndex;
    private void Start()
    {
        Anim_key_attack = Animator.StringToHash("Attack");
        Anim_key_dead = Animator.StringToHash("Dead");
        Anim_key_speed = Animator.StringToHash("Speed");
        Anim_key_attackIndex = Animator.StringToHash("AttackIndex");
        Anim_key_deadIndex = Animator.StringToHash("DeadIndex");
    }
}
