using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoplay : MonoBehaviour
{

    //map
    public Image bg;

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

            Play();

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

    IEnumerator play()
    {
        transform.SetAsLastSibling();

        bg.enabled = false;

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        Debug.Log("Playing Video");

        while (videoPlayer.isPlaying)
        {
            //Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        transform.SetAsFirstSibling();

        Debug.Log("Done Playing Video");
    }

    public void CloseVideo() {

        bg.enabled = true;

        //Play Video
        videoPlayer.Stop();

        //Play Sound
        audioSource.Stop();

        Debug.Log("Video stopped.");

    }
}