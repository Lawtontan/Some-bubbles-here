using System.Collections;
using UnityEngine;

public class BubbleEffect_Behaviour : MonoBehaviour
{
    public ParticleSystem charge_effect;
    public float duration_charge;
    public ParticleSystem shrink_effect;
    public float duration_shrink;
    public ParticleSystem pop_effect;
    public float duration_pop;

    private Transform original_parent;

    private void Awake()
    {
        original_parent = transform.parent;
    }

    public void PlayChargeEffect()
    {
        charge_effect.Play();
        StartCoroutine(DelayPause(charge_effect, duration_charge));
    }

    public void PlayShrinkEffect()
    {
        shrink_effect.Play();
        StartCoroutine(DelayPause(shrink_effect, duration_shrink));
    }

    public void PlayPopEffect()
    {
        transform.parent = null;
        pop_effect.Play();
        StartCoroutine(DelayPause(pop_effect, duration_pop));
    }

    IEnumerator DelayPause(ParticleSystem effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.Stop();
        //incase the parent change while playing the animation
        transform.parent = original_parent;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
