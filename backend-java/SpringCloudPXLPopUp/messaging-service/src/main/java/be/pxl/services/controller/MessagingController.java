package be.pxl.services.controller;

import be.pxl.services.MessagingServiceApplication;
import be.pxl.services.domain.BarRequest;
import be.pxl.services.domain.CocktailRequest;
import be.pxl.services.domain.MenuItemRequest;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@RestController
@RequestMapping("/api/messaging")
@Slf4j
@RequiredArgsConstructor
public class MessagingController {

    @Autowired
    private final RabbitTemplate rabbitTemplate;

    //    @SneakyThrows
    @PostMapping("/add-bar")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendNewBarMessage(@RequestBody BarRequest addBarRequest) {
        String barString = String.format("{ \"Id\" : \"%s\", \"Name\" : \"%s\" }", addBarRequest.getId(), addBarRequest.getName());

        System.out.println(barString);
        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "BarReceivedIntegrationEvent", barString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping("/delete-bar/{id}")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity deleteBarMessage(@PathVariable UUID id) {
        String idString = String.format("{ \"Id\" : \"%s\" }", id);

        System.out.println(idString);

        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "BarDeleteIntegrationEvent", idString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    //    @SneakyThrows
    @PostMapping("/add-cocktail")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendNewCocktailMessage(@RequestBody CocktailRequest addCocktailRequest) {

        String cocktailString = String.format("{ \"Id\" : \"%s\",\"Name\" : \"%s\",\"ImageUrl\" : \"%s\" }", addCocktailRequest.getSerialNumber(), addCocktailRequest.getName(), "");

        System.out.println(cocktailString);

        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "CocktailReceivedIntegrationEvent", cocktailString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping("/delete-cocktail/{serialNumber}")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity deleteNewCocktailMessage(@PathVariable String serialNumber) {

        String serialNumberString = String.format("{ \"Id\" : \"%s\" }", serialNumber);

        System.out.println(serialNumberString);

        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "CocktailDeletedIntegrationEvent", serialNumberString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @PutMapping("/update-cocktail/{serialNumber}")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity updateCocktailMessage(@PathVariable String serialNumber, @RequestBody CocktailRequest cocktailRequest) {

        String serialNumberString = String.format("{ \"Id\" : \"%s\",\"Name\" : \"%s\",\"ImageUrl\" : \"%s\" }", serialNumber, cocktailRequest.getName(), "");

        System.out.println(serialNumberString);

        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "CocktailUpdatedIntegrationEvent", serialNumberString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    //@SneakyThrows
    @PostMapping("/add-item")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendNewMenuItemMessage(@RequestBody MenuItemRequest addMenuItemRequest) {

        String menuItemString = String.format("{ \"Id\" : \"%s\",\"BarId\" : \"%s\", \"CocktailId\" : \"%s\", \"Price\" : \"%s\" }", addMenuItemRequest.getId(), addMenuItemRequest.getBarId(), addMenuItemRequest.getItemId(), addMenuItemRequest.getSellingPrice());

        System.out.println(menuItemString);

        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "MenuItemReceivedIntegrationEvent", menuItemString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping("/delete-item/{id}")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity deleteNewMenuItemMessage(@PathVariable UUID id) {

        String idString = String.format("{ \"Id\" : \"%s\" }", id);

        System.out.println(idString);

        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "MenuItemDeleteIntegrationEvent", idString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

}