export class Cocktail{
    serialNumber: string;
    name: string;
    purchasePrice: number;
    category: string;
  
    constructor(serialNumber: string, name: string, purchasePrice: number, category: string){
      this.serialNumber = serialNumber;
      this.name = name;
      this.purchasePrice = purchasePrice;
      this.category = category;
    }
  }