using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour {

    public enum TRIGGER {  rwalkable, lwalkable, ledge, notfalling, dead };
    private string[] animvars = { "rwalkable", "lwalkable", "ledge", "!falling", "dead" };
    public Animator princeanim;
    public TRIGGER trigger;

    void OnTriggerStay2D(Collider2D coll)
    {
        if (!coll.CompareTag("Player") || trigger == TRIGGER.dead)
            princeanim.SetBool(animvars[(int)trigger], true);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (!coll.CompareTag("Player") && trigger != TRIGGER.dead)
            princeanim.SetBool(animvars[(int)trigger], false);
    }
}