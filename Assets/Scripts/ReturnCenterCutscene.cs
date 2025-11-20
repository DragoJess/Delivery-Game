using System.Collections;
using UnityEngine;

public class ReturnCenterCutscene : MonoBehaviour
{
    [SerializeField] GameObject PackageProp;
    public void Play()
    {
        PlayerController.Instance.transform.position = transform.position + new Vector3(0, 2.5f, 0);
        StartCoroutine(ThrowPackages());
    }
    IEnumerator ThrowPackages()
    {
        yield return new WaitForSeconds(1);
        foreach(var Package in DeliveryManager.Instance.Packages)
        {
            if(Package.ConvertStatus() == PackageStatus.Returned)
            { 
                var Prop = Instantiate(PackageProp, transform.position, Quaternion.identity);
                Prop.GetComponent<Rigidbody2D>().AddForce(new Vector2(4, 6), ForceMode2D.Impulse);
                yield return new WaitForSeconds(Random.Range(.5f, 1f));
            }
        }
    }
}
