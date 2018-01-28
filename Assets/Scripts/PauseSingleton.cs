using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSingleton : MonoBehaviour {

    [SerializeField]
    private Image pausePanel;
    public Text pauseText;

    [SerializeField]
    private Image deathPanel;

    [HideInInspector]
    public static bool onPause = false;
    public static bool onDeath = false;

    private void Start()
    {
        onDeath = false;
        onPause = false;

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (onDeath && onPause)
            return;

        if (onDeath && !onPause)
        {
            Invoke("DeathPanelShow", 5);
            onPause = true;
            return; 
        }

        if (onPause)
        {
            Color alpha = pauseText.color;
            alpha.a = Mathf.PingPong(Time.unscaledTime * 0.5f, 1);
            pauseText.color = alpha;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (!onPause)
        {
            Time.timeScale = 0;
            onPause = true;

            pausePanel.gameObject.SetActive(true);

            float r = Random.Range(0f, 1f);
            if (r < 0.8)
            {
                pauseText.text = "Pause";
            }
            else if (r < 0.95)
            {
                pauseText.text = "Who's there?";
            }
            else if (r <= 1)
            {
                pauseText.text = "Mommy...";
            }

        }
        else
        {
            Time.timeScale = 1;
            onPause = false;

            pausePanel.gameObject.SetActive(false);
        }
    }

    public void DeathPanelShow()
    {
        deathPanel.gameObject.SetActive(true);
    }
}
