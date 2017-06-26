using System.Collections;
using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class BlockManager : MonoBehaviour
    {
        private Renderer render;
        private Rigidbody rigidBody;
        private float waitTime = 2.0f;
        private float moveTime = 5.0f; 
        private float deleteTime = 5.0f;

        private void Start()
        {
            render = GetComponent<Renderer>();
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Bullet"))
            {
                BulletManager b = other.GetComponent<BulletManager>();

                if (b.GetIsActive())
                {
                    StartCoroutine(MoveUp());
                    Material material = Resources.Load("Materials/Block_Red") as Material;
                    render.material = material;
                }
            }
        }

		private IEnumerator MoveUp()
		{
            yield return new WaitForSeconds(waitTime);
            rigidBody.isKinematic = false;
            Destroy(gameObject, deleteTime);
		}
    }
}
