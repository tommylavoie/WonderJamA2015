using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SeekCameraToPlayer(Player obj)
    {
        float x = obj.transform.position.x;
        float y = obj.transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(x,y,-10), ref velocity, 0.1f);
    }
}
