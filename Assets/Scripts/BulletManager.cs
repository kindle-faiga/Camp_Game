using System.Collections;
using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(RigidbodyManager))]
    public class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private float aliveTime = 1.0f;

        private bool isActive = false;
        private Vector3 defaultPos;
        private RigidbodyManager rigidbodyManager;

        public bool GetIsActive() { return isActive; }
        public void SetIsActive(bool _isActive) { isActive = _isActive; }
        public IEnumerator Shot()
        {
            isActive = true;
            defaultPos = transform.position;
            yield return new WaitForSeconds(aliveTime);
            isActive = false;
            transform.position = defaultPos;
        }

        private void Start()
        {
            rigidbodyManager = GetComponent<RigidbodyManager>();
        }

        private void FixedUpdate()
        {
            if (isActive)
            {
                rigidbodyManager.Move(Vector3.forward);
            }
        }
    }
}