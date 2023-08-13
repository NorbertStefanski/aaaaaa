package be.pxl.services.domain;

import lombok.*;

import java.util.List;
import java.util.UUID;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class BarRequest {
    private UUID id;
    private String name;
    private String address;
    private String description;
    private List<MenuItemRequest> menuItems;

}
