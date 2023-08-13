import { ConsumableItem } from "./consumable-item.model";
import { Popup } from "./popup.model";

export class MenuItemViewModel{
    id: string;
    itemId: String;
    itemName: String;
    barId: string;
    sellingPrice: number;
    purchasePrice: number;
  
    constructor(id: string, consumableItem: String, consumableItemName: String, popup: string, sellingPrice: number, purchasePrice: number){
      this.id = id;
      this.itemId = consumableItem;
      this.itemName = consumableItemName;
      this.barId = popup;
      this.sellingPrice = sellingPrice;
      this.purchasePrice = purchasePrice;
    }
  }