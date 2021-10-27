using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SandwhichSled : MonoBehaviour
{
    public GameObject[] SandwhichPrefabs;
    public GameObject Display;
    public TextMeshProUGUI[] Requirements;
    public Font font;
    
    public bool finished = false;
    public bool first_finished = true;

    private GridControl Sandwhich;
    public GameObject FlavourText;
    private AudioManager audioManager;
    private float velocity;

    private const int WorldHeight = 10;
    private const int WorldWidth = 15;
    private SessionInfo session;

    // Start is called before the first frame update
    void Start()
    {
        session = SessionInfo.Singleton;
        var rand = new System.Random();
        var created = Instantiate(SandwhichPrefabs[rand.Next(SandwhichPrefabs.Length)], this.gameObject.transform.position, Quaternion.identity);
        created.transform.SetParent(this.transform);   
        Sandwhich = created.GetComponent<GridControl>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        if(finished) {
            if(first_finished){
                first_finished = false;
                if (audioManager != null){
                    audioManager.RingBell();
                }
                Sandwhich.Lock();
                velocity = 0;
                session.sandwhichFinished += 1;
                if(session.sandwhichFinished % 2 == 0){
                    ToppingFactory.AddMoreFlavour();
                }
            }

            velocity += Time.deltaTime*20.0f;
            this.transform.position += new Vector3(0, velocity * Time.deltaTime, 0);
            if(CalculateBounds().min.y > WorldHeight){
                Destroy(this.gameObject);
            }
        } else {
            velocity += 10.0f * Time.deltaTime;
            var delta = Time.deltaTime * velocity;
            if(CanMoveLeft(delta)){
                this.transform.position -= new Vector3(delta, 0, 0);
            } else {
                velocity = 0;
            }
        }
    }

    public bool CanMoveLeft(float delta) {
        var bounds = CalculateBounds();
        if(bounds.min.x < -WorldWidth) return false;
        foreach(var other in GameObject.FindObjectsOfType<SandwhichSled>()){
            if(other == this) continue;
            if(other.transform.position.x > this.transform.position.x) continue;
            if(other.CalculateBounds().max.x > bounds.min.x-delta) return false;
        }
        return true;
    }

    public Bounds CalculateBounds ()
    {
        Bounds bounds = new Bounds(this.transform.position, new Vector3(0, 0, 0));
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            bounds.Encapsulate(col.bounds);
        }
        return bounds;
    }

    void UpdateText(){
        if(Requirements.Length != Sandwhich.Flavours.Length){
            Requirements = new TextMeshProUGUI[Sandwhich.Flavours.Length];
            for(var ii = 0; ii < Sandwhich.Flavours.Length; ii++){
                //var body = new GameObject("requirementtext");
                GameObject body = Instantiate(FlavourText, Display.transform);
                body.name = "requirementtext";
                Requirements[ii] = body.GetComponent<TextMeshProUGUI>();
                Requirements[ii].text = Sandwhich.Flavours[ii].ToString();
            }

            LayoutRebuilder.MarkLayoutForRebuild(this.transform as RectTransform);
        }

        finished = true;
        for(var ii = 0; ii < Sandwhich.Flavours.Length; ii++){
            if (Sandwhich.HasFlavour(Sandwhich.Flavours[ii]))
            {
                Requirements[ii].fontStyle |= FontStyles.Strikethrough;
            }
            else
            {
                Requirements[ii].fontStyle &= ~FontStyles.Strikethrough;
                finished = false;
            }
        }
    }

    private void OnDestroy()
    {
        // if (audioManager != null)
        // {
        //     audioManager.RingBell();
        // }
    }
}
