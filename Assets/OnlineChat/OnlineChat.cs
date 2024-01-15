using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class OnlineChat : MonoBehaviour
{
    [SerializeField] private GameObject chatPanel;
	[Header("Send")]
	[SerializeField] private string chatSQL;
    [SerializeField] private TMP_InputField letterText;
    public string senderName;
    [SerializeField] private string lang;

    [Header("Receive")]
	[SerializeField] private string chatReceiveSQL;
	[SerializeField] private TextMeshProUGUI chatText;
    [SerializeField] private string linesCount;
	
	string[] result;
    IEnumerator chatCoroutine;

	void Start()
	{
		chatCoroutine = UpdateText();
	}

    public void OpenChat()
    {
        chatPanel.SetActive(true);
        StartCoroutine(chatCoroutine);
    }

    public void CloseChat()
    {
        chatPanel.SetActive(false);
        StopCoroutine(chatCoroutine);
    }

    public void SendLetter()
    {
    	if (letterText.text != null || letterText.text != "")
    	{
    		StartCoroutine(GetRequest($"https://adopt-geekplay.com/scripts/{chatSQL}.php?sender={senderName}&letter={letterText.text}&lang={lang}&lines={linesCount}", false));
    		letterText.text = "";
    	}
    }

    IEnumerator GetRequest(string uri, bool receive)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string request = webRequest.downloadHandler.text;
                    //string result = request.Substring(request.IndexOf("{\""));
                    //string[] parts = result.Split("}\""); 
                    //parts[0] += "}";
                    Debug.Log(request); //JSON С ССОХРАНЕНИЯМИ

                    if (receive)
                    {
                    	//обработка строки
                    	chatText.text = "";
                    	result = request.Split(" \"");
                    	int count = 0;
                    	for (int i = result.Length - 1; i > 0; i--)
                    	{
                    		if (i % 2 != 0)
                    		{
                    			count += 1;
                    			//chatText.text += result[i].Substring(0, result[i].IndexOf("\""));
                    			if (count % 2 == 0)
                    			{
                    				chatText.text += result[i+2].Substring(0, result[i+2].IndexOf("\""));
                    				chatText.text += "\n";
                    			}
                    			else
                    			{
                    				chatText.text += result[i-2].Substring(0, result[i-2].IndexOf("\""));
                    				chatText.text += ": ";
                    			}
                    		}
                    	}
                    	//chatText.text = result2;
                    }
                    break;
            }
        }
    }

    IEnumerator UpdateText()
    {
    	while (true)
    	{
    		yield return new WaitForSeconds(2);
    		StartCoroutine(GetRequest($"https://adopt-geekplay.com/scripts/{chatReceiveSQL}.php?lang={lang}&lines={linesCount}", true));
    	}
    }
}
