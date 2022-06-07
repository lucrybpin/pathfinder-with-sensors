using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public enum DayState {
    sunrise,
    morning,
    noon,
    afternoon,
    sunset,
    evening,
    night
}

[System.Serializable]
public class DayStateConfig {
    [SerializeField] Color lightColor;
    [SerializeField] float bloom;

    public Color LightColor { get => lightColor; }
    public float Bloom { get => bloom; }
}

public class DayAndNightController : MonoBehaviour {


    [SerializeField] DayState state;
    [SerializeField] DayStateConfig sunriseConfig;
    [SerializeField] DayStateConfig morningConfig;
    [SerializeField] DayStateConfig noonConfig;
    [SerializeField] DayStateConfig afternoonConfig;
    [SerializeField] DayStateConfig sunsetConfig;
    [SerializeField] DayStateConfig eveningConfig;
    [SerializeField] DayStateConfig nightConfig;

    [Range( 0, 24 )]
    [SerializeField] float currentTime;
    [SerializeField] float timeSpeed;

    [Header("Dependencies")]
    [SerializeField] Light sunDirectionalLight;
    //[SerializeField] Volume postProcessingVolume;

    private void Update ()
    {
        UpdateTime();
        UpdateState();
        UpdateSunAndMoonPosition();
    }

    private void UpdateSunAndMoonPosition ()
    {
        float dayPercentage = currentTime / 24;
        float sunXRotation = Mathf.Lerp( -90, 270, dayPercentage );
        sunDirectionalLight.transform.rotation = Quaternion.Euler( sunXRotation, 70, 0 );
    }

    private void UpdateTime ()
    {
        currentTime += Time.deltaTime * timeSpeed;
        if (currentTime > 24)
            currentTime = 0;
    }

    private void OnValidate ()
    {
        UpdateTime();
        UpdateState();
        UpdateSunAndMoonPosition();
    }

    private void UpdateState ()
    {
        if (currentTime > 5.2 && currentTime < 6.5)
        {
            if (state != DayState.sunrise)
            {
                StartCoroutine(AdjustConfig( sunriseConfig, 1f ));
                state = DayState.sunrise;
            }
        }
        else if (currentTime > 6.5 && currentTime < 11)
        {
            if (state != DayState.morning)
            {
                StartCoroutine( AdjustConfig( morningConfig, 2 ) );
                state = DayState.morning;
            }
        }
        else if (currentTime > 11 && currentTime < 12)
        {
            if (state != DayState.noon)
            {
                StartCoroutine( AdjustConfig( noonConfig, 1 ) );
                state = DayState.noon;
            }
        }
        else if (currentTime > 12 && currentTime < 17.2)
        {
            if (state != DayState.afternoon)
            {
                StartCoroutine( AdjustConfig( afternoonConfig, 2 ) );
                state = DayState.afternoon;
            }
        }
        else if (currentTime > 17.2 && currentTime < 18.5)
        {
            if (state != DayState.sunset)
            {
                StartCoroutine( AdjustConfig( sunsetConfig, 1 ) );
                state = DayState.sunset;
            }
        }
        else if (currentTime > 18.5 && currentTime < 21)
        {
            if (state != DayState.evening)
            {
                StartCoroutine( AdjustConfig( eveningConfig, 1 ) );
                state = DayState.evening;
            }
        }
        else
        {
            if (state != DayState.night)
            {
                StartCoroutine( AdjustConfig( nightConfig, 1 ) );
                state = DayState.night;
            }
        }
    }

    private IEnumerator AdjustConfig(DayStateConfig config, float totalTime)
    {
        Color startingColor = sunDirectionalLight.color;
        Color endingColor = config.LightColor;
        float elaspedTime = 0f;

        //Bloom bloom;
        //postProcessingVolume.profile.TryGet<Bloom>( out bloom );
        //float initialBloom = bloom.intensity.value;
        //float endingBloom = config.Bloom;

        while (elaspedTime < totalTime)
        {
            elaspedTime += Time.deltaTime;
            float percentage = elaspedTime / totalTime;
            sunDirectionalLight.color = Color.Lerp( startingColor, endingColor, percentage );
            //bloom.intensity.value = Mathf.Lerp( initialBloom, endingBloom, percentage );
            yield return null;
        }
        sunDirectionalLight.color = endingColor;
        //bloom.intensity.value = endingBloom;
    }
}
