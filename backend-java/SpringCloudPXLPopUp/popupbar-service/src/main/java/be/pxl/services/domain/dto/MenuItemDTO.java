package be.pxl.services.domain.dto;

import be.pxl.services.domain.ConsumableItem;
import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.PopupBar;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.ManyToOne;
import java.util.UUID;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class MenuItemDTO {

    private UUID id;

    private String itemId;

    private String itemName;

    private UUID barId;

//  private PopupBarDTO popuBarDTO;

    private double purchasePrice;
    private double sellingPrice;

    public MenuItemDTO(MenuItem menuItem) {
        this.id = menuItem.getId();
        this.itemId = menuItem.getItem().getSerialNumber();
        this.itemName = menuItem.getItem().getName();
        this.barId = menuItem.getBar().getId();
        this.purchasePrice = menuItem.getItem().getPurchasePrice();
        this.sellingPrice = menuItem.getSellingPrice();
//      this.popuBarDTO = new PopupBarDTO(menuItem.getBar());
    }
}
