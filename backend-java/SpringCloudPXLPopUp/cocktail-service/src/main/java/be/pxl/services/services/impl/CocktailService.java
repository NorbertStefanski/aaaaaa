package be.pxl.services.services.impl;

import be.pxl.services.client.MessagingClient;
import be.pxl.services.client.PopupBarClient;
import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.dto.CocktailDTO;
import be.pxl.services.domain.dto.CocktailRequest;
import be.pxl.services.repository.ICocktailRepository;
import be.pxl.services.services.ICocktailService;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class CocktailService implements ICocktailService {
    private final ICocktailRepository cocktailRepository;
    private final PopupBarClient popupBarClient;

    private final MessagingClient messagingClient;

    public List<CocktailDTO> getAllCocktails() {
        return cocktailRepository.findAll().stream().map(CocktailDTO::new).collect(Collectors.toList());
    }

    public CocktailDTO getCocktailById(String id) throws Exception {
        Cocktail cocktail = cocktailRepository.findById(id).orElseThrow(Exception::new);
        return new CocktailDTO(cocktail);
    }

    @Override
    public CocktailDTO addCocktail(CocktailRequest cocktailRequest) {
        Cocktail cocktail = new Cocktail();
        String serialNumber = cocktailRequest.getSerialNumber();
        if (serialNumber.length() != 13) {
            throw new IllegalArgumentException("EAN code needs to be 13 numbers long");
        }
        //12 numbers without check digit at the end
        String serialNumberWithoutCheckDigit = serialNumber.substring(0, serialNumber.length() - 1);
        //13 numbers with check digit at the end
        String checkSerialNumber = ean13CheckDigit(serialNumberWithoutCheckDigit);

        if (!serialNumber.equals(checkSerialNumber)) {
            throw new IllegalArgumentException("EAN code is not valid");
        }
        cocktail.setSerialNumber(serialNumber);
        cocktail.setName(cocktailRequest.getName());
        cocktail.setPurchasePrice(cocktailRequest.getPurchasePrice());
        cocktail.setCategory(cocktailRequest.getCategory());
        cocktailRepository.save(cocktail);
        notifyAddPopupService(cocktail);
        notifyAddMessagingService(cocktail);
        return new CocktailDTO(cocktail);
    }

    @Override
    public void deleteCocktail(String id) throws Exception {
        Cocktail cocktail = cocktailRepository.findById(id).orElseThrow(Exception::new);
        notifyDeletePopupService(id);
        notifyDeleteMessagingService(id);
        cocktailRepository.delete(cocktail);
    }

    @Override
    public void deleteAllCocktails() {
        List<Cocktail> cocktails = cocktailRepository.findAll();
        for (Cocktail cocktail : cocktails) {
            cocktailRepository.delete(cocktail);
        }
        notifyAndDeleteAllItemsInPopupService();
    }


    @Override
    public CocktailDTO updateCocktail(String serialNumber, CocktailRequest cocktailRequest) throws Exception {
        Cocktail cocktailBySerialNumber = cocktailRepository.findById(serialNumber).orElseThrow(Exception::new);
        cocktailBySerialNumber.setName(cocktailRequest.getName());
        cocktailBySerialNumber.setPurchasePrice(cocktailRequest.getPurchasePrice());
        cocktailRepository.save(cocktailBySerialNumber);
        notifyAndUpdateItemInPopupService(serialNumber, cocktailBySerialNumber);
        notifyUpdateMessagingService(serialNumber, cocktailBySerialNumber);
        return new CocktailDTO(cocktailBySerialNumber);

    }


    private void notifyAddPopupService(Cocktail cocktail) {
        popupBarClient.addConsumableItem(cocktail);
    }

    private void notifyDeletePopupService(String id) {
        popupBarClient.deleteConsumableItem(id);
    }


    private void notifyAndDeleteAllItemsInPopupService() {
        popupBarClient.deleteAllConsumableItems();
    }

    private void notifyAndUpdateItemInPopupService(String serialNumber, Cocktail cocktail) {
        popupBarClient.updateConsumableItem(serialNumber, cocktail);
    }

    private void notifyAddMessagingService(Cocktail cocktail) {
        messagingClient.addCocktail(cocktail);
    }
    private void notifyDeleteMessagingService(String serialNumber) {
        messagingClient.deleteCocktail(serialNumber);
    }

    private void notifyUpdateMessagingService(String serialNumber, Cocktail cocktail) {
        messagingClient.updateCocktail(serialNumber, cocktail);
    }

    //Calculate last digit to see if EAN code is correct
    public static String ean13CheckDigit(String barcode) {
        int s = 0;
        for (int i = 0; i < 12; i++) {
            int c = Character.getNumericValue(barcode.charAt(i));
            s += c * ( i%2 == 0? 1: 3);
        }
        s = (10 - s % 10) % 10;
        barcode += s;
        return barcode;
    }
}
