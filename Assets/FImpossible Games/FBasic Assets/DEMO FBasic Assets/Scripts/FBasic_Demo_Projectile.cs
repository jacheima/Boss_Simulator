using UnityEngine;

namespace FIMSpace.Basics
{
    public class FBasic_Demo_Projectile : FBasic_ProjectileBase
    {
        protected override void HitTarget(RaycastHit hit)
        {
            if ( hit.transform )
            {
                FBasic_Shared_BulletHittable hittable = hit.transform.GetComponent<FBasic_Shared_BulletHittable>();
                if (hittable)
                {
                    if (hittable.OnProjectileHit != null) hittable.OnProjectileHit.Invoke();
                }
            }

            base.HitTarget(hit);
        }
    }
}