package be.pxl.services.services;

import be.pxl.services.domain.dto.CocktailDTO;
import be.pxl.services.domain.dto.CocktailRequest;

import java.util.List;

public interface ICocktailService {
    List<CocktailDTO> getAllCocktails();

    CocktailDTO getCocktailById(String id) throws Exception;

    CocktailDTO addCocktail(CocktailRequest cocktailRequest);

    void deleteCocktail(String id) throws Exception;

    void deleteAllCocktails();

    CocktailDTO updateCocktail(String serialNumber, CocktailRequest cocktailRequest) throws Exception;
}
