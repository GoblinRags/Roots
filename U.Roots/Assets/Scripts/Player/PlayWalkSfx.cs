using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWalkSfx : MonoBehaviour
{
    public void Walk()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sound.TwoSteps, 1f);
    }
}
