using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] AudioSource aud;

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] audJump;
    [Range(0, 1)][SerializeField] float audJumpVol;

    [SerializeField] AudioClip[] audHurt;
    [Range(0, 1)][SerializeField] float audHurtVol;

    [SerializeField] AudioClip[] audSteps;
    [Range(0, 1)][SerializeField] float audStepsVol;




    public void PlayJump()
    {
        aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
    }
    public void PlayHurt()
    {
        aud.PlayOneShot(audHurt[Random.Range(0, audHurt.Length)], audHurtVol);
    }
    public IEnumerator PlaySteps()
    {
        GameManager.instance.basePlayer.playingSteps = true;
        aud.PlayOneShot(audSteps[Random.Range(0, audSteps.Length)], audStepsVol);

        if (!GameManager.instance.basePlayer.isSprinting)
            yield return new WaitForSeconds(0.5f);
        else
            yield return new WaitForSeconds(0.3f);

        GameManager.instance.basePlayer.playingSteps = false;
    }
}
