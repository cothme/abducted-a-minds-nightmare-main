using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BrutesAttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PostProcessVolume postProcessVolume;
    [SerializeField] PostProcessProfile hasVignette;
    [SerializeField] PostProcessProfile noVignette;
    void Start()
    {
        
    }
    private void Update()
    {
        Debug.Log(PlayerState.Instance.BrutesHit);
    }
    // Update is called once per frame
     void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Player")
        {
            if(PlayerState.Instance.BrutesHit == false)
            {
                postProcessVolume.profile = hasVignette;
                StartCoroutine(BrutesHitCoroutine());
            }
            else
            {
                return;
            }
        }
    }
    IEnumerator BrutesHitCoroutine()
    {
        PlayerState.Instance.BrutesHit = true;
        yield return new WaitForSeconds(5f);
        PlayerState.Instance.BrutesHit = false;
        postProcessVolume.profile = noVignette;
    }
}
