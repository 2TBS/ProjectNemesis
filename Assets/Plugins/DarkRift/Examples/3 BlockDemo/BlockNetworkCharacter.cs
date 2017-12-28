using UnityEngine;

internal class BlockNetworkCharacter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The speed to lerp the player's position")]
    public float moveLerpSpeed = 10f;

    [SerializeField]
    [Tooltip("The speed to lerp the player's rotation")]
    public float rotateLerpSpeed = 50f;

    public Vector3 NewPosition { get; set; }
    public Vector3 NewRotation { get; set; }

    void Awake()
    {
        NewPosition = transform.position;
        NewRotation = transform.eulerAngles;
    }

    void Update()
    {
        //Move and rotate to new values
        transform.position = Vector3.Lerp(transform.position, NewPosition, Time.deltaTime * moveLerpSpeed);
        transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, NewRotation.x, Time.deltaTime * rotateLerpSpeed),
            Mathf.LerpAngle(transform.eulerAngles.y, NewRotation.y, Time.deltaTime * rotateLerpSpeed),
            Mathf.LerpAngle(transform.eulerAngles.z, NewRotation.z, Time.deltaTime * rotateLerpSpeed)
        );
    }
}
    
