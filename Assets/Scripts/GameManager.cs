using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatGPTWrapper;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private ChatGPTConversation chatGPT;
    [SerializeField] private TMP_InputField playerInput;
    [SerializeField] private TextMeshProUGUI txt_AiReply;

    [SerializeField] private NPCController npcController;

    private string npcName = "Coco";
    private string playerName = "Player";

    private void Awake()
    {
        if (instance == null)
            instance = this;

        chatGPT.Init();
    }

    private void Start()
    {
        chatGPT.SendToChatGPT("{\"player_said\":\"Hello!\"}");
    }

    private void Update()
    {
        if (Input.GetButtonUp("Submit"))
            SubmitChatMessage();
    }

    public void SubmitChatMessage()
    {
        if (playerInput.text != "")
        {
            chatGPT.SendToChatGPT("{\"player_said\":\"" + playerInput.text + "\"}");
            playerInput.text = "";
        }
    }

    public void ReceiveChatGPTReply(string message)
    {
        Debug.Log(message);
        txt_AiReply.text = message;

        string talkLine;
        try
        {
            if (!message.EndsWith("}"))
            {
                if (message.Contains("}"))
                {
                    message = message.Substring(0, message.LastIndexOf("}") + 1);
                }
                else
                {
                    message += "}";
                }
            }

            if (message.Contains("\\"))
            {
                message = message.Replace("\\", "\\\\");
            }

            NPCJsonReceiver npcJSON = JsonUtility.FromJson<NPCJsonReceiver>(message);
            talkLine = npcJSON.reply_to_player.Replace("\\\\", "\\");

            npcController.ShowAnimation(npcJSON.animation_name);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            talkLine = "Don't say that!";
        }

        txt_AiReply.text = "<color=#ff7082>" + npcName + ": </color> " + talkLine;
    }
}
