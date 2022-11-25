using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLauncher : MonoBehaviour, ILaunchMissile
{
    public bool CanFire => true;

    public Vector3 Position => throw new System.NotImplementedException();

    public void Launch(Vector3 target)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
