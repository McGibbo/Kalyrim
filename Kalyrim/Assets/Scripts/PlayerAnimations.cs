using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

    Animator playerAnimator;
    bool animateBool;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void AnimatePlayerFlip(string animation)
    {
        playerAnimator.SetBool(animation, !playerAnimator.GetBool(animation));
        
    }

    public void GetAnimationBool(string animation)
    {
        animateBool = playerAnimator.GetBool(animation);
    }
        
    public void SetAnimationBool(string animation)
    {
        playerAnimator.SetBool(animation, animateBool);
    }
        
}
