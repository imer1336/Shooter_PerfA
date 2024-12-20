using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float ShootCoolDown = 1;
    public GameObject BulletPrefab;

    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity playerEntity = GetEntity(TransformUsageFlags.None);
            AddComponent(playerEntity, new PlayerComponent
            {
                MoveSpeed = authoring.MoveSpeed,
                ShootCoolDown = authoring.ShootCoolDown,
                BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.None)
            });
        }
    }
}
