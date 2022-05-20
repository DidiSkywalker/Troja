using UnityEngine;
using UnityEngine.UI;

public class ClippyBehaviourScript : MonoBehaviour
{
    enum AnimatedMoves
    {
        SLIDEIN, SLIDEOUT, SLIDEUP, SLIDEDOWN
    }

    public Sprite m_Sprite;
    private Image m_Image;
    private RectTransform m_RectTransform;
    private Animator anim;

    public Vector2 positionOffset = new Vector2(0, 0);
    private Vector2 position = new Vector2(0, 0);
    private Vector2 clippySize;

    public Vector2 percentagePosition = new Vector2(0, 0);

    void Start()
    {
        anim = GetComponent<Animator>();

        m_Image = GetComponent<Image>();
        m_Image.sprite = m_Sprite;
        
        m_RectTransform = GetComponent<RectTransform>();

        recalcClippySize();
        moveClippyRelative(position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) moveClippyAnimated(AnimatedMoves.SLIDEIN);
        if (Input.GetKeyDown(KeyCode.D)) moveClippyAnimated(AnimatedMoves.SLIDEOUT);
        if (Input.GetKeyDown(KeyCode.W)) moveClippyAnimated(AnimatedMoves.SLIDEUP);
        if (Input.GetKeyDown(KeyCode.S)) moveClippyAnimated(AnimatedMoves.SLIDEDOWN);

        moveClippyRelative(percentagePosition);
    }

    private void moveClippyAnimated(AnimatedMoves move)
    {
        switch (move)
        {
            case AnimatedMoves.SLIDEIN:
                anim.SetTrigger("TriggerSlideIn");
                break;
            case AnimatedMoves.SLIDEOUT:
                anim.SetTrigger("TriggerSlideOut");
                break;
            case AnimatedMoves.SLIDEUP:
                anim.SetTrigger("TriggerSlideUp");
                break;
            case AnimatedMoves.SLIDEDOWN:
                anim.SetTrigger("TriggerSlideDown");
                break;
            default:
                break;
        }
    }

    private void replaceClippySprite(Sprite newSprite)
    {
        m_Image.sprite = newSprite;
        recalcClippySize();
        moveClippyRelative(percentagePosition);
    }

    private void moveClippyRelative(Vector2 relativePosition)
    {
        Vector3 absolutePosition = new Vector3(clippySize.x * (0.5f - relativePosition.x) + positionOffset.x, -clippySize.y * (0.5f - relativePosition.y) + positionOffset.y, 0f);
        m_RectTransform.anchoredPosition = absolutePosition;
    }

    private void recalcClippySize()
    {
        clippySize = new Vector2(m_RectTransform.sizeDelta.x * m_RectTransform.localScale.x, m_RectTransform.sizeDelta.y * m_RectTransform.localScale.y);
    }
}