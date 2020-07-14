using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{

    private Transform _player;
    private Transform _anchor;
    private GameObject _currentPlanet;


    private bool _isMove;

    private Vector3 _hitNormal;

    /// <summary>
    /// Перемещение игрока на выбранную планету.
    /// </summary>
    public void MovePlayerToSelectedPlanet(Vector3 mouseWorldPos, bool inputGetButtonDown, float moveSpeed, LayerMask layerMask)
    {
        // Вектор, направленный из позиции игрока до указателя мыши
        Vector3 direction = mouseWorldPos - _player.position;

        // Луч, направленный из позиции игрока на указатель мыши
        Ray ray = new Ray(_player.position, direction);
        Debug.DrawRay(_player.position, direction, Color.red);

        RaycastHit hit;

        if (inputGetButtonDown && Physics.Raycast(ray, out hit, layerMask))
        {
            GameObject planetHit = hit.collider.gameObject;

            if (planetHit != _currentPlanet)
            {
                _currentPlanet = planetHit;
                _hitNormal = hit.normal;
            }
        }

        Vector3 moveDirection = _currentPlanet.transform.position - _anchor.position;

        if (moveDirection.magnitude > 0.2f)
        {
            _isMove = true;

            // Перемещение
            _anchor.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Поворот
            float angleRot = Vector3.SignedAngle(_anchor.right, _hitNormal, Vector3.up);
            _anchor.Rotate(Vector3.up, angleRot * moveSpeed * Time.deltaTime);

        }
        else if (moveDirection.magnitude <= 0.2f && moveDirection.magnitude > 0.03f)
        {
            // Перемещение
            _anchor.Translate(moveDirection.normalized * 0.2f * moveSpeed * Time.deltaTime, Space.World);

            // Поворот
            //float angleRot = Vector3.SignedAngle(_anchor.right, _hitNormal, Vector3.forward);
            //_anchor.Rotate(Vector3.forward, angleRot * moveSpeed * Time.deltaTime);
        }
        else
        {
            _anchor.position = _currentPlanet.transform.position;
            _isMove = false;
        }
    }

    /// <summary>
    /// Вращение игрока вокруг текущей планеты.
    /// </summary>
    public void RotatePlayerAroundPlanet(Vector3 mouseWorldPos, float speedRotCoefficient, float maxSpeedRot)
    {
        if (!_isMove)
        {
            // Вектор якорь-указатель
            Vector3 anchorPointerVect = mouseWorldPos - _anchor.position;

            // Вычисление угла поворота по оси Z
            float angleRot = Vector3.SignedAngle(_anchor.right, anchorPointerVect.normalized, Vector3.up) * speedRotCoefficient * Time.deltaTime;

            // Ограничение угловой скорости
            angleRot = Mathf.Clamp(angleRot, -maxSpeedRot, maxSpeedRot);

            // Вращение по оси Z
            _anchor.Rotate(Vector3.up, angleRot);
        }
    }


    /// <summary>
    /// Начальная инициализация полей экземпляра класса PlayerMovement.
    /// </summary>
    public void InitialInitialization(Transform player, Transform anchor, GameObject startPlanet)
    {
        _player = player;
        _anchor = anchor;
        _currentPlanet = startPlanet;

        anchor.position = startPlanet.transform.position;
    }
}
