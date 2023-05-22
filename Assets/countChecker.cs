using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using TMPro;

public class countChecker : MonoBehaviour
{

    //   private void OnTriggerStay2D(Collider2D other)
    //   {
    //       if (other.gameObject.CompareTag("Blue")) 
    //        {
    //            Destroy(other.gameObject); 
    //        }
    //
    //   }

    [SerializeField]
    GameObject[] Prefabs;

    [SerializeField]
    GameObject bottom;

    [SerializeField]
    GameObject[] Bomb;

    [SerializeField]

    Button generateButton;

    [SerializeField]
    TMP_Text text;
    
    

    int[] ObjectColor = new int[5];
    private int numYellowObjects = 0;
    private int numGreenObjects = 0;
    private int numBlueObjects = 0;
    private int numRedObjects = 0;
    private int numPurpleObjects = 0;

    int MoneySpent;
    int i;

    float insats = 6;

    float MoneyColor;
    
    float money = 100;

    bool hasStarted = false;

    string color;

    private void Start()
    {

        // Generator();
    }
    int total = 0;
    bool ReTrigg = false;
    int counter;
   
    float odds;

    public void Generator()
    {
        
        generateButton.enabled = false;

        if (ReTrigg == false)
        {
            MoneySpent = MoneySpent + 3;
            print("Money spent " + MoneySpent);
            money = money - insats;
            text.SetText(money.ToString());


        }
        for (var y = 0; y < 5; y++)
        {
            for (var x = -5; x < 6; x++)
            {
                int c;
                int r = Random.Range(1, 100);


                

                if (r >= 90)
                {
                    c = 5;
                }

                else if (r >= 80)
                {
                    c = 4;
                }

                else if (r >= 55)
                {
                    c = 3;
                }

                else if (r >= 40)
                {
                    c = 2;
                }
                else if (r >= 20)
                {
                    c = 1;
                }
                
               

                else
                {
                    c = 0;
                }
                 
              

                Instantiate(Prefabs[c], new Vector3(x, 7 + y), Quaternion.identity);
                counter++;

                if (counter == 55 && r == 1)
                {
                    print("fool");
                    Bombstart();
                }
                
                
            if (ReTrigg == true && counter == total)
        {
            
            break;
        }
            }
            
        if (ReTrigg == true && counter == total)
        {
            
            break;
        }
            
                
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!hasStarted)
        {
            
            hasStarted = true;
            StartCoroutine(StartCoroutine());
        }

        if (other.gameObject.CompareTag("Yellow"))
        {
            ObjectColor[0] = numYellowObjects++;

        }

        if (other.gameObject.CompareTag("Green"))
        {
            ObjectColor[1] = numGreenObjects++;
        }

        if (other.gameObject.CompareTag("Blue"))
        {
            ObjectColor[2] = numBlueObjects++;
        }

        if (other.gameObject.CompareTag("Red"))
        {
            ObjectColor[3] = numRedObjects++;
        }

        if (other.gameObject.CompareTag("Purple"))
        {
            ObjectColor[4] = numPurpleObjects++;
        }
    }

    // private void DestroyObjects()
    // {



    //     GameObject[] desobjects = GameObject.FindGameObjectsWithTag(color);

    //     foreach (GameObject Obj in desobjects)
    //     {
    //         Destroy(Obj, i + 1);
    //     }
    // }


    private void EverySecond()
    {

       
        string[] colorNames = { "Yellow", "Green", "Blue","Orange", "Red", "Purple"};



        GameObject[] blocks = GameObject.FindGameObjectsWithTag(colorNames[i]);
        
        if (blocks.Length >= 16)
        {
           
             
           
            if (i == 0)
            {
                
                MoneyColor = 1.5f;
            }
             if (i == 1)
            {
                
                MoneyColor = 2.5f;
            }
             if (i == 2)
            {
                
                MoneyColor = 3.3f;
            }
             if (i == 3)
            {
                
                MoneyColor = 4.7f;
            }
             if (i == 4)
            {
               
                MoneyColor = 6f;
            }
             if (i == 5)
            {
               
                MoneyColor = 10f;
            }

            
            
            money = money + blocks.Length * 0.1f * MoneyColor;
            print("money " + money);
            text.SetText(money.ToString());

            foreach(GameObject block in blocks)
            {   
                
                Destroy(block);
            }
        }
                
        

        foreach (string color in colorNames)
        {
            total += GameObject.FindGameObjectsWithTag(color).Length;
        }
        
        
        i++;
        
        
        if (total != 55 && i == 6)
        {
            hasStarted = false;
            counter = 0;
            ReTrigg = true;
            total = 55 - total;
            i = 0;
            Generator();
            
            

            

        }
        if(total == 55 && i == 6)
        {
            BoxCollider2D boxCollider = bottom.GetComponent<BoxCollider2D>();
            i = 0;
            bottom.SetActive(false);
            StartCoroutine(StartOver());
            
            

        }
            
            
        total = 0;
        
       
    }

    private void Bombstart()
    {

        int r = Random.Range(1, 100);
        int g;
       
        print("r " + r);
       
        if (r >= 100)
        {
            g = 2;
        }
        
        else if (r >= 95)
        {
            g = 1;
        }
        
        else if (r >= 90)
        {
            g = 0;
        }

        else
        {
            g = 10;
        }
        
        print("g " + g);



        GameObject bombInstance = Instantiate(Bomb[g], new Vector3(20, 14), Quaternion.identity);

        Rigidbody2D bombRigidbody = bombInstance.GetComponent<Rigidbody2D>();

        bombRigidbody.AddForce(Vector2.left * 15, ForceMode2D.Impulse);


    
        


    }


    private IEnumerator EverySecondCoroutine()
    {

        while (hasStarted == true && money >= insats)
        {

            yield return new WaitForSeconds(0.5f);
            EverySecond();
        }
        yield break;

    }

    
    
    
    
    
    
    private IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(EverySecondCoroutine());
        
        
    }

    private IEnumerator StartOver()
    {

        ReTrigg = false;
        counter = 0;
        hasStarted = false;
        yield return new WaitForSeconds(1.7f);
        
        bottom.SetActive(true);
        
        Generator();
        
    }
    
}


