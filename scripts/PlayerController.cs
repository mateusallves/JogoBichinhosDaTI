using UnityEngine;

/*

1. Crie uma capsula (3d Object -> Capsule)
2. Adicione um componente rigidbody na capsula através do menu Inspect
3. Abra o menu Constraints do componente rigidbody e mude as seguintes opções:
    - Interpolate: Interpolate
    - Collision Detection: Continuous
4. Anexe o arquivo PlayerController na Capsula

*/

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public struct CameraOffset {
        public float X;
        public float Y;
        public float Z;
    }

    [SerializeField, Min(0.01f)]
    float MoveSpeed = 5.0f;

    [Header("Camera Offsets")]
    [SerializeField] CameraOffset camPosOffset = new CameraOffset { X = 0, Y = 3, Z = -5 };
    [SerializeField] CameraOffset camAngOffset = new CameraOffset { X = 30, Y = 0, Z = 0 };

    Rigidbody rb;
 
    float moveHorizontal, moveVertical;

    Transform cameraTransform;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;
    }

    private void Update() {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        MoveCamera();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MovePlayer() {
        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;

        if (moveHorizontal == 0 && moveVertical == 0) {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    private void MoveCamera() {
        Quaternion cameraRotation = Quaternion.Euler(new Vector3(camAngOffset.X, camAngOffset.Y, camAngOffset.Z));
        Vector3 cameraPosition = transform.position;
        cameraPosition += new Vector3(camPosOffset.X, camPosOffset.Y, camPosOffset.Z);

        cameraTransform.rotation = cameraRotation;
        cameraTransform.position = cameraPosition;
    }
}
