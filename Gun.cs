using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    private GameObject _spear;
    private Transform _player;
    private float _firerate = 5f;
    private float _nextTimeToFire = 0.0f;

    public void Shoot()
    {
        //if (_spear != null && Time.time > _nextTimeToFire)
        //{
        //    _nextTimeToFire = Time.time + 1.0f / _firerate;

        Ray ray = new Ray(_player.position, _player.right);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            GameObject spear = Instantiate(_spear);
            spear.transform.position = hit.point;
            spear.transform.SetParent(hit.transform);
        }

        //}
    }

    public Gun(GameObject spear, Transform playerTransform)
    {
        _spear = spear;
        _player = playerTransform;
    }


    public bool GetPlanetToTeleport(out RaycastHit hit, Vector3 __direction)
    {

        Ray ray = new Ray(transform.position, __direction);
        if (Physics.Raycast(ray, out hit) && hit.transform.TryGetComponent(out Object planet))
        {
            return true;
        }
        return false;
    }

}