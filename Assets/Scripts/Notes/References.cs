using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{



    /*
     * Quaternion.Euler(x,y,z)
       
     * Quaternion.Slurp CAN DETERM THE SPEED IN WHICH THE OBJECT ROTATES

     * Debug.DrawRay  
     
     * 3%2 give you the remainder of that division
     
     * public void OnEnable/OnDisable (allow to run a method when the object is enabled or disbled)
     
     * [HideinInspector] to make public variable not show up in the inspector
      
     // public void forLoop();
    {
     for(int i = 0; i<numEnemies; i++)
        {
            Debug.Log("Creating enemy number: " + i);
        }
    }
     // WHILE LOOP (IEnumerator need to do WaitReturn)
     while(cupsInTheSink > 0)
        {
            Debug.Log ("I've washed a cup!");
            cupsInTheSink--;
        }
     // DO WHILE LOOP (does it atleast one time)

        do
        {
            print ("Hello World");
            
        }while(shouldContinue == true);

     // FOR EACH LOOP
        foreach(string item in strings)
        {
            print (item);
        }

     // DISABLE COMPONENT
        myLight.enabled = !myLight.enabled;

     // ACTIVATE GAMEOBJECT 
        Debug.Log("Active Self: " + myObject.activeSelf);
        Debug.Log("Active in Hierarchy" + myObject.activeInHierarchy);

     // ARRAYS
        public GameObject[] players;
        public GameObject[] players = new GameObject[amount];
        public GameObject[] players = new GameObject[] {gameObject1, gameObject2, gameObject3...};
        0-max numbers of objects in the array;

     // Vector3 GetPosition (int value) (example of how you can create a custom method to create a kindex of vectors 3)
        {

        }

     // INVOKE
        Invoke ("SpawnObject", 2); (This is a method different, no a GameObject)
        InvokeRepeating("SpawnObject", 2, 1);

     // ENUM
        public enum Direction {North, East, South, West};
        public Directon currentDirection;

        enum uses . to correlate example: Direction.North algo Direction.0 or number that was assign;
        (you can asign the value manually North=1, East=54 ... )

     // SWITCH
        switch (intelligence)
        {
        case 1: 
            print ("Ulg, glib, Pblblblblb");
            break;
        default: <5
            print ("Incorrect intelligence level.");
            break;

     // STATICS
        public static int (this allows to get this variable anywhere in the program and live for the life of the program)
        namesofthescript.nameofthevariable to acess it.

        You can use statics in classes and it wil affect every single instance of said class and
        individual cannnot modifice the static variable.
        public Item()
        {
         itemCount = itemCount ++;
        }
        static variables inside a class are created before the other variables.    

     // LIST
        List<BadGuy> badguys = new List<BadGuy>();

     // DICTONARY
        Dictionary<string, BadGuy> badguys = new Dictionary<string, BadGuy>();
        KeyValuePair<> use in a foreach loop.

        itemList.Add(ItemDB.a.consumable[itemId]);

        foreach (KeyValuePair<int,Consumable> Consumable in  ItemDB.a.consumable)
        {
            
        }
        ItemDB.a.usable[itemID]



     // SINGLETONS
        public static GameManager _instance;
        public static GameManager instance
        {
           get
           {
              if (_instance == null)
                 Debug.LogError("The GameManager is NULL.");
                 
                 return _instance;
           }
        }

        private void Awake()
        {
         _instance = this;
         DontDestroyOnLoad(this);
        }

     // COROUTINE

        StartCoroutine(name of the method)

        private IEnumerator name of the method ()
        {
         something();
         yield return WaitForSeconds(1f) (Here you put the amount of time you want to wait for executing the next line of code)
         something();
        }

     // RAYCAST (you can use the component Line Renderer in order to see the raycast in game and needs to be disable it actives when you cast a ray)
        
        Phsyics.Raycast(vector3 origen, vector3 direction, out hit (store aditional information), distance of ray in f )
        {
         // Set the end position for our laser line 
         laserLine.SetPosition (1, hit.point);

         // Get a reference to a health script attached to the collider we hit
         ShootableBox health = hit.collider.GetComponent<ShootableBox>(); 
         (get access to the script)

         Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.green);

        }
     // "RAYCAST" 2D
        Physics2D.OverlapCircle(position + new Vector3(0, 1, 0), .2f) (checks but doesnt return)
        Physics.Linecast(transform.position, position + new Vector3(0, 1, 0), out hit) (?

        You need to create a variable called LayerMask enemyLayer;

     // UI 

        To create a background image like a main manu you need to create a panel and set texture type to sprite 2D/UI
        
        You can press alt while changing the size of a image to scale it from the middle

        With text mashpro you can create a gradient preset (color) to use for every ui element by right clicking in the project f;

        You can add a component call shadow for the Text 

     // HOW TO MAKE A DIALOGUE BOX USING QUEUE
     
        public queue <string> setences;
        star( sentences = new queue<strings>();
        We create a new class call dialogue which contains public NPCname; [TextArea(minlines,maxlines)] public string[] senteces;

     // random boolean
        bool trueorfalse = (Random.value > 0.5f);

     // you can use / en layes to better organize

     // DRAW GIZMOS
        void OnDrawGizmoSelected()
        {
        Gizmos.color = colorToChoose;
        Gizmos.DrawWireSphere(vector3,radio);
        }

     // SAVING THE GAME 1

        PlayerPrefs.SetInt/SetFloat (name of the setting, value of the setting)
        
        PlayerPrefs.DeleteKey(name of the setting)

     // SEPARATE PARTS FROM THE SAME SCRIPT
        [Header("Audio")]

    */
}
