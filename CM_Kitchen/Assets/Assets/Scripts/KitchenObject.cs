using UnityEngine;

public class KitchenObject : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObjectSo;

    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSo;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (_kitchenObjectParent != null) {
            _kitchenObjectParent.ClearKitchenObject();
        }
        _kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("Counter already has a KitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return _kitchenObjectParent;
    }
    public void DestroySelf() {
        _kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else {
            plateKitchenObject = null;
            return false;
        }
    }
}