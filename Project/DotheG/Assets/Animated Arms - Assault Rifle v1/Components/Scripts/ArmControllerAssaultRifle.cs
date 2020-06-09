using UnityEngine;
using System.Collections;

public class ArmControllerAssaultRifle : MonoBehaviour {
   
   Animator anim;
    UIManager ammoTx;
   bool isReloading;
   bool outOfAmmo;
   
   bool isShooting;
   bool isAimShooting;
   bool isAiming;
   bool isRunning;
   bool isJumping;

    GenerateMonster feverTimeAmmo;
    
    //총알 발사 스크립트
    public GameObject bulletFactory; //총알이 생성되는 장소
    public Transform firePosition; // 총알을 생성하여 위치 시키는 장소


    public int cnt = 0;


    //Ammo left
    public int currentAmmo;

   //Used for fire rate
   float lastFired;

   public float fireRate;

   [System.Serializable]
   public class ammoSettings
   {  
      [Header("Ammo")]
      //Total ammo
      public int ammo;
   }
   public ammoSettings AmmoSettings;

   //All Components
   [System.Serializable]
   public class components
   {  
      [Header("Muzzleflash Holders")]
      public GameObject sideMuzzle;
      public GameObject topMuzzle;
      public GameObject frontMuzzle;
      //Array of muzzleflash sprites
      public Sprite[] muzzleflashSideSprites;
      
      [Header("Light Front")]
      public Light lightFlash;
      [Header("Particle System")]
      public ParticleSystem smokeParticles;

      [Header("Bullet In Mag")]
      public Transform bulletInMag;
   }
   public components Components;
   
   //All weapon types
   [System.Serializable]
   public class prefabs
   {  
      [Header("Casing Prefab")]
      public Transform casingPrefab;
   }
   public prefabs Prefabs;
   
   [System.Serializable]
   public class spawnpoints
   {  
      [Header("Spawnpoint")]
      //The position where the casings spawns from
      public Transform casingSpawnPoint;
   }
   public spawnpoints Spawnpoints;
   
   [System.Serializable]
   public class audioClips
   {  
      [Header("Audio Source")]
      public AudioSource mainAudioSource;
      
      [Header("Audio Clips")]
      //All audio clips
      public AudioClip shootSound;
      public AudioClip reloadSound;
   }
   public audioClips AudioClips;
   
   void Awake () {

        feverTimeAmmo = GameObject.Find("GameManager").GetComponent<GenerateMonster>();
      //Set the animator component
      anim = GetComponent<Animator>();

      //Set the ammoText  
      ammoTx = GameObject.Find("GameManager").GetComponent<UIManager>();



      //Set the ammo count
      RefillAmmo ();

      //Dont show the muzzleflash at start
      Components.sideMuzzle.GetComponent<SpriteRenderer> ().enabled = false;
      Components.topMuzzle.GetComponent<SpriteRenderer> ().enabled = false;
      Components.frontMuzzle.GetComponent<SpriteRenderer> ().enabled = false;

      //Disable the light flash at start
      Components.lightFlash.GetComponent<Light> ().enabled = false;
   }
   
   void Update () {

      //Check which animation 
      //is currently playing
      AnimationCheck ();

      //Left click hold to fire
      if (Input.GetMouseButton (0)
         //Disable shooting while running and jumping
          && !isReloading && !outOfAmmo && !isShooting && !isAimShooting && !isRunning && !isJumping) {
         //Shoot automatic
         if (Time.time - lastFired > 1 / fireRate) {
            if (Time.timeScale==1){
              Shoot ();
            }
            lastFired = Time.time;
         }
      }

      //Right click hold to aim
      if (Input.GetMouseButton (1)) {
         anim.SetBool ("isAiming", true);
      } else {
         anim.SetBool ("isAiming", false);
      }
         
      //R key to reload
      if (!feverTimeAmmo.isFeverTime && Input.GetKeyDown (KeyCode.R) && !isReloading) {
         Reload ();
      }

        if (feverTimeAmmo.isFeverTime)
        {
            currentAmmo = AmmoSettings.ammo;
        }

        //Run when holding down left shift and moving
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0) {
         anim.SetFloat("Run", 0.2f);
      } else {
         //Stop running
         anim.SetFloat("Run", 0.0f);
      }

