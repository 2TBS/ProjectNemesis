using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The client to communicate with the server via.")]
    UnityClient client;

    [SerializeField]
    [Tooltip("The ID to identify this object across the network using")]
    byte dragID;

    [SerializeField]
    [Tooltip("The speed at which the object will drag at.")]
    float speed = 15;
    
    /// <summary>
    ///     This will be an object used to smoothly move between positions.
    /// </summary>
    Vector3 targetPosition;

    void Awake ()
    {
        //Check we have a client to send/receive from
        if (client == null)
        {
            Debug.LogError("No client assigned to DragObject!");
            return;
        }

        //Subscribe to the event for when we receive messages
        client.MessageReceived += Client_MessageReceived;

        //Set our default target position to our current position
        targetPosition = transform.position;
	}

    void Update()
    {
        //Lerp between positions to create a smoother transition
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        TagSubjectMessage message = e.Message as TagSubjectMessage;

        //Check the message is a TagSubject message and has our ID as the tag
        if (message != null && message.Tag == dragID)
        {
            //Get the reader from the message so we can read the data
            DarkRiftReader reader = message.GetReader();

            //And update our position!
            targetPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), 0);
        }
    }
	
    //Called when the object is dragged by the mouse
	void OnMouseDrag ()
    {
        //Check we have a client to send from
        if (client == null)
        {
            Debug.LogError("No client assigned to DragObject!");
            return;
        }

        //Firstly we need to work out where the object should be, we can ignore the z-coord returned
        Vector3 newPos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10);

        //We want to send the new position of the object to the other clients so we write the position 
        //into a DarkRiftWriter as x, y and z components
        DarkRiftWriter writer = new DarkRiftWriter();
        writer.Write(newPos.x);
        writer.Write(newPos.y);

        //Then we'll create a new TagSubject message and put the DarkRiftWriter into it.
        //The tag and subject indicate what the message is about so we'll put a tag of '0' to indicate a
        //movement and set the subject to the ID of this object so that we can easily identify it later.
        Message message = new TagSubjectMessage(dragID, 0, writer);

        //We can then send the message
        client.SendMessage(message, SendMode.Unreliable);    //TODO 1 will this be a problem without others as an intention?

        //Last but not least we'll actually move the object on our screen so set the target position to 
        //the new position
        targetPosition = new Vector3(newPos.x, newPos.y, 0);
	}
}
