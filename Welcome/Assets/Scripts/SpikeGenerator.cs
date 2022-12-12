using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject spike;

    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;
    public float speedMultiplier;

    private void Awake()
    {
        currentSpeed = minSpeed;
        generateSpike();
    }

    public void GenerateNextSpikeWidthGap()
    {
        float randomWait = Random.Range(0.1f, 1.2f );
        Invoke("generateSpike", randomWait);
    }

    void generateSpike()
    {
        GameObject SpikeIns = Instantiate(spike, transform.position, transform.rotation);
        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedMultiplier;
        }
    }
}
