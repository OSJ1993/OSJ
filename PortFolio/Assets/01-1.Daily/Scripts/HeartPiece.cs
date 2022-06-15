using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPiece : MonoBehaviour
{
    public void AnimatorOut()
    {
        GetComponent<Animator>().enabled = false;
    }
}
