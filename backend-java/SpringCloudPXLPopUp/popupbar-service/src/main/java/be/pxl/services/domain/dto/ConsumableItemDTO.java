package be.pxl.services.domain.dto;

import be.pxl.services.domain.ConsumableItem;
import be.pxl.services.domain.ConsumableItemCategory;
import be.pxl.services.domain.PopupBar;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class ConsumableItemDTO {

    private String serialNumber;
    private String name;
    private double purchasePrice;

    private ConsumableItemCategory category;

    public ConsumableItemDTO(ConsumableItem consumableItem) {
        this.serialNumber = consumableItem.getSerialNumber();
        this.name = consumableItem.getName();
        this.purchasePrice = consumableItem.getPurchasePrice();
        this.category = consumableItem.getCategory();
    }
}
