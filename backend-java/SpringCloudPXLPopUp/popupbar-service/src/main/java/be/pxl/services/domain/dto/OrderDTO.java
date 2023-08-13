package be.pxl.services.domain.dto;

import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.Order;
import be.pxl.services.domain.PopupBar;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class OrderDTO {

    private UUID id;
    private UUID barId;
    public int userId;
    public int tableId;
    public String orderedItemIds;
    public double orderTotal;

    public OrderDTO(Order order) {
        this.id = order.getId();
        this.barId = order.getBarId();
        this.userId = order.getUserId();
        this.tableId = order.getTableId();
        this.orderedItemIds = order.orderedItemIds;
        this.orderTotal = order.orderTotal;

    }
}
