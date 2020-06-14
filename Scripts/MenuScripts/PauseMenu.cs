using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioListener audio;
    [SerializeField] private GameObject player;
    public static bool paused = false;
    private void Start()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            this.Pause();
        if((paused == false)&&(PlayerInventory.inInventory == false)&&(player.GetComponent<PlayerHealth>().GetPlayerHealth() > 0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    private void Pause()
    {
        if(paused == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.pause=true;
            //audio.enabled = false;
           
        }
        else if(paused==true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            AudioListener.pause = false;
            //audio.enabled =true;
            
        }
    }
    public void Resume()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        audio.enabled = true;
        AudioListener.pause = false;
    }
    public void Exit()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
        
    }
}
