using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPatroller : SimplePatroller
{
    public Animation anim;

    void Update() {
        if (hopping && !anim.isPlaying) {
            anim.Play();
        }
    }
}
