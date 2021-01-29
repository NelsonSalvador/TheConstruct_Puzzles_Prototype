using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class StairTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool _isX;
    [HideInInspector]
    public bool _isZ;

    public GameObject _pivot;

    public AudioSource rotateSound;

    [HideInInspector]
    public int _direction;
    private bool _isRunning = false;

    private float angle;

    RaycastHit alongZ;
    RaycastHit alongX;

    new Vector3 renderer;
    private void Start()
    {
        rotateSound = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isRunning)
        {
            GameObject.FindGameObjectWithTag("World").GetComponent<RotateWorld>().rotating = true;
            GameObject.FindGameObjectWithTag("World").GetComponent<RotateWorld>()._stairsVariables = this.gameObject;
            rotateSound.Play();
            StartCoroutine(Wait());
        }
        
    }

    private void Update()
    {
        renderer = this.gameObject.GetComponent<Renderer>().bounds.center;

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


        if (Physics.Raycast(renderer
            + transform.TransformDirection(Vector3.forward) 
            + (transform.TransformDirection(Vector3.left)* 1.5f)
            + (transform.TransformDirection(Vector3.up) * 0.5f)
            , transform.TransformDirection(Vector3.right)
            , out alongZ, 2f, 1 << LayerMask.NameToLayer("Player")) && !_isRunning)
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
                if (179 < angle && angle < 181)
                    _direction = 1;
                else
                    _direction = -1;
            }
            
           

        }
        else if (Physics.Raycast(renderer 
            + (transform.TransformDirection(Vector3.up) * 1.5f)
            + (transform.TransformDirection(Vector3.left) * 2)
            + (transform.TransformDirection(Vector3.up) * 0.5f)
            , transform.TransformDirection(Vector3.right)
            , out alongX, 6f, 1 << LayerMask.NameToLayer("Player")) && !_isRunning)

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

    private IEnumerator Wait()
    {
        _isRunning = true;

        yield return new WaitForSeconds(1);          
       
        _isRunning = false;
    }
}
