package be.pxl.services.client;

import be.pxl.services.domain.Cocktail;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.*;

@FeignClient(name = "messaging-service") // -> naam van de service
public interface MessagingClient {
    @PostMapping("api/messaging/add-cocktail/")
    void addCocktail(@RequestBody Cocktail cocktail);


    @DeleteMapping("api/messaging/delete-cocktail/{serialNumber}")
    void deleteCocktail(@PathVariable String serialNumber);

    @PutMapping("api/messaging/update-cocktail/{serialNumber}")
    void updateCocktail(@PathVariable String serialNumber, @RequestBody Cocktail cocktail);
}
