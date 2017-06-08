using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;

internal class BlockCharacter : MonoBehaviour
{
    const byte MOVEMENT_TAG = 1;

    /// <summary>
    ///     The DarkRift client to send data though.
    /// </summary>
    UnityClient client;

    /// <summary>
    ///     The ID of this player.
    /// </summary>
    public uint PlayerID { get; set; }

    /// <summary>
    ///     The world this player is in.
    /// </summary>
    BlockWorld blockWorld;

    Vector3 lastPosition;
    Vector3 lastRotation;

    void Update ()
    {
        if (client == null)
        {
            Debug.LogError("No client assigned to BlockCharacter component!");
            return;
        }

        if (PlayerID == client.ID)
        {
            if (Vector3.SqrMagnitude(transform.position - lastPosition) > 0.1f ||
                Vector3.SqrMagnitude(transform.eulerAngles - lastRotation) > 5f)
                SendTransform();

            if (Input.GetMouseButtonDown(0))
            {
                //Get a point 2 meters in front of the center-point of the camera
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 2f));

                //Destroy the block there!
                blockWorld.DestroyBlock(pos);
            }

            if (Input.GetMouseButtonDown(1))
            {
                //Get a point 2 meters in front of the center-point of the camera
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 2f));

                //Place a block there!
                blockWorld.AddBlock(pos);
            }
        }
    }
    
    public void Setup(UnityClient client, BlockWorld blockWorld)
    {
        this.client = client;

        this.blockWorld = blockWorld;
    }

    void SendTransform()
    {
        //Serialize
        DarkRiftWriter writer = new DarkRiftWriter();
        writer.Write(transform.position.x);
        writer.Write(transform.position.y);
        writer.Write(transform.position.z);
        writer.Write(transform.eulerAngles.x);
        writer.Write(transform.eulerAngles.y);
        writer.Write(transform.eulerAngles.z);

        TagSubjectMessage message = new TagSubjectMessage(MOVEMENT_TAG, 0, writer);

        //Send
        client.SendMessage(message, SendMode.Unreliable);

        //Store last values sent
        lastPosition = transform.position;
        lastRotation = transform.eulerAngles;
    }
}
