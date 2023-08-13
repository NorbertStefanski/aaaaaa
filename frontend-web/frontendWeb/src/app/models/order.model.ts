export class Order{
    id: string;
    barId: string;
    userId: number;
    tableId: number;
    orderedItemIds: string;
    orderTotal: number;

    constructor(id: string, barId: string, userId: number, tableId: number, orderedItemIds: string, orderTotal: number){
      this.id = id;
      this.barId = barId;
      this.userId = userId;
      this.tableId = tableId;
      this.orderedItemIds = orderedItemIds;
      this.orderTotal = orderTotal;
    }
  }