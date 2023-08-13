package be.pxl.services.client;


import be.pxl.services.domain.Cocktail;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.*;

@FeignClient(name = "popupbar-service") // -> naam van de service
public interface PopupBarClient {

    @PostMapping("api/popupbar/add-item/")
    void addConsumableItem(@RequestBody Cocktail cocktail);

    @DeleteMapping("api/popupbar/delete-item/{serialNumber}")
    void deleteConsumableItem(@PathVariable String serialNumber);

    @DeleteMapping("api/popupbar/delete-item/")
    void deleteAllConsumableItems();

    @PutMapping("api/popupbar/update-item/{serialNumber}/")
    void updateConsumableItem(@PathVariable String serialNumber, @RequestBody Cocktail cocktail);

}
