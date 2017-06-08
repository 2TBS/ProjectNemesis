using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class BlockWorld : MonoBehaviour
{
    const byte WORLD_TAG = 2;

    const ushort PLACE_BLOCK_SUBJECT = 0;
    const ushort DESTROY_BLOCK_SUBJECT = 1;

    [SerializeField]
    [Tooltip("The client to communicate with the server via.")]
    UnityClient client;

    [SerializeField]
    [Tooltip("The block object to spawn.")]
    GameObject blockPrefab;
    
    List<GameObject> blocks = new List<GameObject>();

    void Awake()
    {
        if (client == null)
        {
            Debug.LogError("No client assigned to BlockWorld component!");
            return;
        }

        client.MessageReceived += Client_MessageReceived;
    }

    void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        TagSubjectMessage message = e.Message as TagSubjectMessage;
        DarkRiftReader reader = message.GetReader();

        //Check the tag
        if (message != null && message.Tag == WORLD_TAG)
        {
            Vector3 position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            switch (message.Subject)
            {
                case PLACE_BLOCK_SUBJECT:
                    GameObject o = Instantiate(
                        blockPrefab,
                        position,
                        Quaternion.identity
                    ) as GameObject;

                    o.transform.SetParent(transform);

                    blocks.Add(o);

                    break;

                case DESTROY_BLOCK_SUBJECT:
                    GameObject block = blocks.SingleOrDefault(b => b != null && b.transform.position == position);

                    if (block == null)
                        return;

                    Destroy(block);

                    blocks.Remove(block);

                    break;
            }
        }
    }

    internal void AddBlock(Vector3 position)
    {
        if (client == null)
        {
            Debug.LogError("No client assigned to BlockWorld component!");
            return;
        }

        //Don't worry about snapping, we'll do that on the server
        DarkRiftWriter writer = new DarkRiftWriter();
        writer.Write(position.x);
        writer.Write(position.y);
        writer.Write(position.z);

        TagSubjectMessage message = new TagSubjectMessage(WORLD_TAG, PLACE_BLOCK_SUBJECT, writer);

        client.SendMessage(message, SendMode.Reliable);
    }

    internal void DestroyBlock(Vector3 position)
    {
        if (client == null)
        {
            Debug.LogError("No client assigned to BlockWorld component!");
            return;
        }

        //Don't worry about snapping, we'll do that on the server
        DarkRiftWriter writer = new DarkRiftWriter();
        writer.Write(position.x);
        writer.Write(position.y);
        writer.Write(position.z);

        TagSubjectMessage message = new TagSubjectMessage(WORLD_TAG, DESTROY_BLOCK_SUBJECT, writer);

        client.SendMessage(message, SendMode.Reliable);
    }
}
