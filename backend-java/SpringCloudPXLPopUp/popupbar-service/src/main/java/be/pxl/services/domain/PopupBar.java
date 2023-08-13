package be.pxl.services.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.GenericGenerator;
import org.hibernate.annotations.Type;

import javax.persistence.*;
import java.util.List;
import java.util.UUID;

@Entity
@Table(name="popupBars")
@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class PopupBar {

    @Id
    @GeneratedValue(generator = "uuid2")
    @GenericGenerator(name = "uuid2", strategy = "uuid2")
    @Column(columnDefinition = "BINARY(16)")
    private UUID id;
    private String name;
    private String address;
    private String description;

    // popupbar x heeft cocktail x,y
    // popupbar y heeft cocktail y,z
    @OneToMany(mappedBy = "bar", cascade = CascadeType.ALL, fetch = FetchType.EAGER)
    private List<MenuItem> menuItems;
}
