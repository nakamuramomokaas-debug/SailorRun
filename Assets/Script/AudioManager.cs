using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GetSE;
    public AudioClip GoldGetSE;
    public AudioClip ExplosionSE;
    public AudioClip JumpSE;
    public AudioClip DisisionSE;
    AudioSource audioSource;
    
    void Start () 
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    public void GetSound() {audioSource.PlayOneShot(GetSE);}
    public void GoldGetSound() {audioSource.PlayOneShot(GoldGetSE);}
    public void ExplosionSound() {audioSource.PlayOneShot(ExplosionSE);}
    public void JumpSound() {audioSource.PlayOneShot(JumpSE);}
    public void DisisionSound() {audioSource.PlayOneShot(DisisionSE);}
}
