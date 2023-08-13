package be.pxl.services.repository;

import be.pxl.services.domain.ConsumableItem;
import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.PopupBar;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.UUID;

public interface IMenuItemRepository extends JpaRepository<MenuItem, UUID> {
    //Geef hele object mee. Achter de schermen wordt bar omgezet naar de id.
    List<MenuItem> findByBar(PopupBar bar);
    MenuItem findByItem(ConsumableItem item);
}
