package be.pxl.services.domain.dto;

import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.PopupBar;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.CascadeType;
import javax.persistence.FetchType;
import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class PopupBarDTO {

    private UUID id;
    private String name;
    private String address;
    private String description;
    private List<MenuItemDTO> menuItems;

    public PopupBarDTO(PopupBar popupBar) {
        this.id = popupBar.getId();
        this.name = popupBar.getName();
        this.address = popupBar.getAddress();
        this.description = popupBar.getDescription();
        List<MenuItemDTO> menuItemDTOList = new ArrayList<>();
        if (popupBar.getMenuItems() != null && popupBar.getMenuItems().size() >= 1) {
            for (MenuItem menuItem : popupBar.getMenuItems()) {
                menuItemDTOList.add(new MenuItemDTO(menuItem));
            }
        }
        this.menuItems = menuItemDTOList;
    }
}
