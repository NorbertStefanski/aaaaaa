import { ConsumableItem } from "./consumable-item.model";
import { Popup } from "./popup.model";

export class MenuItem{
    id: string;
    itemId: String;
    barId: string;
    sellingPrice: number;
  
    constructor(id: string, consumableItem: String, popup: string, sellingPrice: number){
      this.id = id;
      this.itemId = consumableItem;
      this.barId = popup;
      this.sellingPrice = sellingPrice;
    }
  }