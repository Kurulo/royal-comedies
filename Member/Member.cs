using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Member : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [Header("AnimationSettings")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _poseClip;
    [SerializeField] private AnimationClip _danceClip;

    private Transform _selfTransform;
    private Transform _destination;
    private CharacterController _controller;

    private List<Transform> _destinationWay = new List<Transform>();
    private int _currentDestinationID = 0;

    private bool _isStartDesination;

    private bool _isReachedDestination = false;
    private bool _isReadyToByChosen = false;
    public bool IsReadyToByChosen { get { return _isReadyToByChosen; } }

    public bool IsReachedDestination { get { return _isReachedDestination; } }
    public bool IsReachedStartDestination {  get { return _isStartDesination; } }

    private bool _isWinersEventStart = false;
    public bool IsWinersEventStart { set { _isWinersEventStart =value; } }

    private void OnEnable()
    {
        if (_destinationWay.Count != 0)
            _destinationWay.Clear();
    }

    public void InitializedComponents()
    {
        if (_selfTransform == null)
            _selfTransform = transform;

        _isReadyToByChosen = false;
        _isStartDesination = true;

        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused) 
        {
            _animator.speed = 0f;
            return; 
        }
        else
        {
            _animator.speed = 1f;
        }

        if (!_isWinersEventStart)
        {
            if (!_isReachedDestination)
            {
                ReachDestination();
            }
            else if (_isStartDesination)
            {
                _isStartDesination = false;
                ClearList();
                _animator.SetBool("StayPosition", true);
            }
        }
    }
    public void IsReadyToChoosen()
    {
        _isReadyToByChosen = true;
    }

    public void LoserBehaviour()
    {
        _moveSpeed = _moveSpeed * .3f;
        _animator.SetBool("LoserWalk", true);
    }

    public void WinerBehaviour()
    {
        _moveSpeed = _moveSpeed * .3f;
        _animator.SetBool("WinerWalk", true);
    }

    public void WinerEventBehaviour()
    {
        _animator.SetBool("Dance", true);
    }

    public void ReachDestination()
    {
        if (_destinationWay.Count > 0)
        {
            var destination = _destinationWay[_currentDestinationID];

            if (!CheckReachDestionation(destination.position))
            {
                Vector3 direction = (destination.position - _selfTransform.position).normalized;
                Vector3 movement = direction * _moveSpeed * Time.deltaTime;

                Movement(direction, movement);
            }
            else
            {
                _currentDestinationID++;
            }

            _isReachedDestination = CheckReachDestionation(_destinationWay[_destinationWay.Count - 1].position);

            if (_isReachedDestination && !_isStartDesination)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Movement(Vector3 direction, Vector3 movement)
    {
        if (direction != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            _selfTransform.rotation = Quaternion.Lerp(_selfTransform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        _controller.Move(movement);
    }

    private bool CheckReachDestionation(Vector3 destination)
    {
        if (Vector3.Distance(_selfTransform.position, destination) < 1f)
        {
            return true;
        }

        return false;
    }

    public void ClearList()
    {
        _destinationWay.Clear();
    }

    public void SetDestinationWay(Transform destination)
    {
        _destinationWay.Add(destination);
        _currentDestinationID = 0;
        if (!_isStartDesination)
        {
            _animator.SetBool("StayPosition", false);
        }

    }

    public void SetReady()
    {
        _isReachedDestination = false;
    }
}
