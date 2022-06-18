using ClippyAction;
using Events.Channels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClippyBehaviourScript : MonoBehaviour
{
    enum AnimatedMovements
    {
        SLIDEIN, SLIDEOUT, SLIDEUP, SLIDEDOWN, SPEECHBUBBLESCALEUP, SPEECHBUBBLESCALEDOWN
    }

    enum AnimatedMoves
    {
        SLIDEIN, SLIDEOUT, SLIDEUP, SLIDEDOWN
    }

    public ClippyActionSO clippyActionInstance;
    public StringEventChannelSO ClippySayEventChannelSo;

    public GameObject speechBubble;
    public GameObject textField;
    public Sprite m_Sprite;
    private Image m_Image;
    private RectTransform m_RectTransform;
    private Animator anim;
    private RectTransform bubbleRectTransform;
    private RectTransform textRectTransform;
    private TextMeshProUGUI textComponent;
    public string text1;
    public string text2;

    public Vector2 textPadding = new Vector2(60, 30);
    public Vector2 speechBubbleScaleFactor = new Vector2(1, 1);
    public Vector2 speechBubbleScale = new Vector2(900, 450);
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

        bubbleRectTransform = speechBubble.GetComponent<RectTransform>();
        textRectTransform = textField.GetComponent<RectTransform>();
        textComponent = textField.GetComponent<TextMeshProUGUI>();

        textRectTransform.anchoredPosition = new Vector3(textPadding.x, -textPadding.y, 0);

        recalcClippySize();
        moveClippyRelative(position);
    }

    void Update()
    {
        bubbleRectTransform.sizeDelta = new Vector3(speechBubbleScale.x * speechBubbleScaleFactor.x, speechBubbleScale.y * speechBubbleScaleFactor.y, 1);


        if (Input.GetKeyDown(KeyCode.A)) moveClippyAnimated(AnimatedMovements.SLIDEIN);
        if (Input.GetKeyDown(KeyCode.D)) moveClippyAnimated(AnimatedMovements.SLIDEOUT);
        if (Input.GetKeyDown(KeyCode.W)) moveClippyAnimated(AnimatedMovements.SLIDEUP);
        if (Input.GetKeyDown(KeyCode.S)) moveClippyAnimated(AnimatedMovements.SLIDEDOWN);

        if (Input.GetKeyDown(KeyCode.O)) moveClippyAnimated(AnimatedMovements.SPEECHBUBBLESCALEUP);
        if (Input.GetKeyDown(KeyCode.P)) moveClippyAnimated(AnimatedMovements.SPEECHBUBBLESCALEDOWN);

        if (Input.GetKeyDown(KeyCode.F)) clippySingleSay(text1);
        if (Input.GetKeyDown(KeyCode.G)) clippySingleSay(text2);

        if (Input.GetKeyDown(KeyCode.T)) speechBubble.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Z)) speechBubble.SetActive(true);

        if (Input.GetKeyDown(KeyCode.X)) ClippySayEventChannelSo.RaiseStringEvent("input: X");

        

        moveClippyRelative(percentagePosition);
    }

    public void clippySingleSay(string says)
    {
        moveClippyAnimated(AnimatedMovements.SPEECHBUBBLESCALEUP);
        float preferredHeight;
        float lowestWidth = speechBubbleScale.x - 2 * textPadding.x;
        textComponent.text = says;
        /*
        if ((speechBubbleScale.x - 2 * textPadding.x) < textComponent.preferredWidth)
        {
            lowestWidth = textComponent.preferredWidth;
        }
        */
        textRectTransform.sizeDelta = new Vector3(lowestWidth, 0, 1);
        preferredHeight = textComponent.preferredHeight;
        speechBubbleScale.y = preferredHeight + 2 * textPadding.y;
    }

    private void moveClippyAnimated(AnimatedMovements move)
    {
        switch (move)
        {
            case AnimatedMovements.SLIDEIN:
                anim.SetTrigger("TriggerSlideIn");
                break;
            case AnimatedMovements.SLIDEOUT:
                anim.SetTrigger("TriggerSlideOut");
                break;
            case AnimatedMovements.SLIDEUP:
                anim.SetTrigger("TriggerSlideUp");
                break;
            case AnimatedMovements.SLIDEDOWN:
                anim.SetTrigger("TriggerSlideDown");
                break;
            case AnimatedMovements.SPEECHBUBBLESCALEUP:
                anim.SetTrigger("SpeechBubbleScaleUp");
                break;
            case AnimatedMovements.SPEECHBUBBLESCALEDOWN:
                anim.SetTrigger("SpeechBubbleScaleDown");
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
        Vector3 absolutePosition = new Vector3(clippySize.x * (1 - relativePosition.x) + positionOffset.x, -clippySize.y * (1 - relativePosition.y) + positionOffset.y, 0f);
        m_RectTransform.anchoredPosition = absolutePosition;
    }

    private void recalcClippySize()
    {
        clippySize = new Vector2(m_RectTransform.sizeDelta.x * m_RectTransform.localScale.x, m_RectTransform.sizeDelta.y * m_RectTransform.localScale.y);
    }
}