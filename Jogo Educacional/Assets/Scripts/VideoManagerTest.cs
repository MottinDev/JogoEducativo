using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using System;
using Unity.Mathematics;

public class VideoManagerTest : MonoBehaviour
{
    //public AudioSource audioSource;
    public VideoPlayer[] videoPlayers; //videoPlayers
    public VideosSO videoSO;          // The current VideosSO object containing the video and options
    public Button[] optionsButton;      // Array of buttons for player choices
    public Button nextButton;           // Botão para pular para o próximo vídeo (apenas para teste)
    //public Button prevButton;
    //public Button restartButton;
    //public Button unPauseButton;
    public Button nextPhaseButton;
    //public Image legenda;
    public int vpIndex = 0; 


    [SerializeField] ManagerUI managerUI;

    //public bool pause;

    void Start()
    {
        // Validate that videoPlayer is assigned
        if (videoPlayers == null)
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
        for (int i = 0; i < videoPlayers.Length; i++)
        {
            videoPlayers[i].loopPointReached += OnVideoFinished;
        }

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

        //if(prevButton != null)
        //{
        //    prevButton.onClick.AddListener(PrevToNextVideo);
        //}

        //if(restartButton != null)
        //{
        //    restartButton.onClick.AddListener(RestartVideo);
        //}

        //if(unPauseButton != null) unPauseButton.onClick.AddListener(PlayCurrentVideo);

        if(nextPhaseButton != null) nextPhaseButton.onClick.AddListener(NextPhaseButton);

        // Play the initial video
        vpIndex = 0;
        videoPlayers[vpIndex].clip = videoSO.videoClip;
        videoPlayers[vpIndex].Prepare();
        PrepareVideoPlayers();
        //-----------------------------
    }

    void Switch()
    {
        if (vpIndex == 0)
        {
            vpIndex = 1;
        }
        else
        {
            vpIndex = 0;
        }
    }


    void NextPhaseButton()
    {
        if(videoSO.videoFinalBom) managerUI.ProximaFase();
        if(videoSO.videoFinalRuim) managerUI.ReiniciarFase();
    }

    void RestartVideo()
    {
        //videoPlayer.Stop();
        //videoPlayer.time = 0;
        //PlayCurrentVideo();
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

    void PrepareVideoPlayers()
    {
        Debug.Log("preparando vpindex: " + vpIndex);
        //videoPlayers[vpIndex].clip = videoSO.videoClip;
        //videoPlayers[vpIndex].Prepare();
        videoPlayers[vpIndex].gameObject.transform.SetLocalPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);

        if (videoSO.options != null && videoSO.options.Count > 0)
        {
            Debug.Log("preparando demais videoPlayers");
            nextPhaseButton.gameObject.SetActive(false);

            int optionIndex = 0;
            for (int i = 0; i < videoPlayers.Length; i++)
            {
                if (i == vpIndex) { continue; }

                videoPlayers[i].gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -1005), Quaternion.identity);

                if (optionIndex < videoSO.options.Count)
                {
                        videoPlayers[i].gameObject.SetActive(true);
                        videoPlayers[i].clip = videoSO.options[optionIndex].videoClip;
                        videoPlayers[i].Prepare();
                        videoPlayers[i].time = 0.0f;
                        videoPlayers[i].Pause();
                    
                }
                else
                {
                    videoPlayers[i].gameObject.SetActive(false);
                }
                optionIndex++;
            }
            PlayCurrentVideo();
        }
        else
        {
            // Multiple options; display buttons for player interaction
            PlayCurrentVideo();
            //DisplayOptions();
        }



    }
    void PlayCurrentVideo()
    {
        Debug.Log("Play vpindex: " + vpIndex);

        if (videoSO.videoLoop)
        {
            videoPlayers[vpIndex].isLooping = true;
        }
        else
        {
            videoPlayers[vpIndex].isLooping = false;
        }

        if(videoSO.videoFinalBom || videoSO.videoFinalRuim)
        {
            nextPhaseButton.gameObject.SetActive(true);
        }
        else
        {
            nextPhaseButton.gameObject.SetActive(false);
        }

        if (videoPlayers[vpIndex].clip != null)
        {
            videoPlayers[vpIndex].Play();
        }
        else
        {
            Debug.LogError("Videoplayer.Clip is null.");
        }

        if (videoSO.options != null && videoSO.options.Count > 1)
        {
            DisplayOptions();
        }
    }   

    // Called when the video finishes playing
    void OnVideoFinished(VideoPlayer vp)
    {
        if (videoSO.videoLoop) return;

        if (videoSO == null)
        {
            Debug.LogError("VideoSO is null.");
            return;
        }

        if(videoSO.options == null)
        {
            Debug.Log("VideoSO not have options");
            return;
        }

        

        Switch();

        vp.Stop();
        vp.clip = null;
        vp.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -1005), Quaternion.identity);

        if (videoSO.options[0] != null)
        {
            videoSO = videoSO.options[0];
        }
        
        PrepareVideoPlayers();
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
                    optionsButton[i].onClick.RemoveAllListeners();
                    int index = i;
                    optionsButton[i].onClick.AddListener(() => PlayNextVideo(index));
                    Debug.Log("i index: " + i);
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
        Debug.Log("PlayNextVideo, optionBtnIndex: " + optionIndex + " i index: ");
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

                if (optionIndex >= vpIndex) optionIndex++;//correção de index
                vpIndex = optionIndex;
                //PlayCurrentVideo(videoPlayer);
                //videoPlayers[vpIndex].clip = videoSO.videoClip;
                //videoPlayers[vpIndex].Prepare();
                PrepareVideoPlayers();

            }
            else
            {
                Debug.LogError("Invalid option index selected: " + optionIndex);
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
        // PlayCurrentVideo(videoPlayer);
        //videoPlayer.clip = videoSO.videoClip;
        //videoPlayer.Prepare();
    }

    // Skip to the next video without waiting for it to end (testing only)
    public void SkipToNextVideo()
    {
        if (videoPlayers[vpIndex].isPlaying)
        {
            //videoPlayer.Stop();
            OnVideoFinished(videoPlayers[vpIndex]);  // Simulate the video finishing to advance to the next one
        }
    }
}
