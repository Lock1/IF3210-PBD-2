using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    float timer;

    private void Awake() {
        //mendapatkan nilai mask dari layer yang bernama Floor
        floorMask = LayerMask.GetMask("Floor");
        
        //Mendapatkan komponen Animator
        anim = GetComponent<Animator>();
        timer = 0f;
        //Mendapatkan komponen Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        SpeedManager.speed = (int) speed;
        timer += Time.deltaTime;
        // Mendapatkan nilai input horizontal
        float h = Input.GetAxisRaw("Horizontal");
        
        // Mendapatkan nilai input vertical
        float v = Input.GetAxisRaw("Vertical");


        float j = Input.GetAxisRaw("Jump");

        if (j > 0 && timer > 3f) {
            playerRigidbody.AddForce(new Vector3(0f, 500f, 0f));
            timer = 0f;
        }

        Move(h, v);
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
    void Move(float h, float v) {
        // Animasi
        anim.SetBool("IsWalking", h != 0f || v != 0f);

        //Set nilai x dan y
        movement.Set(h, 0f, v);
        
        //Menormalisasi nilai vector agar total panjang dari vector adalah 1
        movement = movement.normalized * speed * Time.deltaTime;
        
        //Move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }
}