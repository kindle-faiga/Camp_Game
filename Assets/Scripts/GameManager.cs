using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CampGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager[] playerManager;
        [SerializeField]
        private Text[] texts;

        private int deadCount = 5;

        public int GetDeadCount()
        {
            --deadCount;

            return deadCount;
        }

        IEnumerator Start()
        {
            foreach (Text t in texts)
			{
                t.enabled = false;
			}

            yield return new WaitForSeconds(2.0f);

            texts[0].enabled = true;

			yield return new WaitForSeconds(1.0f);

            texts[0].enabled = false;
			texts[1].enabled = true;

			yield return new WaitForSeconds(1.0f);

			texts[1].enabled = false;
			texts[2].enabled = true;

			yield return new WaitForSeconds(1.0f);

			texts[2].enabled = false;
			texts[3].enabled = true;

            foreach(PlayerManager p in playerManager)
            {
                p.SetStart();
            }

			yield return new WaitForSeconds(0.5f);

			texts[3].enabled = false;
		}
    }
}