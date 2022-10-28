using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    [SerializeField] private float _moveSpeed = 2f;

    private int _waypointIndex = 0;

    private void Start()
    {
        transform.position = _waypoints[_waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(_waypointIndex <= _waypoints.Length -1)
        {
            transform.position = Vector2.MoveTowards(transform.position, _waypoints[_waypointIndex].transform.position,_moveSpeed * Time.deltaTime);

        }

        if(transform.position == _waypoints[_waypointIndex].transform.position)
        {
            _waypointIndex++;
        }
    }
}
