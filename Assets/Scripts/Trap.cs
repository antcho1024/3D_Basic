using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Animator anim = null;
    private void Awake()
    {
        anim= GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{other.name}가 함정을 밟음");
        anim.SetTrigger("TrapActive");
    }
}
