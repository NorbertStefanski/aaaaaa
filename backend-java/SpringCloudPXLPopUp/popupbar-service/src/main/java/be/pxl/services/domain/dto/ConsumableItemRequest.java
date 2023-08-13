package be.pxl.services.domain.dto;

import be.pxl.services.domain.ConsumableItemCategory;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.Size;

@Data
@Builder //object creëeren
@NoArgsConstructor
@AllArgsConstructor
public class ConsumableItemRequest {

    //Moet nog checken of deze 13 tekens lang is
    @NotBlank(message = "Serial number is mandatory")
    @Size(min = 13, message = "Serial number has to be 13 characters long")
    @Size(max = 13, message = "Serial number has to be 13 characters long")
    private String serialNumber;
    @NotBlank(message = "Name is mandatory")
    private String name;
    @NotBlank(message = "Purchase price is mandatory")
    private double purchasePrice;
    @NotBlank(message = "Category is mandatory")
    private ConsumableItemCategory category;

}
