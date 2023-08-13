package be.pxl.services.repository;

import be.pxl.services.domain.ConsumableItem;
import org.springframework.data.jpa.repository.JpaRepository;

public interface IConsumableItemRepository extends JpaRepository<ConsumableItem, String> {
}
