package be.pxl.services.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class CocktailRequest {
    private String serialNumber;

    private String name;

    private double purchasePrice;

    private DrinkCategory category;
}
