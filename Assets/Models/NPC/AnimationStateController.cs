using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControllerJurgen : MonoBehaviour
{
    Animator animator;
    int isTalkingHash;
    int isHappyHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isTalkingHash = Animator.StringToHash("isTalking");
        isHappyHash = Animator.StringToHash("isHappy");
    }

    // Update is called once per frame
    void Update()
    {
        bool isHappy = animator.GetBool(isHappyHash);
        bool isTalking = animator.GetBool(isTalkingHash);
        bool triggerTalking = Input.GetKey("e");
        bool triggerHappy = Input.GetKey("left shift");

        if (!isTalking && triggerTalking)
        {
            animator.SetBool(isTalkingHash, true);
        }
        if (isTalking && !triggerTalking)
        {
            animator.SetBool(isTalkingHash, false);
        }
        if (!isHappy && (isTalking && triggerTalking))
        {
            animator.SetBool(isHappyHash, true);
        }
        if (isHappy && (!isTalking || !triggerTalking))
        {
            animator.SetBool(isHappyHash, false);
        }
    }
}
