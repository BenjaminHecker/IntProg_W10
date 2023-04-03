using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private SkinnedMeshRenderer faceBlendShape;

    private int blinking = 0;
    private float blinkingValue = 0;
    private float blinkingTimer = 0f;
    private float blinkingTimerTotal = 3.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        blinkingTimer += Time.deltaTime;
        if (blinking == 0 && (Random.value < 0.001f || blinkingTimer > blinkingTimerTotal))
        {
            blinkingTimer = 0;
            blinkingTimerTotal = Random.Range(1.1f, 5.01f);
            blinking = 1;
            blinkingValue = 0;
        }
        else if (blinking == 1)
        {
            blinkingValue += Time.deltaTime * 1000;

            if (blinkingValue > 100)
            {
                blinking = 2;
                faceBlendShape.SetBlendShapeWeight(35, 100);
            }
            else
            {
                faceBlendShape.SetBlendShapeWeight(35, blinkingValue);
            }
        }
        else if (blinking == 2)
        {
            blinkingValue -= Time.deltaTime * 600;

            if (blinkingValue < 0)
            {
                blinking = 0;
                faceBlendShape.SetBlendShapeWeight(35, 0);
            }
            else
            {
                faceBlendShape.SetBlendShapeWeight(35, blinkingValue);
            }
        }
    }

    public void ShowAnimation(string animID)
    {
        for (int i = 0; i < 60; i++)
        {
            if (i == 35) continue;

            faceBlendShape.SetBlendShapeWeight(i, 0);
        }

        if (animID == "idle")
        {
            float rand = Random.value;

            if (rand < 0.3f)
                anim.SetTrigger("idle1");
            else if (rand < 0.6f)
                anim.SetTrigger("idle2");
            else
                anim.SetTrigger("idle3");

            if (Random.value < 0.5f)
                faceBlendShape.SetBlendShapeWeight(9, 100);
            else
                faceBlendShape.SetBlendShapeWeight(24, 67);
        }
        else if (animID == "shy")
        {
            anim.SetTrigger("shy");
        }
        else if (animID == "confused")
        {
            anim.SetTrigger("confused");
            faceBlendShape.SetBlendShapeWeight(32, 100);
        }
        else if (animID == "joking")
        {
            anim.SetTrigger("joking");
            faceBlendShape.SetBlendShapeWeight(33, 100);
        }
        else if (animID == "worried")
        {
            anim.SetTrigger("worried");
            faceBlendShape.SetBlendShapeWeight(52, 100);
        }
        else if (animID == "focus")
        {
            anim.SetTrigger("focus");
            faceBlendShape.SetBlendShapeWeight(50, 100);
        }
        else if (animID == "surprise")
        {
            anim.SetTrigger("surprise");
            faceBlendShape.SetBlendShapeWeight(53, 100);
        }
        else if (animID == "angry")
        {
            anim.SetTrigger("angry");
            faceBlendShape.SetBlendShapeWeight(49, 100);
        }
        else if (animID == "cheers")
        {
            anim.SetTrigger("cheers");
            faceBlendShape.SetBlendShapeWeight(24, 100);
        }
        else if (animID == "nod")
        {
            anim.SetTrigger("nod");
            faceBlendShape.SetBlendShapeWeight(19, 100);
        }
        else if (animID == "waving_arm")
        {
            anim.SetTrigger("waving_arm");
            faceBlendShape.SetBlendShapeWeight(24, 100);
        }
        else if (animID == "proud")
        {
            anim.SetTrigger("proud");
            faceBlendShape.SetBlendShapeWeight(24, 100);
        }
    }
}
