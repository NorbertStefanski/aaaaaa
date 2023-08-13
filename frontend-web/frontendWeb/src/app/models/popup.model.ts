import { Cocktail } from "./cocktail.model";

export class Popup{
    id: string;
    name: string;
    address: string;
    description: string;
    menuItems: Array<Cocktail>
  
    constructor(id: string, name: string, address: string, descripiton: string, menuItems: Array<Cocktail>) {
      this.id = id;
      this.name = name;
      this.address = address;
      this.description = descripiton;
      this.menuItems = menuItems;
    }
  }
