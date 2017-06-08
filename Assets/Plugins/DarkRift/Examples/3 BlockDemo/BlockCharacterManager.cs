using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using System.Collections.Generic;
using UnityEngine;

internal class BlockCharacterManager : MonoBehaviour
{
    const byte MOVEMENT_TAG = 1;

    [SerializeField]
    [Tooltip("The client to communicate with the server via.")]
    UnityClient client;

    /// <summary>
    ///     The characters we are managing.
    /// </summary>
    Dictionary<uint, BlockNetworkCharacter> characters = new Dictionary<uint, BlockNetworkCharacter>();

    void Awake()
    {
        if (client == null)
        {
            Debug.LogError("No client assigned to BlockPlayerSpawner component!");
            return;
        }

        client.MessageReceived += Client_MessageReceived;
    }

    void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        TagSubjectMessage message = e.Message as TagSubjectMessage;

        //Check the tag
        if (message != null && message.Tag == MOVEMENT_TAG)
        {
            DarkRiftReader reader = message.GetReader();

            //Read message
            Vector3 newPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Vector3 newRotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            uint id = reader.ReadUInt32();

            //Update characters to move to new positions
            characters[id].NewPosition = newPosition;
            characters[id].NewRotation = newRotation;
        }
    }

    public void AddCharacter(uint id, BlockNetworkCharacter character)
    {
        characters.Add(id, character);
    }

    public void DestroyCharacter(uint id)
    {
        Destroy(characters[id]);
    }
}

