package be.pxl.services.domain.dto;

import be.pxl.services.domain.ConsumableItem;
import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.PopupBar;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class MenuItemRequest {
    private UUID id;

    private String itemId;

    private UUID barId;

//    private ConsumableItem item;
//
//    private PopupBar bar;

    private double sellingPrice;

    public MenuItemRequest(MenuItem menuItem) {
        this.id = menuItem.getId();
        this.itemId = menuItem.getItem().getSerialNumber();
        this.barId = menuItem.getBar().getId();
        this.sellingPrice = menuItem.getSellingPrice();
    }
}
