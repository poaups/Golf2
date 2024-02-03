using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public float duration = 1f;
    public float Puissance;

    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position; // Ca creer un nv vecteur avec les vecteur de la camera
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float randomX = Random.Range(-Puissance, Puissance);
            float randomY = Random.Range(-Puissance, Puissance);

            // Vecteur de la camera = au vecteur creer(qui a les meme CO que la camera) + les parametres.
            transform.position = startPosition + new Vector3(randomX, randomY, 0);

            yield return null;
        }

        // A la fin de la duration
        transform.position = startPosition;
    }
}
