using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatabinding : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public Animator GetAnimator
    {
        get
        {
            return animator;
        }
    }

    public Vector3 MoveDir
    {
        set
        {
            animator.SetFloat(Anim_key_X, value.x);
            animator.SetFloat(Anim_key_Y, value.z);
        }
    }

    public bool Empty
    {
        set
        {
            if (value)
                animator.Play("Empty", 1, 0);
        }
    }

    public bool Fire
    {
        set
        {
            if (value)
                animator.Play("Fire", 1, 0);
        }
    }

    public bool Reload
    {
        set
        {
            if (value)
                animator.Play("Reload", 1, 0);
        }
    }

    public bool Draw
    {
        set
        {
            if (value)
                animator.Play("Draw", 1, 0);
        }
    }
    private int Anim_key_X;
    private int Anim_key_Y;
    // Start is called before the first frame update
    void Start()
    {
        Anim_key_X = Animator.StringToHash("X");
        Anim_key_Y = Animator.StringToHash("Y");
    }

    public void ChangeAnimatorController(AnimatorOverrideController ao)
    {
        animator.runtimeAnimatorController = ao;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
