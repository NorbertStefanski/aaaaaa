package be.pxl.services.controller;

import be.pxl.services.domain.dto.CocktailDTO;
import be.pxl.services.domain.dto.CocktailRequest;
import be.pxl.services.services.ICocktailService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/cocktail")
@RequiredArgsConstructor
public class CocktailController {
    final private ICocktailService cocktailService;

    @GetMapping
    public ResponseEntity<List<CocktailDTO>> getCocktails() {
        return new ResponseEntity<>(cocktailService.getAllCocktails(), HttpStatus.OK);
    }
    @GetMapping("/{serialNumber}")
    public ResponseEntity<CocktailDTO> getCocktailById(@PathVariable String serialNumber) throws Exception {
        return new ResponseEntity<>(cocktailService.getCocktailById(serialNumber), HttpStatus.OK);
    }

    @PostMapping("")
    public ResponseEntity<CocktailDTO> addCocktail(@RequestBody CocktailRequest cocktailRequest) {
        CocktailDTO cocktailDTO = cocktailService.addCocktail(cocktailRequest);
        return new ResponseEntity<>(cocktailDTO, HttpStatus.CREATED);
    }

    @PutMapping("/{serialNumber}")
    public ResponseEntity<CocktailDTO> updateCocktail(@PathVariable String serialNumber, @RequestBody CocktailRequest cocktailRequest) throws Exception {
        CocktailDTO cocktailDTO = cocktailService.updateCocktail(serialNumber, cocktailRequest);
        return new ResponseEntity<>(cocktailDTO, HttpStatus.CREATED);
    }

    @DeleteMapping("/{serialNumber}")
    public ResponseEntity deleteCocktail(@PathVariable String serialNumber) throws Exception {
        cocktailService.deleteCocktail(serialNumber);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping
    public ResponseEntity deleteAllCocktails() {
        cocktailService.deleteAllCocktails();
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
