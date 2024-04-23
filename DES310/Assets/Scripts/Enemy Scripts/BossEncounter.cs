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
    [SerializeField] EventHandler eventHandler;
    [SerializeField] Vector2 bossSpawnPos;
    [SerializeField] GameObject pauseMenu;

    float amplitude = 1.0f;
    float frequency = 100.0f;
    float cameraTimer = 0;

    void Update()
    {
        if (!pauseMenu.activeSelf)
        {
            if (boss.activeSelf)
            {
                Time.timeScale = 0;
                eventHandler.TimescaleFreeze.Invoke(true);

                // Increase Shake
                if (cameraTimer <= 2)
                {
                    amplitude = cameraTimer / 2f;
                    bossCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
                    bossCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
                }
                else if (cameraTimer > 2 && cameraTimer < 5)
                {
                    amplitude = 1 - ((cameraTimer - 4f) / (5f - 4f));
                    bossCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
                    bossCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
                }

                // Force Position
                if (cameraTimer < 5)
                {
                    boss.transform.position = bossSpawnPos;
                    boss.transform.rotation = Quaternion.Euler(0, 0, -90);
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
                eventHandler.TimescaleFreeze.Invoke(false);
                bossHealthSlider.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private void Awake()
    {
        boss.transform.position = bossSpawnPos;
    }

    private void OnEnable()
    {
        cameraTimer = 0;

        if (bossCam.activeSelf)
        {
            amplitude = bossCam.GetComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain;
            frequency = bossCam.GetComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set Boss Active
            if (boss)
            {
                boss.SetActive(true);

                SoundManager.instance.PlaySound(SoundManager.SFX.BossScream, boss.transform, 1f);

                // Close Door
                door.Lock();
                door.Close();
            }

            // Camera Sequence
            if (cameraTimer == 0)
            {
                bossCam.SetActive(true);
                mainCam.GetComponent<CinemachineBrain>().m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;

                boss.transform.position = bossSpawnPos;
                Time.timeScale = 0f;
                eventHandler.TimescaleFreeze.Invoke(true);
            }
        }
    }
}
