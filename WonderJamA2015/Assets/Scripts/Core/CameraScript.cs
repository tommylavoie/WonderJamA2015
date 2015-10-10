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

    public void SeekCameraToPlayer(Player obj, float speed)
    {
        float x = obj.transform.position.x;
        float y = obj.transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(x,y,-10), ref velocity, speed);
    }

    public void SeekCameraToGoal(EndGoal obj, float speed)
    {
        float x = obj.transform.position.x;
        float y = obj.transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(x, y, -10), ref velocity, speed);
    }

    public void DirectGoTo(Vector3 position)
    {
        transform.position = position;
    }
}
