using UnityEngine;

namespace CampGame
{
    public class BlockManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Bullet"))
            {
                BulletManager b = other.GetComponent<BulletManager>();

                if (b.GetIsActive())
                {
                    Debug.Log(transform.position);
                }
            }
        }
    }
}
