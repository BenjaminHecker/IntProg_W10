using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatGPTWrapper;
using TMPro;

public class BotChatManager : MonoBehaviour
{
    [SerializeField] private ChatGPTConversation chatGPT;
    [SerializeField] private TMP_InputField playerInput;
    [SerializeField] private TextMeshProUGUI txt_AiReply;

    //[SerializeField] private NPCController npcController;

    private string botName = "Other";
    private string playerName = "Player";

    private void Awake()
    {
        chatGPT.Init();
    }

    private void Start()
    {
        chatGPT.SendToChatGPT("{\"player_said\":\"Hello\"}");
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

            BotJsonReceiver botJSON = JsonUtility.FromJson<BotJsonReceiver>(message);
            talkLine = "<color=" + botJSON.text_color + ">" + botJSON.reply_to_player.Replace("\\\\", "\\") + "</color>";

            Color backgroundColor;
            if (ColorUtility.TryParseHtmlString(botJSON.background_color, out backgroundColor))
                Camera.main.backgroundColor = backgroundColor;

            Debug.Log("text_color: " + botJSON.text_color + ", background_color: " + botJSON.background_color);

            //npcController.ShowAnimation(npcJSON.animation_name);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            talkLine = "ignoring your response";
        }

        txt_AiReply.text = "<color=#ff7082>" + botName + ": </color> " + talkLine;
    }
}
