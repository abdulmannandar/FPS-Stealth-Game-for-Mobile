using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapUse : MonoBehaviour, IDragHandler
{
    public RectTransform imageRectTransform; 
    public float zoomSpeed = 0.1f;
    public float minZoom = 1.0f; 
    public float maxZoom = 2.0f; 

    private Vector2 lastMousePosition;

    public GameObject maphandler;
    public GameObject map;
    public GameObject objectives;

    private AudioSource audiosource;
    public AudioClip click;
    public AudioClip paper;
    public AudioClip mpzoom;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void ZoomIn()
    {
        audiosource.clip = mpzoom;
        audiosource.Play();
        Zoom(Vector2.zero, zoomSpeed);
    }

    public void ZoomOut()
    {
        audiosource.clip = mpzoom;
        audiosource.Play();
        Zoom(Vector2.zero, -zoomSpeed);
    }

    private void Zoom(Vector2 zoomCenter, float increment)
    {
        Vector3 scale = imageRectTransform.localScale;
        scale += new Vector3(increment, increment, 0);

        
        scale = Vector3.Max(Vector3.one * minZoom, Vector3.Min(Vector3.one * maxZoom, scale));
        imageRectTransform.localScale = scale;
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        imageRectTransform.anchoredPosition += delta;
    }
    public void OpenMap()
    {
        audiosource.clip = paper;
        audiosource.Play();
        maphandler.SetActive(true);
    }
    public void CloseMap()
    {
        audiosource.clip = paper;
        audiosource.Play();
        maphandler.SetActive(false);
    }
    public void OpenMapProper()
    {
        audiosource.clip = click;
        audiosource.Play();
        objectives.SetActive(false);
        map.SetActive(true);
    }
    public void ViewObjectives()
    {
        audiosource.clip = click;
        audiosource.Play();
        objectives.SetActive(true);
        map.SetActive(false);
    }

}

