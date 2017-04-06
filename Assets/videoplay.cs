using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoplay : MonoBehaviour
{

    //map
    public Image bg;

    public GameObject panelButton;
    public GameObject panelText;

    public GameObject videoPanel;
    public GameObject videoButton;

    //Raw Image to Show Video Images [Assign from the Editor]
    public RawImage image;
    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    //is this selected by the arrow keys?
    public bool isSelected;

    //Audio
    private AudioSource audioSource;

    // Use this for initialization

    void Update() {

        if (Input.GetKeyDown("return") && isSelected == true) {

            ShowPanel();

        }

    }
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(prepareVideo());
    }

    IEnumerator prepareVideo()
    {
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.VideoClip;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;
    }

    public void Play()
    {
        StartCoroutine(play());
    }

    public void ShowPanel() {
        videoButton.SetActive(true);
        videoPanel.SetActive(true);

    }

    IEnumerator play()
    {
        transform.SetAsLastSibling();

        bg.enabled = false;

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        panelText.SetActive(false);
        panelButton.SetActive(false);

        Debug.Log("Playing Video");

        while (videoPlayer.isPlaying)
        {
            //Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }
        if (videoPlayer.isPlaying == false) {
            
            CloseVideo();

        }

        transform.SetAsFirstSibling();

        videoButton.SetActive(true);
        videoButton.transform.SetAsLastSibling();

        Debug.Log("Done Playing Video");
    }

    public void CloseVideo() {

        bg.enabled = true;

        videoPanel.SetActive(false);

        videoButton.SetActive(false);

        panelButton.SetActive(true);
        
        panelText.SetActive(true);

        //Play Video
        videoPlayer.Stop();

        //Play Sound
        audioSource.Stop();

        Debug.Log("Video stopped.");

    }
}