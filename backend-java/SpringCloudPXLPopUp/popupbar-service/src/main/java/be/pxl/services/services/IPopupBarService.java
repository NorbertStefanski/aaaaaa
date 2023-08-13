package be.pxl.services.services;

import be.pxl.services.domain.dto.*;

import java.util.List;
import java.util.UUID;

public interface IPopupBarService {

    List<PopupBarDTO> getAllPopupBars();

    PopupBarDTO getPopupBarById(UUID id) throws Exception;

    PopupBarDTO addPopupBar(PopupBarRequest popupBarRequest);

    void deletePopupBar(UUID id) throws Exception;

    void deleteAllPopupBars();

    ConsumableItemDTO addConsumableItem(ConsumableItemRequest consumableItemRequest);

    List<ConsumableItemDTO> getAllConsumableItems();

    void deleteConsumableItem(String serialNumber) throws Exception;

    void deleteAllConsumableItems();

    ConsumableItemDTO updateConsumableItem(String serialNumber, ConsumableItemRequest consumableItemRequest) throws Exception;

    void addMenuItem(MenuItemRequest menuItemRequest) throws Exception;

    MenuItemDTO updateMenuItem(UUID id, MenuItemRequest menuItemRequest) throws Exception;

    List<MenuItemDTO>  getAllMenuItems();

    List<MenuItemDTO> getAllMenuItemsByBar(UUID id) throws Exception;

    void deleteMenuItem(UUID id) throws Exception;

    List<OrderDTO> getAllOrders();
}
