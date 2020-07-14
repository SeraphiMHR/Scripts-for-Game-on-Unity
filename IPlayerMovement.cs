using UnityEngine;

interface IPlayerMovement
{
    /// <summary>
    /// Перемещение игрока на выбранную планету.
    /// </summary>
    void MovePlayerToSelectedPlanet(Vector3 mouseWorldPos, bool inputGetButtonDown, float moveSpeed, LayerMask layerMask);

    /// <summary>
    /// Вращение игрока вокруг текущей планеты.
    /// </summary>
    void RotatePlayerAroundPlanet(Vector3 mouseWorldPos, float speedRotCoefficient, float maxSpeedRot);

    /// <summary>
    /// Начальная инициализация полей экземпляра класса PlayerMovement.
    /// </summary>
    void InitialInitialization(Transform player, Transform anchor, GameObject startPlanet);
}
