using UnityEngine;
using System.Collections;

public class OffsetAnim : MonoBehaviour {

    public bool hasFlag = false;
    Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("hasFlag", hasFlag);
        if (hasFlag)
        {
            anim.Play("Flag", 0, Random.Range(0.0f, 1.0f));
        }
           else
        {
            anim.Play("NoFlag", 0, Random.Range(0.0f, 1.0f));
        }      
    }
}
