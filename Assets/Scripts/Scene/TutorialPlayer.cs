using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialPlayer : MonoBehaviour
{
    [SerializeField]
    VideoClip[] videoClips;
    [SerializeField]
    GameObject MainCamera;
    [SerializeField]
    GameObject button;

    int currenttask;
    VideoPlayer player;

    private void Start()
    {
        player = MainCamera.GetComponent<VideoPlayer>();
        player.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        player.loopPointReached += endreached;
    }

    public void PlayVideo()
    {
        player.clip = videoClips[currenttask];
        player.Play();
        player.targetCameraAlpha = 1F;
    }

    public void SetCurrentTask(int value)
    {
        currenttask = value;

        if (currenttask > videoClips.Length)
        {
            button.SetActive(false);
        }
        else
        {
            button.SetActive(true);
        }
    }

    void endreached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.targetCameraAlpha = 0F;
    }
}
