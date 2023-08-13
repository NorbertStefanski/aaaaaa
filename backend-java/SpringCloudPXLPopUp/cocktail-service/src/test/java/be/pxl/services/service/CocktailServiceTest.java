package be.pxl.services.service;

import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.dto.CocktailDTO;
import be.pxl.services.repository.ICocktailRepository;
import be.pxl.services.services.ICocktailService;
import be.pxl.services.services.impl.CocktailService;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class CocktailServiceTest {

    @Mock
    ICocktailRepository cocktailRepository;
    @InjectMocks
    CocktailService cocktailService;

    @Test
    void getAllCocktailsTest() {
        List<Cocktail> cocktails = makeCocktailList(2);

        when(cocktailRepository.findAll()).thenReturn(cocktails);
        List<CocktailDTO> allCocktails = cocktailService.getAllCocktails();

        assertEquals(2, allCocktails.size());
        assertEquals("cocktail1", allCocktails.get(0).getName());

    }



    @Test
    void findCocktailByIdReturnsPopupBarCorrectlyTest() throws Exception {
        List<Cocktail> cocktails = makeCocktailList(5);
        when(cocktailRepository.findById(cocktails.get(0).getSerialNumber())).thenReturn(Optional.ofNullable(cocktails.get(0)));
        CocktailDTO cocktailDTO = cocktailService.getCocktailById(cocktails.get(0).getSerialNumber());
        assertEquals("cocktail1", cocktailDTO.getName());
        assertEquals("1", cocktailDTO.getSerialNumber());
    }


    private List<Cocktail> makeCocktailList(int countOfCocktails) {
        List<Cocktail> cocktails = new ArrayList<>();
        for (int i = 1; i <= countOfCocktails; i++) {
            Cocktail cocktail = Cocktail.builder().name("cocktail" + i).serialNumber(String.valueOf(i)).purchasePrice(i).build();

            cocktails.add(cocktail);
        }

        return cocktails;
    }


}
