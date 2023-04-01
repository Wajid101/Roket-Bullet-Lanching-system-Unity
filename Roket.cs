using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roket : MonoBehaviour
{
    //	public float RoketSpeed=30000;
    public B_R_Sobject R;
    public int Magsize = 15;
    public Rigidbody Roketi;
    List<Rigidbody> Roketlist;
    public Transform BarrelEnd, BarrelEndl;
    public AudioSource Sound;
    //	public float Reloadtime=10;
    //	public float MagzineSize=41f;
    public float Count = 0f;
    public Image Megbar;
    float time = 0.0f;
    float fillimage = 0f;
    public static Roket instance;
    public bool aim_check = false;
    public int Ammo_cast = 50;
    //	Quaternion targetRotaion;



    public enum Ammocheck { bullet, roket };
    public Ammocheck A_check = Ammocheck.bullet;

    Vector3 DirectionValue;
    float _Angle, Distanceobj;
    public bool ismulti = false;
    //	RaycastHit hit;
    int count = 0;







    public void Start()
    {

        instance = this;
        fillimage = 1 / R.Reload_time;
        Roketlist = new List<Rigidbody>();
        for (int i = 0; i < Magsize; i++)
        {
            Rigidbody objbullet = (Rigidbody)Instantiate(Roketi);

            objbullet.gameObject.SetActive(false);
            Roketlist.Add(objbullet);
        }
    }
    public void fire()
    {
        for (int i = 0; i < Roketlist.Count; i++)
        {
            if (Count < R.Megsize)
            {
                if (!Roketlist[i].gameObject.activeInHierarchy)
                {
                    if (ismulti == true)
                    {
                        if (count == 0)
                        {
                            Roketlist[i].transform.position = BarrelEnd.position;
                            Roketlist[i].transform.rotation = BarrelEnd.rotation;
                            count = 1;
                        }
                        else
                        {
                            Roketlist[i].transform.position = BarrelEndl.position;
                            Roketlist[i].transform.rotation = BarrelEndl.rotation;
                            count = 0;
                        }

                    }
                    else
                    {
                        Roketlist[i].transform.position = BarrelEnd.position;
                        Roketlist[i].transform.rotation = BarrelEnd.rotation;
                    }

                    Roketlist[i].gameObject.SetActive(true);
                    Rigidbody temrigbodybullet = Roketlist[i];
                    if (Raycst_position.instance.hit.collider != null)
                    {
                        if (aim_check == true)
                        {
                            temrigbodybullet.velocity = (Raycst_position.instance.hit.transform.position - transform.position).normalized * R.Speed * 2f;
                        }
                        else
                        {
                            temrigbodybullet.velocity = (Raycst_position.instance.hit.point - transform.position).normalized * R.Speed * 2f;
                        }

                    }

                    else
                    {
                        temrigbodybullet.velocity = (BarrelEnd.right * R.Speed * 2f);
                    }

                    Sound.Play();
                    Count++;
                    Megbar.fillAmount = (R.Megsize - Count) / R.Megsize;

                    break;
                }
                else
                {
                    time = 0f;
                }


            }
        }
        if (Count >= R.Megsize)
        {
            StartCoroutine(Wait());

        }
    }
    void Update()
    {


        // Does the ray intersect any objects excluding the player layer
        if (Raycst_position.instance.hit.collider != null)
        {
            if (A_check == Ammocheck.bullet)
            {
                Distanceobj = (Raycst_position.instance.hit.transform.position - transform.position).magnitude;
                if (Distanceobj <= 150 && AngleValue(Raycst_position.instance.hit.transform, transform) <= 4)
                {
                    if (Raycst_position.instance.hit.collider.tag == "E" || Raycst_position.instance.hit.collider.tag == "E1")
                    {
                        aim_check = true;
                    }
                }
                else
                {
                    aim_check = false;
                }

            }
        }
        else
        {
            aim_check = false;
        }

        if (Count >= R.Megsize)
        {
            time += Time.deltaTime;
            Megbar.fillAmount = (time * fillimage);

        }

        if (instance == null)
        {
            instance = this;
        }

    }



    IEnumerator Wait()
    {
        Megbar.fillAmount = 0;
        yield return new WaitForSeconds(R.Reload_time);
        Dbonus_Acast.instance.Ammo_Cast += Ammo_cast;
        Count = 0f;
    }





    float AngleValue(Transform Target, Transform _Transform) //return Angle Value between Target, _Transform
    {
        DirectionValue = (Target.transform.position - _Transform.position).normalized; //Set Direction Value between target and _Trasnform.
        _Angle = Vector3.Angle(DirectionValue, _Transform.forward); //Set Angle Value between DirectionValue and forward transform.
        return _Angle; // return Angle
    }




}


