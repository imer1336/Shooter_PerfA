using Unity.Entities;

public struct PlayerComponent : IComponentData
{
   public float MoveSpeed;
   public float ShootCoolDown;
   public Entity BulletPrefab;
}
