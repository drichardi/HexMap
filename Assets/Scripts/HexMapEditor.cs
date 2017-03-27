using UnityEngine.EventSystems;
using UnityEngine;

public class HexMapEditor : MonoBehaviour {

    public Color[] colors;
    public GameObject[] meshes;
    public HexGrid hexGrid;

    private GameObject activeMesh;
    private Color activeColor;

    private void Awake() {
        SelectColor(0);
        SelectMesh(0);
    }

    private void Update() {
        if (Input.GetMouseButton(0) &&
            !EventSystem.current.IsPointerOverGameObject()) {
            HandleInput();
        }
    }

    void HandleInput() {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit)) {
            hexGrid.ColorCell(hit.point, activeColor);
            if (activeMesh != null) {
                hexGrid.AddMesh(hit.point, activeMesh);
            }
        }
    }

    public void SelectColor (int index) {
        activeColor = colors[index];
    }

    public void SelectMesh (int index) {
        activeMesh = meshes[index];
    }
}
