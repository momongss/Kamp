using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.Monetization;

public class TimeManager : MonoBehaviour
{
    public Material skybox_mat_day;
    public Material skybox_mat_night;
    public Light main_light;

    public float currentTime_Hour = 12f;
    public float darkStartTime_Hour = 14f;
    public float campingEndTime_Hour = 15f;

    public float timeScale = 10f;

    public float darkrate_ambientLight = 0.001f;
    public float darkrate_exposure = 0.001f;
    public float darkrate_light = 0.001f;

    public Color32 ambientLight_day;
    public Color32 ambientLight_night;

    public enum State { Day, Night, End }
    public State state = State.Day;

    private void Start()
    {
        RenderSettings.skybox = skybox_mat_day;
        RenderSettings.ambientLight = ambientLight_day;

        main_light.intensity = 1f;
        skybox_mat_day.SetFloat("_Exposure", 0.75f);
    }

    public void StartNightCamp()
    {
        RenderSettings.skybox = skybox_mat_night;
        // RenderSettings.ambientLight = ambientLight_night;
    }

    public void ChangeState(State newState)
    {
        switch (newState)
        {
            case State.Day:

                break;

            case State.Night:
                UINotice_NightStart.I.ShowNotice();
                break;
            case State.End:

                break;
        }

        state = newState;
    }

    private void Update()
    {
        currentTime_Hour += (Time.deltaTime * timeScale) * 0.001f;

        switch (state)
        {
            case State.Day:
                if (currentTime_Hour >= darkStartTime_Hour)
                {
                    RenderSettings.ambientLight = new Color(
                        RenderSettings.ambientLight.r - darkrate_ambientLight,
                        RenderSettings.ambientLight.g - darkrate_ambientLight,
                        RenderSettings.ambientLight.b - darkrate_ambientLight);

                    float exposure = skybox_mat_day.GetFloat("_Exposure");
                    skybox_mat_day.SetFloat("_Exposure", exposure - darkrate_exposure * Time.deltaTime);
                    main_light.intensity -= darkrate_light * Time.deltaTime;

                    if (main_light.intensity <= 0.2f)
                    {
                        ChangeState(State.Night);
                    }
                }
                break;

            case State.Night:
                if (currentTime_Hour >= campingEndTime_Hour)
                {
                    ChangeState(State.End);
                }
                break;
        }
    }

}
