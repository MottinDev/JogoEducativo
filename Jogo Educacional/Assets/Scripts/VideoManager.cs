using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;
//using UnityEngine.UIElements;

public class VideoManager : MonoBehaviour
{
    public AudioSource audioSource;
    public VideoPlayer videoPlayer;     // VideoPlayer component to play videos
    public VideosSO videoSO;          // The current VideosSO object containing the video and options
    public Button[] optionsButton;      // Array of buttons for player choices
    public Button nextButton;           // Botão para pular para o próximo vídeo (apenas para teste)
    public Button prevButton;
    public Button restartButton;
    public Button unPauseButton;
    public Image legenda;
    public Image result;

    public bool pause;

    void Start()
    {
        // Validate that videoPlayer is assigned
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer is not assigned in the inspector.");
            return;
        }

        // Validate that videoClip is assigned
        if (videoSO == null)
        {
            Debug.LogError("VideoClip (VideosSO) is not assigned in the inspector.");
            return;
        }

        // Add listener for when the video finishes playing
        videoPlayer.loopPointReached += OnVideoFinished;

        // Disable options buttons at the start
        if (optionsButton != null && optionsButton.Length > 0)
        {
            ShowButtons();
        }
        else
        {
            Debug.LogError("OptionsButton array is not assigned or is empty in the inspector.");
        }

        // Configure Next button for testing
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(SkipToNextVideo);
        }
        else
        {
            Debug.LogError("NextButton is not assigned in the inspector.");
        }

        if(prevButton != null)
        {
            prevButton.onClick.AddListener(PrevToNextVideo);
        }

        if(restartButton != null)
        {
            restartButton.onClick.AddListener(RestartVideo);
        }

        if(unPauseButton != null) unPauseButton.onClick.AddListener(PlayCurrentVideo);

        // Play the initial video
        PlayCurrentVideo();

        //-----------------------------
        
    }

    void RestartVideo()
    {
        videoPlayer.Stop();
        videoPlayer.time = 0;
        PlayCurrentVideo();
    }
    void ShowButtons()
    {
        foreach (Button btn in optionsButton)
        {
            if (btn != null)
            {
                btn.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("One of the buttons in optionsButton array is null.");
            }
        }
    }

    void ShowSubtitle()
    {
        if ( videoSO.legenda == null)
        {
            legenda.enabled = false;
            return;   
        }
        legenda.enabled = true;
        legenda.sprite = videoSO.legenda;
    }

    void ShowResult()
    {
        result.sprite = videoSO.resultado;
        result.gameObject.SetActive(true);
    }

    void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void StopAudio()
    {
        audioSource.Stop();
    }

    // Plays the current video
    void PlayCurrentVideo()
    {
        new WaitForSeconds(1);

        unPauseButton.gameObject.SetActive(false);

        if (videoSO.videoLoop)
        {
            videoPlayer.isLooping = true;
        }
        else
        {
            videoPlayer.isLooping = false;
        }

        if (videoSO != null && videoSO.videoClip != null)
        {
            videoPlayer.clip = videoSO.videoClip;
            videoPlayer.Play();
            ShowSubtitle();
            if (videoSO.audioClip != null)
            {
                PlayAudio(videoSO.audioClip);
            }
        }
        else
        {
            Debug.LogError("VideoClip or its videoClip property is null.");
        }

        if (videoSO.options != null && videoSO.options.Count > 1)
        {
            DisplayOptions();
        }

        StartCoroutine(WaitToPause());
    }

    IEnumerator WaitToPause()
    {
        bool value = true;
        while (value)
        {
            if (pause && videoPlayer.isPlaying && videoPlayer.isPrepared)
            {
                unPauseButton.gameObject.SetActive(true);
                videoPlayer.Stop();
                pause = false;

                value = false;
            }

            yield return null;
        }
    }

        

    // Called when the video finishes playing
    void OnVideoFinished(VideoPlayer vp)
    {
        if(videoSO.videoLoop) return;

        if (videoSO == null)
        {
            Debug.LogError("VideoClip is null.");
            return;
        }

        if (videoSO.options != null && videoSO.options.Count > 0)
        {
            if (videoSO.options.Count == 1)
            {
                // Only one option; automatically play the next video
                videoSO = videoSO.options[0];
                pause = videoSO.videoPause;
                PlayCurrentVideo();
            }
            else
            {
                // Multiple options; display buttons for player interaction
                DisplayOptions();
            }
        }
        else
        {
            if (videoSO.resultado != null)
            {
                ShowResult();
            }
            else
            {
                result.gameObject.SetActive(false);
            }
            // No more videos; end of sequence
            Debug.Log("End of video sequence.");
        }
    }

    // Displays option buttons for player choices
    void DisplayOptions()
    {
        if (optionsButton == null || optionsButton.Length == 0)
        {
            Debug.LogError("OptionsButton array is not assigned or is empty.");
            return;
        }

        if (videoSO == null || videoSO.options == null)
        {
            Debug.LogError("VideoClip or its options are null.");
            return;
        }

        // Enable options buttons and assign appropriate video clips
        for (int i = 0; i < optionsButton.Length; i++)
        {
            if (i < videoSO.options.Count)
            {
                if (optionsButton[i] != null)
                {
                    optionsButton[i].gameObject.SetActive(true);
                    int index = i; // Capture index for closure in listener
                    optionsButton[i].onClick.RemoveAllListeners();
                    optionsButton[i].onClick.AddListener(() => PlayNextVideo(index));
                    // Optionally, set button text or image to represent the choice
                    // optionsButton[i].GetComponentInChildren<Text>().text = videoClip.options[i].optionName;
                }
                else
                {
                    Debug.LogWarning($"optionsButton[{i}] is null.");
                }
            }
            else
            {
                if (optionsButton[i] != null)
                {
                    optionsButton[i].gameObject.SetActive(false);
                }
            }
        }
    }

    // Plays the next video based on player choice
    public void PlayNextVideo(int optionIndex)
    {
        // Hide options buttons
        if (optionsButton != null)
        {
            foreach (Button btn in optionsButton)
            {
                if (btn != null)
                {
                    btn.gameObject.SetActive(false);
                }
            }
        }

        // Validate option index and play the selected video
        if (videoSO != null && videoSO.options != null)
        {
            if (optionIndex >= 0 && optionIndex < videoSO.options.Count)
            {
                videoSO = videoSO.options[optionIndex];
                PlayCurrentVideo();
            }
            else
            {
                Debug.LogError("Invalid option index selected.");
            }
        }
        else
        {
            Debug.LogError("VideoClip or its options are null.");
        }
    }

    public void PrevToNextVideo()
    {
        if (videoSO.prevVideo == null) return;

        videoSO = videoSO.prevVideo;
        PlayCurrentVideo();
    }

    // Skip to the next video without waiting for it to end (testing only)
    public void SkipToNextVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            OnVideoFinished(videoPlayer);  // Simulate the video finishing to advance to the next one
        }
    }
}
