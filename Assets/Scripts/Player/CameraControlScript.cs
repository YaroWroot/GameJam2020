 using UnityEngine;
 using System.Collections;

 public class CameraControlScript : MonoBehaviour
{
    public Vector3 _cameraOffset = new Vector3(0, 18, -10);

    private GameObject _player;

    bool follow = true;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(follow) transform.position = _player.transform.position + _cameraOffset;
    }

    public void StopFollow()
    {
        follow = false;
    }
}
