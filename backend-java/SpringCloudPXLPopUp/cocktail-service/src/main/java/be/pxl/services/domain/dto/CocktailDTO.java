package be.pxl.services.domain.dto;

import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.DrinkCategory;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.EnumType;
import javax.persistence.Enumerated;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class CocktailDTO {

    private String serialNumber;
    private String name;
    private double purchasePrice;

    private DrinkCategory category;

    public CocktailDTO(Cocktail cocktail) {
        this.serialNumber = cocktail.getSerialNumber();
        this.name = cocktail.getName();
        this.purchasePrice = cocktail.getPurchasePrice();
        this.category = cocktail.getCategory();
    }

}
