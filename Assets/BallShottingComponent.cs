using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BallShottingComponent : MonoBehaviour
{
    private GameObject player;
    private FiringTimeProvider firingTimeProvider;
    private TargetProviderProvider targetProviderProvider;
    private SpeedProviderProvider speedProviderProvider;
    private Vector3 initialPosition;

    private Oscilator oscilator;

    private bool onPosition = false;

    // Use this for initialization
    private void Start()
    {
        player = GameObject.Find("Audi");
        firingTimeProvider = GetComponent<FiringTimeProvider>();
        targetProviderProvider = GetComponent<TargetProviderProvider>();
        speedProviderProvider = GetComponent<SpeedProviderProvider>();

        oscilator = new Oscilator(-5, 5, 1);

        GetComponent<FollowingMachine>().onPosition += () =>
        {
            onPosition = true;
            initialPosition = this.transform.position;
        };

    }

    // Update is called once per frame
    private void Update()
    {
        if (firingTimeProvider.ShouldFire() && onPosition)
        {
            Shot();
        }

        if (onPosition)
        {
            this.transform.position = new Vector3(initialPosition.x + oscilator.GetCurrentValue(), this.transform.position.y, this.transform.position.z);
        }
    }


    private void Remove()
    {
        onPosition = false;
    }

    void Shot()
    {
        var bullet = Instantiate(Templates.GetTemplate("ElectricBlast"));

        targetProviderProvider.ProvideTargetProvider(bullet);
        speedProviderProvider.ProvideSpeedProvider(bullet);
        bullet.SetActive(true);
        bullet.transform.position = this.transform.position;
        

    }
}