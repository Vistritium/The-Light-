using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BallShottingComponent : MonoBehaviour
{
    private GameObject player;
    private FiringTimeProvider firingTimeProvider;
    private TargetProviderProvider targetProviderProvider;
    private SpeedProviderProvider speedProviderProvider;

    private bool onPosition = false;

    // Use this for initialization
    private void Start()
    {
        player = GameObject.Find("Audi");
        firingTimeProvider = GetComponent<FiringTimeProvider>();
        targetProviderProvider = GetComponent<TargetProviderProvider>();
        speedProviderProvider = GetComponent<SpeedProviderProvider>();

        GetComponent<FollowingMachine>().onPosition += () => onPosition = true;

    }

    // Update is called once per frame
    private void Update()
    {
        if (firingTimeProvider.ShouldFire() && onPosition)
        {
            Shot();
        }
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