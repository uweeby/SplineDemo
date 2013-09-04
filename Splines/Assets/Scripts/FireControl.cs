using UnityEngine;

public class FireControl : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Bullet;

    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Point4;
    public float Speed;

    public float FireRate;
    public float Cooldown;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
	{
	    CheckCooldown();
	}

    void CheckCooldown()
    {
        Cooldown += Time.deltaTime; //Tick Tock

        if (Cooldown >= FireRate)
        {
            //Fire!
            Cooldown = 0.0f;

            FireGun();
        }
    }

    void FireGun()
    {
        GameObject newBullet = GameObject.Instantiate(Bullet, Gun.transform.position, transform.rotation) as GameObject;
        SplineBall splineBall = newBullet.GetComponent<SplineBall>();
        splineBall.Point1 = this.Point1;
        splineBall.Point2 = this.Point2;
        splineBall.Point3 = this.Point3;
        splineBall.Point4 = this.Point4;
        splineBall.Speed = this.Speed;
    }
}
