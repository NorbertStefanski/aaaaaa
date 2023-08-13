import {MenuItem} from "./menuItem.model";
import {ConsumableItem} from "./consumable-item.model";
import {Popup} from "./popup.model";
import {Cocktail} from "./cocktail.model";

describe('MenuItem', () => {
  let menuItem: MenuItem;
  let consumableItem: ConsumableItem;
  let popup: Popup;

  beforeEach(() => {
    consumableItem = new ConsumableItem("item1","itemName",2,"itemDescription");
    popup = new Popup("popup1","PopupName","PopupType","PopupDescription", new Array<Cocktail>());
    menuItem = new MenuItem("menu1", "consumableItem", popup.id, 10);
  });

  it('should create an instance of MenuItem', () => {
    expect(menuItem).toBeTruthy();
  });

  it('should set the id property correctly', () => {
    expect(menuItem.id).toEqual('menu1');
  });

  // it('should set the itemId property correctly', () => {
  //   expect(menuItem.itemId).toEqual(consumableItem);
  // });
  //
  // it('should set the barId property correctly', () => {
  //   expect(menuItem.barId).toEqual(popup);
  // });

  it('should set the sellingPrice property correctly', () => {
    expect(menuItem.sellingPrice).toEqual(10);
  });
});
