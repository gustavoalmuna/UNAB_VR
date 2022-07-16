using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    //hacemos que la luz parpadee
    public float minWaitTime = 0.1f;
    public float maxWaitTime = 0.5f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightPulse());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LightPulse()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            GetComponent<Light>().intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
