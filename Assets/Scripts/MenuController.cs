using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private bool isPlaying;
    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer soundMixer;
    [SerializeField]
    private GameObject menuPanel;
    public bool settings=false;
    // Start is called before the first frame update
    void Start()
    {
        LoadParams();
        m_AudioSource = GetComponent<AudioSource>();
        isPlaying = true;
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) && isPlaying)||settings)
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(true);
                if (gameObject.transform.parent != null) 
                gameObject.transform.parent.GetComponent<MoveController>().IsPlay = false;
               // gameObject.transform.parent.gameObject.SetActive(false);
                m_AudioSource.Pause();
                isPlaying = false;
                
            }
        }

       else  if (Input.GetKeyDown(KeyCode.Escape) && !isPlaying)
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(false);
                //gameObject.transform.parent.gameObject.SetActive(true);
                m_AudioSource.Play();
                isPlaying = true;
                setIsPlay(isPlaying);
            }
        }
    }

    private void SetSettings()
    {
        settings = true;
    }

    public void ContinueClick()
    {
        menuPanel.SetActive(false);
        //gameObject.transform.parent.gameObject.SetActive(true);
        float musicVol, soundVol;
        soundMixer.GetFloat("MasterVolume", out soundVol);
        musicMixer.GetFloat("MasterVolume", out musicVol);
        SaveParams(soundVol,musicVol);
        m_AudioSource.Play();
        isPlaying = true;
        setIsPlay(isPlaying);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void OnSliderValueChangeSound(float value)
    {
        soundMixer.SetFloat("MasterVolume", Mathf.Log10(value)*20);
    }

    public void OnSliderValueChangeMusic(float value)
    {
        musicMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void SliderChange(float value)
    {

    }

    public void setIsPlay(bool val)
    {
        if (gameObject.transform.parent != null)
        {
            gameObject.transform.parent.GetComponent<MoveController>().IsPlay = val;
            gameObject.transform.parent.GetComponent<AnimController>().IsPlay = val;
            gameObject.transform.parent.GetComponent<ShootController>().IsPlay = val;
            GameObject[] massEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in massEnemy)
            {
                obj.GetComponentInChildren<PatrolController>().IsPlay = val;
            }
            GameObject[] massBullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject obj in massBullets)
            {
                obj.GetComponent<BulletController>().IsPlay = val;
            }
        }
    }

    private void SaveParams(float soundVol, float musicVol)
    {
        PlayerPrefs.SetFloat("SoundVol", soundVol);
        PlayerPrefs.SetFloat("MusicVol", musicVol);
        PlayerPrefs.Save();
    }

    private void LoadParams()
    {
        if (PlayerPrefs.HasKey("SoundVol"))
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("SoundVol")) * 20);
        else
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(0) * 20);
        if (PlayerPrefs.HasKey("MusicVol"))
            musicMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVol")) * 20);
        else
            musicMixer.SetFloat("MasterVolume", Mathf.Log10(0) * 20);
    }


}
