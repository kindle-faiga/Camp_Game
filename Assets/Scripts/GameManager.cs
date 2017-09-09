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
		[SerializeField]
		private Text[] ranks;

        private int deadCount = 5;
        private int deadNum = 3;
        private int rankCount = 9;

        public int GetDeadCount(int id)
        {
            --deadCount;

            --deadNum;
            rankCount -= id;

            if(deadNum <= 0)
            {
                ranks[rankCount].enabled = true;
                ranks[rankCount].text = "1";
                texts[4].enabled = true;
                texts[4].text = "P" + (rankCount + 1) + " Win!";
            }

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