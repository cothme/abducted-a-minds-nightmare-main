using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class RunnersAttackScript : MonoBehaviour
{
    [SerializeField] AudioSource runnersHitSound;
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Player")
        {
            if(PlayerState.Instance.RunnersHit == false)
            {
                StartCoroutine(RunnersHitCoroutine());
            }
            else
            {
                return;
            }
        }
    }
    IEnumerator RunnersHitCoroutine()
    {
        runnersHitSound.Play();
        PlayerState.Instance.RunnersHit = true;
        yield return new WaitForSeconds(3f);
        PlayerState.Instance.RunnersHit = false;
    }
}
