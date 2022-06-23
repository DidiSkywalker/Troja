using System.Collections;
using System.Collections.Generic;
using Page;
using TMPro;
using UnityEngine;


public class BookScript : MonoBehaviour
{
    public GameObject artifactDisplay;
    public GameObject book;
    public GameObject pageText;

    public PageSO testPage;

    public float scaleInDelay = 0.9f;
    public float stayOpenTime = 5f;

    private Animator artifactAnim;
    private Animator bookAnim;

    private MeshFilter artifactMeshFilter;
    private MeshRenderer artifactMeshRenderer;
    private TextMeshProUGUI pageTextMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        artifactAnim = artifactDisplay.GetComponent<Animator>();
        bookAnim = book.GetComponent<Animator>();
        artifactMeshFilter = artifactDisplay.GetComponent<MeshFilter>();
        artifactMeshRenderer = artifactDisplay.GetComponent<MeshRenderer>();
        pageTextMeshPro = pageText.GetComponent<TextMeshProUGUI>();
    }

    public void displayArtifactAsPage(PageSO page)
    {
        artifactMeshFilter.mesh = page.artifactMesh;
        artifactMeshRenderer.materials[0].mainTexture = page.artifactTexture;
        pageTextMeshPro.text = page.pageText;

        bookAnim.SetTrigger("Aufschlagen");

        IEnumerator scaleInCoroutine()
        {
            yield return new WaitForSeconds(scaleInDelay);
            artifactAnim.SetTrigger("scaleIn");
        }
        StartCoroutine(scaleInCoroutine());

        IEnumerator disapearCoroutine()
        {
            yield return new WaitForSeconds(stayOpenTime);
            bookAnim.SetTrigger("Zuschlagen");
        }
        StartCoroutine(disapearCoroutine());
    }
}
