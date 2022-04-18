using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip fireSound, reloadSound;
    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        fireSound = Resources.Load<AudioClip>("fire");
        reloadSound = Resources.Load<AudioClip>("reloading");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip){
            case "fire":
                audioSrc.PlayOneShot(fireSound);
            break;

            case "reloading":
                audioSrc.PlayOneShot(reloadSound);
                break;
        } 
    }
}
