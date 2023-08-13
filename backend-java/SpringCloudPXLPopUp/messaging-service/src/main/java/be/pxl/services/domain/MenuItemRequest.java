package be.pxl.services.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Data
@Builder //object creëeren
@AllArgsConstructor
public class MenuItemRequest {

    private UUID id;

    private String itemId;

    private UUID barId;

//  private PopupBarDTO popuBarDTO;

    private double sellingPrice;

    public MenuItemRequest() {
//        this.id = menuItem.getId();
//        this.itemId = menuItem.getItem().getSerialNumber();
//        this.barId = menuItem.getBar().getId();
//        this.sellingPrice = menuItem.getSellingPrice();
//      this.popuBarDTO = new PopupBarDTO(menuItem.getBar());
    }
}
