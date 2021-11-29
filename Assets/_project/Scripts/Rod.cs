using NonStandard;
using UnityEngine;

public class Rod : MonoBehaviour {
    public Wire wire;
    public BoxCollider box;

    public Vector3 start {
        get { return wire.StartPoint; }
        set { Set(value, end); }
    }
    public Vector3 end {
        get { return wire.EndPoint; }
        set { Set(start, value); }
    }
    public static void SetBoxCollider(BoxCollider box, Vector3 start, Vector3 end, float width, float height) {
        Vector3 delta = end - start;
        float length = delta.magnitude;
        box.size = new Vector3(width, height, length);
        box.center = new Vector3(0, 0, length / 2);
    }
    public void Set(Vector3 start, Vector3 end) {
        wire.Rod(start, end);
        BoxCollider box = wire.GetComponent<BoxCollider>();
        if (box == null) {
            box = wire.gameObject.AddComponent<BoxCollider>();
            float s = Lines.LINE_SIZE;
            box.size = new Vector3(s, s, s);
        }
        SetBoxCollider(box, start, end, Lines.LINE_SIZE, Lines.LINE_SIZE);
    }
    public static Rod Create(Vector3 start, Color color = default) {
        return Create(start, start, color);
    }
    public static Rod Create(Vector3 start, Vector3 end, Color color = default) {
        Wire w = Lines.MakeWire("rod");
        GameObject g = w.gameObject;
        Rod rod = g.AddComponent<Rod>();
        rod.Set(start, end);
        if (color != default) { rod.SetColor(color); }
        return rod;
    }
    public void SetColor(Color color) { Lines.SetColor(wire.lr, color); }
    private void Awake() {
        if (wire == null) { wire = GetComponent<Wire>(); }
    }
    public void SetGrabbable(bool enable) {
        Grabbable g = GetComponent<Grabbable>();
        if (g == null && enable) {
            g = gameObject.AddComponent<Grabbable>();
            // non-rigidbodies will not correctly trigger OnTrigger events
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb == null) {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
            }
        }
        if (enable) { g.enabled = enable; }
    }
}
