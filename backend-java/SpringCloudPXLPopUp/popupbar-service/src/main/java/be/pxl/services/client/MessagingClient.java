package be.pxl.services.client;

import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.PopupBar;
import be.pxl.services.domain.dto.MenuItemRequest;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import java.util.UUID;

@FeignClient(name = "messaging-service") // -> naam van de service
public interface MessagingClient {
    @PostMapping("api/messaging/add-bar/")
    void addPopupbar(@RequestBody PopupBar bar);

    @DeleteMapping("api/messaging/delete-bar/{id}")
    void deletePopupBar(@PathVariable UUID id);


    @PostMapping("api/messaging/add-item/")
    void addMenuItem(@RequestBody MenuItemRequest request);

    @DeleteMapping("api/messaging/delete-item/{id}")
    void deleteMenuItem(@PathVariable UUID id);
}
