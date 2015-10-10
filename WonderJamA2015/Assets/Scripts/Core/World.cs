using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*<summary>
 * Game's central entity. Is a singleton.
</summary>*/

public class GameMaster : MonoBehaviour
{
    private static GameMaster _instance;

    public static GameMaster instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameMaster>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;


    }

    void start()
    {

    }

    public void OnGUI()
    {

    }
}

