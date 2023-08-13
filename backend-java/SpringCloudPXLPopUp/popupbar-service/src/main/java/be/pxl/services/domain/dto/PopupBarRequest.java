package be.pxl.services.domain.dto;

import be.pxl.services.domain.MenuItem;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.CascadeType;
import javax.persistence.FetchType;
import javax.persistence.OneToMany;
import java.util.List;
import java.util.UUID;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class PopupBarRequest {
    private UUID id;
    private String name;
    private String address;
    private String description;
    private List<MenuItem> menuItems;

}
