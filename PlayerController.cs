using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Camera _gameCamera = null;
    [SerializeField] private Transform _anchor = null;
    [SerializeField] private GameObject _startPlanet = null;

    [SerializeField] private float _speedRotateCoefficient = 10f;
    [SerializeField] private float _maxSpeedRotate = 5f;

    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private GameObject _spear = null;

    IPlayerMovement _playerMovement;
    IGun _gun;

    private bool _isNotFoundComponent;

    private void Start()
    {
        if (_gameCamera == null)
        {
            Debug.LogError("Где камера, Лебовски?");
            _isNotFoundComponent = true;
            return;
        }

        if (_anchor == null)
        {
            Debug.LogError("Где якорь, Лебовски?");
            _isNotFoundComponent = true;
            return;
        }

        if (_startPlanet == null)
        {
            Debug.LogError("Где стартовая планета, Лебовски?");
            _isNotFoundComponent = true;
            return;
        }

        if (_spear == null)
        {
            Debug.LogError("Где копье, Лебовски?");
            _isNotFoundComponent = true;
            return;
        }

        _playerMovement = new PlayerMovement();
        _playerMovement.InitialInitialization(transform, _anchor, _startPlanet);

        _gun = new Gun(_spear, transform);

    }


    private void Update()
    {

        if (!_isNotFoundComponent)
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition(Input.mousePosition);
            //Debug.Log(mouseWorldPos);
            _playerMovement.RotatePlayerAroundPlanet(mouseWorldPos, _speedRotateCoefficient, _maxSpeedRotate);
            _playerMovement.MovePlayerToSelectedPlanet(mouseWorldPos, Input.GetMouseButtonDown(1), _moveSpeed, _layerMask);

            if (Input.GetMouseButtonDown(0)) _gun.Shoot();
        } 
    }

    private Vector3 GetMouseWorldPosition(Vector2 mousePosition)
    {
        float cameraToPlayerDistance = _gameCamera.transform.position.y - transform.position.y;
        Vector3 mouseWorldPos = _gameCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cameraToPlayerDistance));
        return mouseWorldPos;
    }
}
