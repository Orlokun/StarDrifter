using UnityEngine;
using System.Collections;


public class CCEngineElectro : MonoBehaviour
{
    [Header("keep in mind: this system works with colliders")]
    [Header("4. adjust 'Find Radius' (distance) for electric lines. Done.")]
    [Header("3. set 'LayerExclude' field of 'electro engine prefab' by your gameobject unique layer")]
    [Header("2. set your gameobject to your own unique layer;")]
    [Header("1. assign 'electro engine prefab' to your gameobject;")]
    [Header("Usage:")]
    [Space(10)]
    public float LineWidth = 0.01f;
    public float FindRadius = 14f;
    [Header("ignore layer")]
    public int LayerExclude = 4;

    LineRenderer _LineRenderer;
    Vector3[] position;
    Vector3 offset = Vector3.zero;
    bool rebuild = true;
    float power = 0;
    readonly float line_draw = 0.3f;
    readonly float noise = 1;

    // Use this for initialization
    void Start()
    {
        _LineRenderer = GetComponent<LineRenderer>();
        _LineRenderer.enabled = true;
        _LineRenderer.startWidth = LineWidth;
        _LineRenderer.endWidth = LineWidth;
        transform.GetChild(0).gameObject.SetActive(false);

        StartCoroutine(RepeatingFunction(Random.Range(0.1f, 1.0f)));
    }

    // Update is called once per frame
    void Update()
    {
        if (rebuild) RndSphere();
        rebuild = false;

        DrawTheLine();

        if (power <= 6) power = 7;
        power += (-power) * 0.009f;

        for (int k2 = 0; k2 <= position.Length - 1; k2++)
        {
            offset = transform.TransformPoint(new Vector3(Mathf.Cos(k2 * power * noise), Mathf.Sin(k2 * power * noise), k2 * line_draw));
            position[k2] = offset;
            position[0] = transform.position;
            _LineRenderer.SetPosition(k2, position[k2]);
            transform.GetChild(0).transform.position = position[position.Length - 1];
        }
    }

    void RndSphere()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks * 1000);
        transform.LookAt((Random.onUnitSphere * FindRadius) + transform.position);
    }

    void DrawTheLine()
    {
        int layerMask = 1 << LayerExclude;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, FindRadius, layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            float distance = hit.distance;
            distance /= line_draw;
            position = new Vector3[(int)distance];
            _LineRenderer.positionCount = (int)distance;
            _LineRenderer.enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            return;
        }

        position = new Vector3[1];
        _LineRenderer.positionCount = 1;
        _LineRenderer.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator RepeatingFunction(float interval)
    {
        yield return new WaitForSeconds(interval);
        rebuild = true;
        StartCoroutine(RepeatingFunction(Random.Range(0.1f, 1.5f)));
    }
}
