package be.pxl.services.services.impl;

import be.pxl.services.client.MessagingClient;
import be.pxl.services.domain.ConsumableItem;
import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.PopupBar;
import be.pxl.services.domain.dto.*;
import be.pxl.services.repository.IConsumableItemRepository;
import be.pxl.services.repository.IMenuItemRepository;
import be.pxl.services.repository.IOrderRepository;
import be.pxl.services.repository.IPopupBarRepository;
import be.pxl.services.services.IPopupBarService;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.UUID;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class PopupBarService implements IPopupBarService {

    private final IPopupBarRepository popupBarRepository;
    private final IConsumableItemRepository consumableItemRepository;

    private final IMenuItemRepository menuItemRepository;

    private final IOrderRepository orderRepository;

    private final MessagingClient messagingClient;
    @Override
    public List<PopupBarDTO> getAllPopupBars() {
        return popupBarRepository.findAll().stream().map(PopupBarDTO::new).collect(Collectors.toList());
    }

    @Override
    public PopupBarDTO getPopupBarById(UUID id) throws Exception {
        PopupBar popupBar = popupBarRepository.findById(id).orElseThrow(Exception::new);
        return new PopupBarDTO(popupBar);
    }

    @Override
    public PopupBarDTO addPopupBar(PopupBarRequest popupBarRequest) {
        PopupBar popupBar = new PopupBar();
        popupBar.setName(popupBarRequest.getName());
        popupBar.setAddress(popupBarRequest.getAddress());
        popupBar.setDescription(popupBarRequest.getDescription());
        popupBar.setMenuItems(popupBarRequest.getMenuItems());
        //Add properties
        popupBarRepository.save(popupBar);

        notifyAddPopupService(popupBar);
        return new PopupBarDTO(popupBar);
    }

    @Override
    public void deletePopupBar(UUID id) throws Exception {
        PopupBar popupBar = popupBarRepository.findById(id).orElseThrow(Exception::new);
        popupBarRepository.delete(popupBar);
        
        notifyDeletePopupService(id);
    }

    @Override
    public void deleteAllPopupBars() {
        List<PopupBar> allPopupBars = popupBarRepository.findAll();
        popupBarRepository.deleteAll(allPopupBars);
    }

    @Override
    public ConsumableItemDTO addConsumableItem(ConsumableItemRequest consumableItemRequest) {
        ConsumableItem consumableItem = ConsumableItem.builder()
                .serialNumber(consumableItemRequest.getSerialNumber())
                .name(consumableItemRequest.getName())
                .purchasePrice(consumableItemRequest.getPurchasePrice())
                .category(consumableItemRequest.getCategory())
                .build();

        ConsumableItem createdItem = consumableItemRepository.save(consumableItem);
        return new ConsumableItemDTO(createdItem);
    }

    @Override
    public List<ConsumableItemDTO> getAllConsumableItems() {
        return consumableItemRepository.findAll().stream().map(ConsumableItemDTO::new).collect(Collectors.toList());
    }

    @Override
    public void deleteConsumableItem(String serialNumber) throws Exception {
        ConsumableItem consumableItem = consumableItemRepository.findById(serialNumber).orElseThrow(Exception::new);
        consumableItemRepository.delete(consumableItem);
    }

    @Override
    public void deleteAllConsumableItems() {
        List<ConsumableItem> consumableItems = consumableItemRepository.findAll();
        consumableItemRepository.deleteAll(consumableItems);
    }

    @Override
    public ConsumableItemDTO updateConsumableItem(String serialNumber, ConsumableItemRequest consumableItemRequest) throws Exception {
        ConsumableItem consumableItemBySerialNumber = consumableItemRepository.findById(serialNumber).orElseThrow(Exception::new);
        consumableItemBySerialNumber.setName(consumableItemRequest.getName());
        consumableItemBySerialNumber.setPurchasePrice(consumableItemRequest.getPurchasePrice());
        consumableItemRepository.save(consumableItemBySerialNumber);
        return new ConsumableItemDTO(consumableItemBySerialNumber);
    }

    @Override
    public void addMenuItem(MenuItemRequest menuItemRequest) throws Exception {
        MenuItem menuItem = new MenuItem();
        PopupBar barById = popupBarRepository.findById(menuItemRequest.getBarId()).orElseThrow(Exception::new);
        if (barById.getMenuItems().stream().filter(b -> b.getItem().getSerialNumber().equals(menuItemRequest.getItemId())).toList().size() >= 1) {
            throw new RuntimeException("Menu-item already exists in this bar");
        }

        ConsumableItem itemById = consumableItemRepository.findById(menuItemRequest.getItemId()).orElseThrow(Exception::new);
        menuItem.setBar(barById);
        menuItem.setItem(itemById);
        menuItem.setSellingPrice(menuItemRequest.getSellingPrice());

        //Add properties
        MenuItem created = menuItemRepository.save(menuItem);

        MenuItemRequest request = new MenuItemRequest(created);
        notifyAddMenuItemPopupService(request);
    }

    @Override
    public MenuItemDTO updateMenuItem(UUID id, MenuItemRequest menuItemRequest) throws Exception {
        MenuItem menuItem = menuItemRepository.findById(id).orElseThrow(Exception::new);
        PopupBar barById = popupBarRepository.findById(menuItemRequest.getBarId()).orElseThrow(Exception::new);
        ConsumableItem itemById = consumableItemRepository.findById(menuItemRequest.getItemId()).orElseThrow(Exception::new);;
        menuItem.setBar(barById);
        menuItem.setItem(itemById);
        menuItem.setSellingPrice(menuItemRequest.getSellingPrice());
        menuItemRepository.save(menuItem);
        return new MenuItemDTO(menuItem);
    }

    @Override
    public List<MenuItemDTO> getAllMenuItems() {
        return menuItemRepository.findAll().stream().map(MenuItemDTO::new).collect(Collectors.toList());
    }

    @Override
    public List<MenuItemDTO> getAllMenuItemsByBar(UUID id) throws Exception {
        PopupBar bar = popupBarRepository.findById(id).orElseThrow(Exception::new);
        return menuItemRepository.findByBar(bar).stream().map(MenuItemDTO::new).collect(Collectors.toList());
    }

    @Override
    public void deleteMenuItem(UUID id) throws Exception {
        MenuItem menuItem = menuItemRepository.findById(id).orElseThrow(Exception::new);

        PopupBar bar = menuItem.getBar();
        List<MenuItem> menuItems = bar.getMenuItems();
        menuItems.remove(menuItem);
        bar.setMenuItems(menuItems);
        popupBarRepository.saveAndFlush(bar);

        menuItem.setBar(null);
        menuItem.setItem(null);
        menuItemRepository.saveAndFlush(menuItem);
        menuItemRepository.delete(menuItem);

        notifyDeleteMenuItemPopupService(id);
    }

    @Override
    public List<OrderDTO> getAllOrders() {
        return orderRepository.findAll().stream().map(OrderDTO::new).collect(Collectors.toList());
    }

    private void notifyAddPopupService(PopupBar bar) {
        messagingClient.addPopupbar(bar);
    }

    private void notifyDeletePopupService(UUID id) {
        messagingClient.deletePopupBar(id);
    }

    private void notifyAddMenuItemPopupService(MenuItemRequest request) {
        messagingClient.addMenuItem(request);
    }

    private void notifyDeleteMenuItemPopupService(UUID id) {
        messagingClient.deleteMenuItem(id);
    }
}
