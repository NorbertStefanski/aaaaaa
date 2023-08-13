package be.pxl.services.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name="consumableItems")
@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class ConsumableItem {
    @Id
    @Column(length = 13)
    private String serialNumber;
    private String name;
    private double purchasePrice;
    @Enumerated(value = EnumType.STRING)
    private ConsumableItemCategory category;

    // uni directional
//    @OneToMany(mappedBy = "item", cascade = CascadeType.ALL)
//    private List<MenuItem> menuItems;
}
