using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Roket f;
    public GameObject BulletShell;
    private float Nextfire = 0.0f;
    public float Firerate = 2f;
    public int isshoot;
    public bool isbullet = false;
    void Start()
    {
        isshoot = 0;
        //		shell.SetActive (false);
    }
    void Update()
    {
        if (isshoot == 1)
        {
            //			shell.SetActive (true);
            //			Debug.Log("shootingg");
            Shoot();
        }

    }

    public void Shoot()
    {
        if (Time.time >= Nextfire)
        {
            Nextfire = Time.time + 2f / Firerate;
            f.fire();
        }
    }
    public void Down()
    {
        if (f.Count < f.R.Megsize && isbullet == true)
        {
            Invoke("Show", 0.1f);
            isshoot = 1;
        }
        else
        {
            isshoot = 1;
        }

    }
    public void Up()
    {
        if (isbullet == true)
        {
            Invoke("Show", 0.1f);
            isshoot = 0;
        }
        else
        {
            isshoot = 0;
        }


    }


    public void Show()
    {
        if (isshoot == 1 && BulletShell != null)
        {
            BulletShell.SetActive(true);
            CancelInvoke();
        }
        else if (isshoot != 1 && BulletShell != null)
        {
            BulletShell.SetActive(false);
            CancelInvoke();
        }

    }

}