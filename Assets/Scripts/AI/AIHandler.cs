using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class AIHandler : MonoBehaviour
{

    [SerializeField]
    CarHandler carHandler;

    [SerializeField]
    LayerMask otherCarsLayerMask;

    [SerializeField]
    MeshCollider meshCollider;

    [Header("SFX")]
    [SerializeField]
    AudioSource honkHornAS;

    RaycastHit[] raycastHits = new RaycastHit[1];
    bool isCarAhead = false;
    float carAheadDistance = 0;

    int drivingInLane = 0;


    WaitForSeconds wait = new WaitForSeconds(0.2f);
    private void Awake()
    {
        if (CompareTag("Player"))
        {
            Destroy(this);
            return;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(UpdateLessOftenCO());
    }

    // Update is called once per frame
    void Update()
    {
        float accelerationInput = 1.0f;
        float steerInput = 0.0f;

        if (isCarAhead)
        {
            accelerationInput = -1;
            if(carAheadDistance < 10 && !honkHornAS.isPlaying)
            {
                honkHornAS.pitch = Random.Range(0.5f,1.1f);
                honkHornAS.Play();
            }
        }
        float desiredPositionX = Utils.CarLanes[drivingInLane];
        float difference = desiredPositionX - transform.position.x;
        if(Mathf.Abs(difference) > 0.05f)
            steerInput = 1.0f * difference;
        steerInput = Mathf.Clamp(steerInput,-1.0f,1.0f);
        carHandler.SetInput(new Vector2(steerInput,accelerationInput));
    }
    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            isCarAhead = CheckIfOtherCarIsAhead();
            yield return wait;
        }
    }
    bool CheckIfOtherCarIsAhead()
    {   
        meshCollider.enabled = false;
        int numberOfHits = Physics.BoxCastNonAlloc(transform.position,Vector3.one * 0.25f,transform.forward,raycastHits,Quaternion.identity,2,otherCarsLayerMask);
        meshCollider.enabled = true;
        if(numberOfHits > 0) {
            carAheadDistance = (transform.position - raycastHits[0].point).magnitude;
            return true;
            }
        return false;
    }
    public void OnEnable()
    {
        carHandler.SetMaxSpeed(Random.Range(2,4));
        drivingInLane = Random.Range(0, Utils.CarLanes.Length);

    }
}
