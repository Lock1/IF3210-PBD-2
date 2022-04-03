using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake() {
        //mendapatkan nilai mask dari layer yang bernama Floor
        floorMask = LayerMask.GetMask("Floor");
        
        //Mendapatkan komponen Animator
        anim = GetComponent<Animator>();
        
        //Mendapatkan komponen Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // Mendapatkan nilai input horizontal
        float h = Input.GetAxisRaw("Horizontal");
        
        // Mendapatkan nilai input vertical
        float v = Input.GetAxisRaw("Vertical");

        // Mendapatkan nilai jump
        float a = Input.GetAxisRaw("Jump");

        Move(h, a, v);
        Turning();
    }

    void Turning() {
        //Buat Ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Buat raycast untuk floorHit
        RaycastHit floorHit;

        //Lakukan raycast
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            //Mendapatkan vector daro posisi player dan posisi floorHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //Mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //Rotasi player
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    //Method player dapat berjalan
    void Move(float h, float a, float v) {
        // Animasi
        anim.SetBool("IsWalking", h != 0f || v != 0f);

        //Set nilai x dan y
        movement.Set(h, a, v);
        
        //Menormalisasi nilai vector agar total panjang dari vector adalah 1
        movement = movement.normalized * speed * Time.deltaTime;
        
        //Move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }
}