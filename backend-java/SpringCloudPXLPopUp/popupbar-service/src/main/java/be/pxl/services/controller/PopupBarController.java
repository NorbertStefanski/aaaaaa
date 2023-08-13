package be.pxl.services.controller;

import be.pxl.services.domain.ConsumableItem;
import be.pxl.services.domain.dto.*;
import be.pxl.services.services.IPopupBarService;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/api/popupbar")
@RequiredArgsConstructor
@Slf4j
public class PopupBarController {
    final private IPopupBarService popupBarService;

    @GetMapping
    public ResponseEntity<List<PopupBarDTO>> getPopupBars() {
        return new ResponseEntity<>(popupBarService.getAllPopupBars(), HttpStatus.OK);
    }
    @GetMapping("/{id}")
    public ResponseEntity<PopupBarDTO> getPopupBarsById(@PathVariable UUID id) throws Exception {
        return new ResponseEntity<>(popupBarService.getPopupBarById(id), HttpStatus.OK);
    }

    @PostMapping("")
    public ResponseEntity<PopupBarDTO> AddPopupBar(@RequestBody PopupBarRequest popupBarRequest) {
        PopupBarDTO popupBarDTO = popupBarService.addPopupBar(popupBarRequest);
        return new ResponseEntity<>(popupBarDTO, HttpStatus.CREATED);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity deletePopupBar(@PathVariable UUID id) throws Exception {
        popupBarService.deletePopupBar(id);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping("")
    public ResponseEntity deleteAllPopupBars() {
        popupBarService.deleteAllPopupBars();
        return new ResponseEntity<>(HttpStatus.OK);
    }

    //ConsumableItem
    @PostMapping("/add-item/")
    public ResponseEntity addConsumableItem(@RequestBody ConsumableItemRequest consumableItemRequest) {
        popupBarService.addConsumableItem(consumableItemRequest);
        return new ResponseEntity<>(HttpStatus.OK);
    }

     @PutMapping("/update-item/{serialNumber}/")
     public ResponseEntity updateConsumableItem(@PathVariable String serialNumber , @RequestBody ConsumableItemRequest consumableItemRequest) throws Exception {
         popupBarService.updateConsumableItem(serialNumber, consumableItemRequest);
         return new ResponseEntity<>(HttpStatus.CREATED);
     }

    @GetMapping("/consumable-item")
    public ResponseEntity<List<ConsumableItemDTO>> getAllConsumableItems() {
        return new ResponseEntity<>(popupBarService.getAllConsumableItems(), HttpStatus.OK);
    }

    @DeleteMapping("/delete-item/{serialNumber}")
    public ResponseEntity deleteConsumableItem(@PathVariable String serialNumber) throws Exception {
        popupBarService.deleteConsumableItem(serialNumber);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping("/delete-item/")
    public ResponseEntity deleteAllConsumableItems() throws Exception {
        popupBarService.deleteAllConsumableItems();
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @PostMapping("/menu-item/")
    public ResponseEntity addMenuItem(@RequestBody MenuItemRequest menuItemRequest) throws Exception {
        popupBarService.addMenuItem(menuItemRequest);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @PutMapping("/menu-item/{id}/")
    public ResponseEntity updateMenuItem(@PathVariable UUID id , @RequestBody MenuItemRequest menuItemRequest) throws Exception {
        popupBarService.updateMenuItem(id, menuItemRequest);
        return new ResponseEntity<>(HttpStatus.CREATED);
    }

    @GetMapping("/menu-item")
    public ResponseEntity<List<MenuItemDTO>> getAllMenuItems() {
        return new ResponseEntity<>(popupBarService.getAllMenuItems(), HttpStatus.OK);
    }

    @GetMapping("/menu-item/bar/{id}")
    public ResponseEntity<List<MenuItemDTO>> getAllMenuItemsByBar(@PathVariable UUID id) throws Exception {
        return new ResponseEntity<>(popupBarService.getAllMenuItemsByBar(id), HttpStatus.OK);
    }

    @DeleteMapping("/delete-menu-item/{id}")
    public ResponseEntity deleteMenuItem(@PathVariable UUID id) throws Exception {
        popupBarService.deleteMenuItem(id);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @GetMapping("/orders")
    public ResponseEntity<List<OrderDTO>> getOrders() {
        return new ResponseEntity<>(popupBarService.getAllOrders(), HttpStatus.OK);
    }


}

