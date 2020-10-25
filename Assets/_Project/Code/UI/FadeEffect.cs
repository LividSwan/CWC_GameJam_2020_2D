using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class FadeEffect : MonoBehaviour

    {
        public Animator animator;

        public void FadeOutEffect()
        {
            animator.SetTrigger("FadeOut");
        }
    }

