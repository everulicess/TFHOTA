using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private Animator animator;
    private int isTalkingHash;
    private int isHappyHash;

    private int curState;


    void Start()
    {
        animator = GetComponent<Animator>();
        isTalkingHash = Animator.StringToHash("isTalking");
        isHappyHash = Animator.StringToHash("isHappy");
    }

    public void ChangeAnimation(string id)
    {
        switch (id)
        {
            case "Talk":
                animator.SetBool(isTalkingHash, true);
                return;

            case "Happy":
                animator.SetBool(isHappyHash, true);
                return;

            case "Idle":
                animator.SetBool(isTalkingHash, false);
                animator.SetBool(isHappyHash, false);
                return;
        }
    }
}
