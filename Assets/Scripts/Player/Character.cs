using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator anim;
    private string animName;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        ChangeAnim(Constants.ANIM_IDLE);
    }

    public virtual void OnDespawn()
    {

    }

    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }

    public void ResetAnim()
    {
        ChangeAnim(Constants.ANIM_IDLE);
    }
}
