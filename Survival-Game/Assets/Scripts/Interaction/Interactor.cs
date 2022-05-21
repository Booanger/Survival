using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    //[SerializeField] private Material highlighMaterial;
    //[SerializeField] LayerMask interactionLayer;
    private Camera _camera;
    //[SerializeField] Transform _rightHand
    [field : SerializeField] public InventorySystem Inventory { get; private set; }

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        _camera = transform.parent.GetComponentInChildren<Camera>();
        Debug.Log(_camera);
        Inventory = GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, maxDistance)) //Physics.Raycast(ray, out hit)
            {
                var _interactableObject = hit.collider.gameObject.GetComponent<IInteractable>();
                if (_interactableObject != null)
                {
                    _interactableObject.Interact(this);
                }
            }
        }
        Debug.DrawLine(_camera.transform.position, hit.point, Color.red);
    }

    /**
    private void OnApplicationQuit()
    {
        //Bu satýr deðiþtirilebilir oyun kapanýp geri açýldýðýnda envantere eklenen objeler envanterde
        //kalýyor ama sahnede tekrar oluþuyorlar bunu önlemek lazým
         
        Inventory.inventory.Container = new InventorySlot[Inventory.inventory.inventorySize];
    }*/
}
