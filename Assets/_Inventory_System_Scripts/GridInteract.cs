using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]

public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    ItemGrid itemGrid; 
   private void Awake()
   {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as  InventoryController;
        itemGrid = GetComponent<ItemGrid>();
   }
   private void Start()
   {
       inventoryController.SelectedItemGrid = itemGrid;
   }
   private void Update()
   {
       Debug.Log(inventoryController.SelectedItemGrid);
   }
    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryController.SelectedItemGrid = itemGrid;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        return;
    } 
}
