using Unity.Mathematics;
using Unity.Entities;

public struct EnemySpawnerComponent : IComponentData
{
    public float spawnCooldown;
    public float2 cameraSize;
}
