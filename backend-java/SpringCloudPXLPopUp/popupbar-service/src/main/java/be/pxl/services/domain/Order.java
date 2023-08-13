package be.pxl.services.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.GenericGenerator;

import javax.persistence.*;
import java.util.List;
import java.util.UUID;

@Entity
@Table(name="orders")
@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class Order {

    @Id
    private UUID id;
    private UUID barId;
    public int userId;
    public int tableId;
    public String orderedItemIds;
    public double orderTotal;

}
