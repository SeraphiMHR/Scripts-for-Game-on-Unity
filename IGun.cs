using System.Collections;
using UnityEngine;

public interface IGun
{
    void Shoot();
    bool GetPlanetToTeleport(out RaycastHit hit, Vector3 __direction);
}
