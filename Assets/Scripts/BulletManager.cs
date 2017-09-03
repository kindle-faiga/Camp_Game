using System.Collections;
using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(RigidbodyManager))]
    public class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private float aliveTime = 1.0f;

        private int id = 1;
        private bool isActive = false;
        private float rotation = 0;
        private RigidbodyManager rigidbodyManager;

		private void Start()
		{
			rigidbodyManager = GetComponent<RigidbodyManager>();
		}

        public int GetId() { return id; }
        public void SetId(int _id) { id = _id; }
        public bool GetIsActive() { return isActive; }
        public void SetIsActive(bool _isActive) { isActive = _isActive; }

        public IEnumerator Shot(Vector3 position, float _rotation)
        {
            isActive = true;
            transform.position = position;
            rotation = _rotation;
            yield return new WaitForSeconds(aliveTime);
            isActive = false;
            rigidbodyManager.Stop();
        }

        private void FixedUpdate()
        {
            if (isActive)
            {
                float sin = Mathf.Sin(Mathf.Deg2Rad * rotation);
                float cos = Mathf.Cos(Mathf.Deg2Rad * rotation);
                rigidbodyManager.Move(new Vector3(sin,0,cos));
            }
        }
    }
}