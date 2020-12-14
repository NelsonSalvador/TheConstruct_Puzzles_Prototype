using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool _isX;
    [HideInInspector]
    public bool _isZ;

    public GameObject _pivot;

    [HideInInspector]
    public int _direction;
    private bool _isRunning = false;

    private float angle;

    RaycastHit alongZ;
    RaycastHit alongX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isRunning)
        {
            GameObject.FindGameObjectWithTag("World").GetComponent<RotateWorld>().rotating = true;
            GameObject.FindGameObjectWithTag("World").GetComponent<RotateWorld>()._stairsVariables = this.gameObject;
            StartCoroutine(Wait());
        }
        
    }

    private void Update()
    {

        if (this.transform.right == _pivot.transform.right ||
            this.transform.right == - _pivot.transform.right)
        {
            _isX = true;
            _isZ = false;
        }
        else
        {
            _isX = false;
            _isZ = true;
        }


        if (Physics.Raycast(this.gameObject.GetComponent<Renderer>().bounds.center, transform.TransformDirection(Vector3.forward), out alongZ, 1f) && !_isRunning)
        {
            
            CharacterController player = alongZ.collider as CharacterController;
            if (player != null)
            {
                if (_isX)
                {
                    angle = Vector3.Angle(transform.TransformDirection(Vector3.forward), _pivot.transform.TransformDirection(Vector3.forward));

                    if ((179 < angle && angle < 181) || (89 < angle && angle < 91))
                        _direction = 1;
                    else
                        _direction = -1;
                }
                else
                {
                    angle = Vector3.Angle(transform.TransformDirection(Vector3.forward), _pivot.transform.TransformDirection(Vector3.right));
                    Debug.Log(angle);
                    if (179 < angle && angle < 181)
                        _direction = 1;
                    else
                        _direction = -1;
                }
                
            }
            
        }
        else if (Physics.Raycast(this.gameObject.GetComponent<Renderer>().bounds.center, transform.TransformDirection(Vector3.up), out alongX, 1f) && !_isRunning)
        {
             
            CharacterController player = alongX.collider as CharacterController;
            if (player != null)
            {
                if (_isX)
                {
                    angle = Vector3.Angle(transform.TransformDirection(Vector3.up), _pivot.transform.TransformDirection(Vector3.forward));

                    if (179 < angle && angle < 181)
                        _direction = 1;
                    else
                        _direction = -1;
                }
                else
                {
                    angle = Vector3.Angle(transform.TransformDirection(Vector3.up), _pivot.transform.TransformDirection(Vector3.right));
                    Debug.Log(angle);
                    if (179 < angle && angle < 181)
                        _direction = 1;
                    else
                        _direction = -1;
                }
            } 


        }
        Debug.DrawRay(this.gameObject.GetComponent<Renderer>().bounds.center, transform.TransformDirection(Vector3.up), Color.green);
        Debug.DrawRay(this.gameObject.GetComponent<Renderer>().bounds.center, transform.TransformDirection(Vector3.forward), Color.blue);
    }

    private IEnumerator Wait()
    {
        _isRunning = true;

        yield return new WaitForSeconds(1);          
       
        _isRunning = false;
    }
}
