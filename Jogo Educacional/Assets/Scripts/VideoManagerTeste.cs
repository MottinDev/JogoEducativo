using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections.Generic;

public class VideoManagerTeste : MonoBehaviour
{
    public VideoPlayer videoPlayer;     // VideoPlayer component to play videos
    public VideosSO videoClip;          // The current VideosSO object containing the video and options
    public Button[] optionsButton;      // Array of buttons for player choices
    public Button nextButton;           // Botão para pular para o próximo vídeo (apenas para teste)

    void Start()
    {
        // Validate that videoPlayer is assigned
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer is not assigned in the inspector.");
            return;
        }

        // Validate that videoClip is assigned
        if (videoClip == null)
        {
            Debug.LogError("VideoClip (VideosSO) is not assigned in the inspector.");
            return;
        }

        // Add listener for when the video finishes playing
        videoPlayer.loopPointReached += OnVideoFinished;

        // Disable options buttons at the start
        if (optionsButton != null && optionsButton.Length > 0)
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

        // Play the initial video
        PlayCurrentVideo();
    }

    // Plays the current video
    void PlayCurrentVideo()
    {
        if (videoClip != null && videoClip.videoClip != null)
        {
            videoPlayer.clip = videoClip.videoClip;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoClip or its videoClip property is null.");
        }
    }

    // Called when the video finishes playing
    void OnVideoFinished(VideoPlayer vp)
    {
        if (videoClip == null)
        {
            Debug.LogError("VideoClip is null.");
            return;
        }

        if (videoClip.options != null && videoClip.options.Count > 0)
        {
            if (videoClip.options.Count == 1)
            {
                // Only one option; automatically play the next video
                videoClip = videoClip.options[0];
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

        if (videoClip == null || videoClip.options == null)
        {
            Debug.LogError("VideoClip or its options are null.");
            return;
        }

        // Enable options buttons and assign appropriate video clips
        for (int i = 0; i < optionsButton.Length; i++)
        {
            if (i < videoClip.options.Count)
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
        if (videoClip != null && videoClip.options != null)
        {
            if (optionIndex >= 0 && optionIndex < videoClip.options.Count)
            {
                videoClip = videoClip.options[optionIndex];
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
