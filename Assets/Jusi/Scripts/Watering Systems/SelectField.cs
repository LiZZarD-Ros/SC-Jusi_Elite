using UnityEngine;

public class SelectField : MonoBehaviour
{
    private bool rainBonusActive = false;
    public ParticleSystem rainParticles;

    public void ApplyRainEffect(GameObject cloud)
    {
        rainBonusActive = true;

        if (rainParticles != null)
            rainParticles.Play();

        // Tell the cloud to destroy itself + trigger respawn
        cloud.GetComponent<MoveCloud>().Consume();
    }

    // Example harvest function
    public int Harvest(int baseAmount)
    {
        int harvest = baseAmount;
        if (rainBonusActive)
        {
            harvest *= 2;
            rainBonusActive = false; // bonus only once
        }
        return harvest;
    }

    // Detect if cloud was dropped
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cloud"))
        {
            ApplyRainEffect(other.gameObject);
        }
    }
}
