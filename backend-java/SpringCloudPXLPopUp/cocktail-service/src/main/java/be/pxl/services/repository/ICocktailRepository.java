package be.pxl.services.repository;

import be.pxl.services.domain.Cocktail;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ICocktailRepository extends JpaRepository<Cocktail, String> {

}
