using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
{
    public GameObject _player;
    public Transform _pivot;
    public GameObject _children;

    [HideInInspector]
    public GameObject _stairsVariables;


    private Vector3 playerPos;

    public bool rotating;
    public float RotationSpeed;

    private void Start()
    {
        rotating = false;
    }

    // Update is called once per frame
    void Update()
    {       

        if (!rotating)
            UpdatePivot();
        else
        {
            Rotate();
        }

    }

    private void UpdatePivot()
    {
        playerPos = new Vector3(_player.transform.position.x,
                                _player.transform.position.y - 0.5f,
                                _player.transform.position.z);

        _pivot.position = playerPos;
    }

    private void Rotate()
    {
        _children.transform.SetParent(_pivot);
       
        if(_stairsVariables.GetComponent<StairTrigger>()._isX && _stairsVariables.GetComponent<StairTrigger>()._direction == 1)
        {

            _pivot.Rotate(Vector3.right * (RotationSpeed * Time.deltaTime));
            
            if(_pivot.rotation.eulerAngles.x >= 85)
            {
                StopRotation(new Vector3(90, 0, 0));
            }
        }
        else if (_stairsVariables.GetComponent<StairTrigger>()._isX && _stairsVariables.GetComponent<StairTrigger>()._direction == -1)
        {
            _pivot.Rotate(Vector3.left * (RotationSpeed * Time.deltaTime));
            if (_pivot.rotation.eulerAngles.x <= 275)
            {
                StopRotation(new Vector3(270, 0, 0));
            }
        }
        else if (_stairsVariables.GetComponent<StairTrigger>()._isZ && _stairsVariables.GetComponent<StairTrigger>()._direction == -1)
        {
            _pivot.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime));
            if (_pivot.rotation.eulerAngles.z >= 85)
            {
                StopRotation(new Vector3(0, 0, 90));
            }
        }
        else if (_stairsVariables.GetComponent<StairTrigger>()._isZ && _stairsVariables.GetComponent<StairTrigger>()._direction == 1)
        {
            _pivot.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime));
            if (_pivot.rotation.eulerAngles.z <= 275)
            {
                StopRotation(new Vector3(0, 0, 270));
            }
        }
    }

    private void StopRotation(Vector3 rotation)
    {
        rotating = false;
        _pivot.rotation = Quaternion.Euler(rotation);
        _children.transform.SetParent(null);
        _pivot.rotation = Quaternion.identity;
    }
}
