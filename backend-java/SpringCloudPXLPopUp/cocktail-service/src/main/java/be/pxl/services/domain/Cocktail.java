package be.pxl.services.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity
@Table(name="cocktails")
@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class Cocktail {

    //@Id
    //@GeneratedValue(strategy = GenerationType.AUTO)
    //private Long id;

    @Id
    private String serialNumber;
    private String name;
    private double purchasePrice;
    @Enumerated(value = EnumType.STRING)
    private DrinkCategory category;

    public Cocktail(String serialNumber, String name){
        this.serialNumber = serialNumber;
        this.name = name;
    }
}
