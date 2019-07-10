using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
 * Referenced from: https://unity3d.college/2016/04/11/baseball-bat-physics-unity/
 */

public class ClubHeadFollower : MonoBehaviour
{
    ClubHeadSpawner _spawner;
    Rigidbody _rigidbody;
    Vector3 _velocity;

    [SerializeField]
    private float sensitivity = 50f;

    [SerializeField]
    private float maxSqrVelocity = 20f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 destination = _spawner.transform.position;
        _rigidbody.transform.rotation = transform.rotation;

        _velocity = (destination - _rigidbody.transform.position) * sensitivity;

        //scale velocity to the max sqr velocity
        if(_velocity.sqrMagnitude > maxSqrVelocity)
        {
            _velocity = _velocity * Mathf.Sqrt(maxSqrVelocity / _velocity.sqrMagnitude);
        }

        _rigidbody.velocity = _velocity;
        transform.rotation = _spawner.transform.rotation;
    }

    public void SetTarget(ClubHeadSpawner clubHeadSpawner)
    {
        _spawner = clubHeadSpawner;
    }
}
