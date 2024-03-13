using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] Text gameOverText;
    [SerializeField] Text continueText;
    [SerializeField] Canvas canvas;

    [SerializeField] EventHandler eventHandler;
    [SerializeField] GameObject musicManager;
    InputManager inputManager;

    float timer = 3;

    void Start()
    {
        eventHandler.PlayerDeath.AddListener(OnPlayerDeath);

        inputManager = GetComponent<InputManager>();
        canvas.enabled = false;
        timer = 2;
    }

    void Update()
    {
        if (canvas.isActiveAndEnabled)
        {
            if (timer < 0)
            {
                continueText.color = Color.white;

                if (inputManager.GetButtonDown("Interact"))
                {
                    Time.timeScale = 1;
                    musicManager.GetComponent<AudioSource>().mute = false;
                    continueText.text = "";
                    SceneManager.LoadScene("Main Hub");
                }
            }

            // Fade in
            if (timer > 0)
            {
                image.color = Color.Lerp(new Color(50, 50, 50, 1), new Color(50, 50, 50, 0), timer / 2);
                gameOverText.color = Color.Lerp(new Color(1, 0, 0, 1), new Color(1, 0, 0, 0), timer / 2);
                continueText.color = Color.clear;

                timer -= Time.unscaledDeltaTime;
                Debug.Log(timer);
            }
        }
    }

    void OnPlayerDeath()
    {
        musicManager.GetComponent<AudioSource>().mute = true;
        Time.timeScale = 0.0f;
        canvas.enabled = true;
    }
}