      //Space key to jump
      //Disable jumping animation while reloading
      if (Input.GetKeyDown (KeyCode.Space) && !isReloading) {
         //Play jump animation
         anim.Play("Jump");
      }

         
      //If out of ammo
      if (currentAmmo == 0) {
         outOfAmmo = true;
         //if ammo is higher than 0
      } else if (currentAmmo > 0) {
         outOfAmmo = false;
      }
   }

   //Muzzleflash
   IEnumerator MuzzleFlash () {
      
      //Show muzzleflash if useMuzzleFlash is true
      //Show a random muzzleflash from the array
      Components.sideMuzzle.GetComponent<SpriteRenderer> ().sprite = Components.muzzleflashSideSprites 
         [Random.Range (0, Components.muzzleflashSideSprites.Length)];
      Components.topMuzzle.GetComponent<SpriteRenderer> ().sprite = Components.muzzleflashSideSprites 
         [Random.Range (0, Components.muzzleflashSideSprites.Length)];
         
      //Show the muzzleflashes
      Components.sideMuzzle.GetComponent<SpriteRenderer> ().enabled = true;
      Components.topMuzzle.GetComponent<SpriteRenderer> ().enabled = true;
      Components.frontMuzzle.GetComponent<SpriteRenderer> ().enabled = true;

      //Enable the light flash
      Components.lightFlash.GetComponent<Light> ().enabled = true;
      
      //Play smoke particles 
      Components.smokeParticles.Play ();

      //Show the muzzleflash for 0.02 seconds
      yield return new WaitForSeconds (0.02f);
      
      //Hide the muzzleflashes
      Components.sideMuzzle.GetComponent<SpriteRenderer> ().enabled = false;
      Components.topMuzzle.GetComponent<SpriteRenderer> ().enabled = false;
      Components.frontMuzzle.GetComponent<SpriteRenderer> ().enabled = false;

      //Disable the light flash if true
      Components.lightFlash.GetComponent<Light> ().enabled = false;
   }
   
   //Shoot
   void Shoot() {
      //Play shoot animation
      if (!anim.GetBool ("isAiming")) {
         anim.Play ("Fire");
      } else {
         anim.SetTrigger("Shoot");
      }

        GameObject bullet = Instantiate(bulletFactory);
        //bullet을 bulletFactory에서 생성
        bullet.transform.position = firePosition.position;
        //firePosition => 총구 위치
        bullet.transform.forward = firePosition.forward;
        //총알의 방향을 총구의 방향으로 일치
        //bullet.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);

        //Remove 1 bullet

        if (!feverTimeAmmo.isFeverTime) {
            currentAmmo -= 1;
        }
        ammoTx.ammoText.text = currentAmmo.ToString();
      
      //Play shoot sound
      AudioClips.mainAudioSource.clip = AudioClips.shootSound;
      AudioClips.mainAudioSource.Play();
        
        //Spawn a casing at the casing spawnpoint
      Instantiate (Prefabs.casingPrefab, 
         Spawnpoints.casingSpawnPoint.transform.position, 
         Spawnpoints.casingSpawnPoint.transform.rotation);
      
      //Show the muzzleflash
      StartCoroutine (MuzzleFlash ());

      
   }
   
   //Refill ammo
   public void RefillAmmo () {
      //Set the ammo
      currentAmmo = AmmoSettings.ammo;
        ammoTx.ammoText.text = currentAmmo.ToString();
   }
   
   //Reload
   void Reload () {
      
      //Play reload animation
      anim.Play ("Reload");
      
      //Play reload sound
      AudioClips.mainAudioSource.clip = AudioClips.reloadSound;
      AudioClips.mainAudioSource.Play();
      
      if (outOfAmmo == true) {
         //Hide the bullet inside the mag if ammo is 0
         Components.bulletInMag.GetComponent<MeshRenderer> ().enabled = false;
      }

      //Start the "show bullet" timer
      StartCoroutine (BulletInMagTimer ());
   }
   
   IEnumerator BulletInMagTimer () {
      //Wait for set amount of time 
      yield return new WaitForSeconds(0.5f);
      //Show the bullet inside the mag
      Components.bulletInMag.GetComponent<MeshRenderer> ().enabled = true;
   }
   
   //Check current animation playing
   void AnimationCheck () {
      
      //Check if shooting
      if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Fire")) {
         isShooting = true;
      } else {
         isShooting = false;
      }

      //Check if shooting while aiming down sights
      if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Aim Fire")) {
         isAimShooting = true;
      } else {
         isAimShooting = false;
      }

      //Check if running
      if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Run")) {
         isRunning = true;
      } else {
         isRunning = false;
      }
      
      //Check if jumping
      if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
         isJumping = true;
      } else {
         isJumping = false;
      }
      
      //Check if reloading
      if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Reload")) {
         // If reloading
         isReloading = true;
         //Refill ammo
         RefillAmmo();
      } else {
         //If not reloading
         isReloading = false;
      }
   }
}