package be.pxl.services.service;

import be.pxl.services.domain.PopupBar;
import be.pxl.services.domain.dto.PopupBarDTO;
import be.pxl.services.repository.IPopupBarRepository;
import be.pxl.services.services.impl.PopupBarService;
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
class ProductServiceTest {

    UUID testUUID1 = UUID.randomUUID();
    UUID testUUID2 = UUID.randomUUID();

    @Mock
    IPopupBarRepository popupBarRepository;
    @InjectMocks
    PopupBarService popupBarService;

    @Test
    void getAllPopupBarsTest() {
        List<PopupBar> popupBars = makePopupBarsList(2);

        when(popupBarRepository.findAll()).thenReturn(popupBars);
        List<PopupBarDTO> allProducts = popupBarService.getAllPopupBars();

        assertEquals(2, allProducts.size());
        assertEquals("bar1", allProducts.get(0).getName());
    }

    @Test
    void findPopupBarByIdReturnsPopupBarCorrectlyTest() throws Exception {
        List<PopupBar> bars = makePopupBarsList(5);
        when(popupBarRepository.findById(bars.get(0).getId())).thenReturn(Optional.ofNullable(bars.get(0)));
        PopupBarDTO barDTO = popupBarService.getPopupBarById(bars.get(0).getId());
        assertEquals("bar1", barDTO.getName());
        assertEquals(testUUID1, barDTO.getId());
    }



    private List<PopupBar> makePopupBarsList(int countOfBars) {
        List<PopupBar> bars = new ArrayList<>();
        for (int i = 1; i <= countOfBars; i++) {
            PopupBar bar = PopupBar.builder().name("bar" + i).address("address" + i).build();

            bars.add(bar);
        }
        PopupBar popupBar1 = bars.get(0);
        PopupBar popupBar2 = bars.get(1);
        popupBar1.setId(testUUID1);
        popupBar2.setId(testUUID2);
        return bars;
    }

}
