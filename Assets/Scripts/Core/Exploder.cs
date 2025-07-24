using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Exploder
    {
        public void Explode(Vector3 position, float force, float radius)
        {
            Collider[] hits = Physics.OverlapSphere(position, radius);

            foreach (Collider hit in hits)
            {
                if (hit.attachedRigidbody == null)
                    continue;

                hit.attachedRigidbody.AddExplosionForce(force, position, radius);
            }
        }
    }
}