using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

internal class BlockPlayerSpawner : MonoBehaviour
{
    const byte SPAWN_TAG = 0;

    const ushort SPAWN_SUBJECT = 0;

    const ushort DESPAWN_SUBJECT = 1;

    [SerializeField]
    [Tooltip("The client to communicate with the server via.")]
    UnityClient client;

    [SerializeField]
    [Tooltip("The block world in the scene.")]
    BlockWorld blockWorld;

    [SerializeField]
    [Tooltip("The player object to spawn.")]
    GameObject playerPrefab;

    [SerializeField]
    [Tooltip("The network player object to spawn.")]
    GameObject networkPlayerPrefab;

    [SerializeField]
    [Tooltip("The network player manager.")]
    BlockCharacterManager characterManager;

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
        if (message != null && message.Tag == SPAWN_TAG)
        {
            DarkRiftReader reader = message.GetReader();

            switch (message.Subject)
            {
                case SPAWN_SUBJECT:
                    SpawnPlayer(reader);
                    break;

                case DESPAWN_SUBJECT:
                    DespawnPlayer(reader);
                    break;
            }
        }
    }

    void SpawnPlayer(DarkRiftReader reader)
    {
        Vector3 position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        Vector3 rotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

        uint id = reader.ReadUInt32();

        if (id == client.ID)
        {
            GameObject o = Instantiate(
                playerPrefab,
                position,
                Quaternion.Euler(rotation)
            ) as GameObject;

            BlockCharacter character = o.GetComponent<BlockCharacter>();
            character.PlayerID = id;
            character.Setup(client, blockWorld);
        }
        else
        {
            GameObject o = Instantiate(
                networkPlayerPrefab,
                position,
                Quaternion.Euler(rotation)
            ) as GameObject;

            BlockNetworkCharacter character = o.GetComponent<BlockNetworkCharacter>();
            characterManager.AddCharacter(id, character);
        }
    }

    void DespawnPlayer(DarkRiftReader reader)
    {
        characterManager.DestroyCharacter(reader.ReadUInt32());
    }
}

