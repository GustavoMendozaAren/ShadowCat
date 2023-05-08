using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightEnemy : MonoBehaviour
{

    public Light2D lightToDim;

    private float mEndTime = 0f;
    private float mStartTime = 1.5f;

    private float contador;

    public bool DimLight = false;
    // Start is called before the first frame update
    void Start()
    {
        lightToDim = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DimLight)
        {
            contador += Time.deltaTime * 2.8f;
            lightToDim.intensity = Mathf.InverseLerp(mStartTime, mEndTime, contador);
        }

    }
}
