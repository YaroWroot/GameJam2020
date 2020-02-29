 using UnityEngine;
 using System.Collections;

 public class CameraControlScript : MonoBehaviour
{
    public Vector3 _cameraOffset = new Vector3(0, 18, -10);

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + _cameraOffset;
    }
}
