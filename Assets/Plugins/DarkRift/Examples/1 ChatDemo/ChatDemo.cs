using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ChatDemo : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The client to communicate with the server via.")]
    UnityClient client;

    [SerializeField]
    [Tooltip("The InputField the user can type in.")]
    InputField input;

    [SerializeField]
    [Tooltip("The transform to place new messages in.")]
    Transform chatWindow;

    [SerializeField]
    [Tooltip("The scrollrect for the chat window (if present).")]
    ScrollRect scrollRect;

    [SerializeField]
    [Tooltip("The message prefab where messages will be added.")]
    GameObject messagePrefab;

    void Awake()
    {
        //Check we have a client to send/receive from
        if (client == null)
        {
            Debug.LogError("No client assigned to Chat component!");
            return;
        }

        //Subscribe to the event for when we receive messages
        client.MessageReceived += Client_MessageReceived;
    }

    private void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        //Verify the type of message is as we're expecting
        if (e.Message is SimpleMessage)
        {
            //Then cast it
            SimpleMessage msg = (SimpleMessage)e.Message;

            //Now we need to create a new UI object to put the message in so instantiate our prefab and add it 
            //as a child to the chat window
            GameObject messageObj = Instantiate(messagePrefab) as GameObject;
            messageObj.transform.SetParent(chatWindow);

            //We need the Text component so search for it
            Text text = messageObj.GetComponentInChildren<Text>();

            //If the Text component is present then get the DarkRiftReader from the message and read the text 
            //from it into the Text component
            if (text != null)
                text.text = msg.GetReader().ReadString();
            else
                Debug.LogError("Message object does not contain a Text component!");

            if (scrollRect != null)
            {
                Canvas.ForceUpdateCanvases();
                scrollRect.verticalNormalizedPosition = 0f;
            }
        }
    }

    //This will be called when the user presses enter in the input field
    public void MessageEntered()
    {
        //Check we have a client to send from
        if (client == null)
        {
            Debug.LogError("No client assigned to Chat component!");
            return;
        }

        //First we need to build a DarkRiftWriter to put the data we want to send in, it'll default to Unicode 
        //encoding so we don't need to worry about that
        DarkRiftWriter writer = new DarkRiftWriter();

        //We can then write the input text into it
        writer.Write(input.text);

        //Next we construct a message, in this case we can just use a Simple Message because there is nothing fancy
        //that needs to happen before we read the data.
        SimpleMessage message = new SimpleMessage(writer);

        //Finally we send the message to everyone connected!
        client.SendMessage(message, SendMode.Reliable);
    }
}
