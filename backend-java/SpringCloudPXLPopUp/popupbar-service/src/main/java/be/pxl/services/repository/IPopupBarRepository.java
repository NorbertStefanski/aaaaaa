package be.pxl.services.repository;

import be.pxl.services.domain.PopupBar;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface IPopupBarRepository extends JpaRepository<PopupBar, UUID> {

}
