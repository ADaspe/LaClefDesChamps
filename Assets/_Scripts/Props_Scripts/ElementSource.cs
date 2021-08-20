using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementSource : MonoBehaviour
{
    public Element sourceElement;
    [Tooltip("Time before the source is automatically replenished")]
    [SerializeField] private float replenishTime = 0f;
    [SerializeField] UnityEvent onReplenish = null;

    private bool sourceTaken;

    public void AbsorbSource(PlayerBook book)
    {
        if (!sourceTaken)
        {
            if (book != null)
            {
                if (sourceElement == Element.Fire)
                {
                    FireElement fireElement = new FireElement(Element.Fire);
                    book.SetElement(fireElement);
                }
                else if (sourceElement == Element.Frog)
                {
                    FrogElement frogElement = new FrogElement(Element.Frog);
                    book.SetElement(frogElement);
                }
                else if (sourceElement == Element.Light)
                {
                    FireflyElement lightElement = new FireflyElement(Element.Light);
                    book.SetElement(lightElement);
                }
                else if (sourceElement == Element.Metal)
                {
                    MetalElement metalElement = new MetalElement(Element.Metal);
                    book.SetElement(metalElement);
                }
            }
            else print("[Element Source] ERROR : book is null, no book was found !");

            OnSourceAbsorb();
        }
    }

    private void OnSourceAbsorb()
    {
        sourceTaken = true;
        StartCoroutine(Replenish());
        //Animation de la source; déclenchement de FX
    }

    IEnumerator Replenish()
    {
        yield return new WaitForSeconds(replenishTime);
        sourceTaken = false;
        onReplenish?.Invoke();
    }
}
