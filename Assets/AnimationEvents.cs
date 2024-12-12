using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityEvent OnAnimationPlay;

    public string animationName;

    public bool isPlay = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator  = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var name = animator.GetCurrentAnimatorStateInfo(0);

        if (name.IsName(animationName) && !isPlay)
        {
            OnAnimationPlay.Invoke();
            isPlay = true;
        }
    }
}
