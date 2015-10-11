using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void PlaySound(AudioClip clip)
    {
        CameraScript cam = (CameraScript)(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>());
        AudioSource audiosource = cam.GetComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.loop = false;
        audiosource.Play();
    }
}
