using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtResourceManager : MonoBehaviour
{
    public static ArtResourceManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject CollideEffect;
    public GameObject DeadEffect;
    public GameObject HitEffect;
    public GameObject KickEffect;

}
