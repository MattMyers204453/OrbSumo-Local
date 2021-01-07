using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Assets
{
    public class Powers : MonoBehaviour
    {
        //Transparent Power stuff
        public Rigidbody sphere;
        public Material normal;
        public Material transparent;

        public string playerNumber;

        //Big Power stuff
        public float animationDuration = 1f;
        public float xScale = 2.0f;
        public float yScale = 2.0f;
        public float zScale = 2.0f;
        private Vector3 bigScale;
        private Vector3 smallScale;

        private string loadedPower;
        private string loadedAntiPower;
        private string loadedIconTag;

        private String[] powerNames = { "useTransparentPower", "useBigPower", "usePunchPower"};
        private String[] antiPowerNames = { "stopUsingTransparentPower", "stopUsingBigPower", "stopUsingPunchPower" };
        private String[] iconTags = { "Ghost", "Big", "Punch"};

        public AudioSource powerLoadedSound;
        public AudioSource useGhostSound;
        //public AudioSource endGhostSound;
        public AudioSource useBigSound;
        //public AudioSource endBigSound;
        public AudioSource usePunchSound;
        //public AudioSource endPunchSound;

        public void loadRandomPower()
        {
            System.Random random = new System.Random();
            int randomPowerIndex = random.Next(powerNames.Length);
            loadedPower = powerNames[randomPowerIndex];
            loadedAntiPower = antiPowerNames[randomPowerIndex];
            loadedIconTag = iconTags[randomPowerIndex] + playerNumber;
            if (!ApplicationState.startOfNewGameScene)
            {
                powerLoadedSound.Play();
            }
            showPowerIcon();
        }

        public void delayedLoadRandomPower()
        {
            Invoke("loadRandomPower", 0.1f);
        }

        public void executeLoadedPower(out float powerDuration)
        {
            MethodInfo miExecute = this.GetType().GetMethod(loadedPower);
            float powerDurationHolder = 0;
            object[] args = { powerDurationHolder };
            miExecute.Invoke(this, args);
            powerDuration = (float)args[0];
        }

        public void stopUsingPower()
        {
            MethodInfo miStop = this.GetType().GetMethod(loadedAntiPower);
            miStop.Invoke(this, null);
            hidePowerIcon();
        }

        private void showPowerIcon()
        {
            GameObject.FindGameObjectWithTag(loadedIconTag).GetComponent<Image>().enabled = true;
        }

        private void hidePowerIcon()
        {
            GameObject.FindGameObjectWithTag(loadedIconTag).GetComponent<Image>().enabled = false;
        }

        //Makes the player transparent for 3 seconds
        public void useTransparentPower(out float duration)
        {
            sphere.GetComponent<Renderer>().material = transparent;
            Physics.IgnoreLayerCollision(8, 8, true);
            useGhostSound.Play();
            duration = 3f;
        }

        //Reverses the transparent power
        public void stopUsingTransparentPower()
        {
            sphere.GetComponent<Renderer>().material = normal;
            Physics.IgnoreLayerCollision(8, 8, false);
            //endGhostSound.Play();
        }

        //Makes the player larger for 5 seconds
        public void useBigPower(out float duration)
        {
            smallScale = sphere.transform.localScale;
            bigScale = new Vector3(xScale, yScale, zScale);
            sphere.mass += 0.12f;
            useBigSound.Play();
            StartCoroutine(ScaleUpAnimation(smallScale, bigScale, animationDuration));
            duration = 5f;
        }

        //Helper method for the big power
        IEnumerator ScaleUpAnimation(Vector3 start, Vector3 end, float time)
        {
            float i = 0;
            float rate = 1 / time;
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                transform.localScale = Vector3.Lerp(start, end, i);
                yield return 0;
            }
        }

        //Reverses the big power
        public void stopUsingBigPower()
        {
            sphere.mass -= 0.12f;
            //endGhostSound.Play();
            StartCoroutine(ScaleDownAnimation(bigScale, smallScale, animationDuration));
        }

        //Helper method to reverse the big power
        IEnumerator ScaleDownAnimation(Vector3 end, Vector3 start, float time)
        {
            float i = 0;
            float rate = 1 / time;
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                sphere.transform.localScale = Vector3.Lerp(end, start, i);
                yield return 0;
            }
        }

        //Makes the player larger for 5 seconds
        public void usePunchPower(out float duration)
        {
            sphere.gameObject.GetComponent<PostProcessVolume>().enabled = true;
            sphere.gameObject.GetComponent<PlayerScript>().adjustOpponentBounce = true;
            sphere.gameObject.GetComponent<PlayerScript>().opponentBounceChange = 5f;
            usePunchSound.Play();
            duration = 3f;
        }

        //Makes the player larger for 5 seconds
        public void stopUsingPunchPower()
        {
            sphere.gameObject.GetComponent<PostProcessVolume>().enabled = false;
            sphere.gameObject.GetComponent<PlayerScript>().adjustOpponentBounce = false;
            sphere.gameObject.GetComponent<PlayerScript>().opponentBounceChange = 0f;
            //endPunchSound.Play();
        }
    }
}
