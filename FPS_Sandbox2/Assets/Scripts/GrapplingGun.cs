using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
   private LineRenderer lr;
   private Vector3 grapplePoint;
   public LayerMask whatIsGrappleable;
   public Transform gunTip, cam, player;
   private float maxDistance = 100f;
   private SpringJoint joint;
   public ParticleSystem muzzleFlash;

   void Awake(){
        lr = GetComponent<LineRenderer>();
   }

   void Update(){
        if (Input.GetMouseButtonDown(1)) {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1)){
            StopGrapple();
        }
        else if (Input.GetMouseButtonDown(0)) {
          Shoot();
        }
   }

   void LateUpdate(){
    DrawRope();
   }

   void StartGrapple() {
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, whatIsGrappleable)){
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
   }

   void DrawRope() {
    if(!joint) return;
    lr.SetPosition(0, gunTip.position);
    lr.SetPosition(1, grapplePoint);


   }

   void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
   }

   public bool IsGrappling(){
        return joint != null;
   }

   public Vector3 GetGrapplePoint(){
        return grapplePoint;
   }

   void Shoot() {
     
     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

     RaycastHit hit;

     muzzleFlash.Play();  

     if(Physics.Raycast(ray, out hit, 100)){
          if(hit.transform.name == "enemy(Clone)"){
          Destroy(hit.transform.gameObject);
          }
     }
        
   }
}
