using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEncounter : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] HorizontalDoor door;
    [SerializeField] Slider bossHealthSlider;

    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject bossCam;
    [SerializeField] GameObject mainCam;

    float amplitude = 1.0f;
    float maxAmplitude = 1.0f;
    float frequency = 1.0f;
    float cameraTimer = 0;

    void Update()
    {
        if (boss.activeSelf)
        {
            // Increase Shake
            if (cameraTimer <= 2)
            {
                amplitude = cameraTimer / 2f;
            }
            else if (cameraTimer > 2 && cameraTimer < 5)
            {
                amplitude = 1 - ((cameraTimer - 4f) / (5f - 4f));
            }
            else
            {
                amplitude = 0;
            }

            cameraTimer += Time.unscaledDeltaTime;
        }

        if (!boss)
        {
            door.Unlock();
        }

        if (cameraTimer >= 4)
        {
            bossCam.SetActive(false);
        }

        if (cameraTimer >= 5) 
        {
            mainCam.GetComponent<CinemachineBrain>().m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
            Time.timeScale = 1f;
            bossHealthSlider.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (bossCam.activeSelf)
        {
            amplitude = bossCam.GetComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain;
            frequency = bossCam.GetComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain;
        }
        cameraTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set Boss Active
            if (boss)
            {
                boss.SetActive(true);

                // Close Door
                door.Lock();
                door.Close();
            }

            // Camera Sequence
            if (cameraTimer == 0)
            {
                bossCam.SetActive(true);
                mainCam.GetComponent<CinemachineBrain>().m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;

                Time.timeScale = 0f;
            }
        }
    }
}
