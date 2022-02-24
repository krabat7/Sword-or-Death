using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    private Finish _finish;
    [SerializeField] private Animator animator;

    void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
    }

    public void ActivateLeverArm()
    {
        animator.SetTrigger("activate");
        _finish.Activate();
    }
}

