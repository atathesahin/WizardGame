using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public GameObject hitVFXPrefab; // Inspector üzerinden VFX prefab'ını bağlamak için

    public void PlayHitVFX(Vector3 position)
    {
        // VFX'i instantiate et
        GameObject hitVFX = Instantiate(hitVFXPrefab, position, Quaternion.identity);

        // VFX'i bir süre sonra yok etmek için coroutine kullanabilirsin
        StartCoroutine(DestroyVFX(hitVFX));
    }

    IEnumerator DestroyVFX(GameObject vfx)
    {
        // VFX'i birkaç saniye sonra yok et
        yield return new WaitForSeconds(3f);
        Destroy(vfx);
    }
}
