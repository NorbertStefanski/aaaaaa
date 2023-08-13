package be.pxl.services.rest;

import be.pxl.services.controller.PopupBarController;
import be.pxl.services.domain.ConsumableItem;
import be.pxl.services.domain.ConsumableItemCategory;
import be.pxl.services.domain.PopupBar;
import be.pxl.services.domain.dto.ConsumableItemDTO;
import be.pxl.services.domain.dto.PopupBarDTO;
import be.pxl.services.services.IPopupBarService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.test.web.servlet.RequestBuilder;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;


//import static org.junit.jupiter.api.Assertions.*;
@WebMvcTest(controllers = PopupBarController.class)
class PopupBarControllerTest {
    UUID testUUID1 = UUID.randomUUID();
    UUID testUUID2 = UUID.randomUUID();
    @MockBean
    private IPopupBarService popupBarService;

    @Autowired
    private MockMvc mockMvc;

    @Test
    void getPopupBarsTest() throws Exception {
        List<PopupBarDTO> popupBarDTOS = makePopupBarsDTOList(2);

        when(popupBarService.getAllPopupBars()).thenReturn(popupBarDTOS);

        RequestBuilder requestBuilder = MockMvcRequestBuilders
                .get("/api/popupbar/")
                .accept(MediaType.APPLICATION_JSON);

        MvcResult result = mockMvc.perform(requestBuilder).andReturn();

        String expected = "[{\"id\":\"" + testUUID1 + "\",\"name\":\"bar1\",\"address\":\"address1\",\"description\":null,\"menuItems\":[]}," +
                "{\"id\":\"" + testUUID2 + "\",\"name\":\"bar2\",\"address\":\"address2\",\"description\":null,\"menuItems\":[]}]";

        assertEquals(expected, result.getResponse()
                .getContentAsString());
    }


    @Test
    void getAllConsumableItemsTest() throws Exception {
        List<ConsumableItemDTO> consumableItemDTOS = makeConsumableItemDTOList(2);

        when(popupBarService.getAllConsumableItems()).thenReturn(consumableItemDTOS);

        RequestBuilder requestBuilder = MockMvcRequestBuilders
                .get("/api/popupbar/consumable-item")
                .accept(MediaType.APPLICATION_JSON);

        MvcResult result = mockMvc.perform(requestBuilder).andReturn();

        String expected = "[{\"serialNumber\":\"1\",\"name\":\"cocktail1\",\"purchasePrice\":1.0,\"category\":\"Cocktail\"}," +
                "{\"serialNumber\":\"2\",\"name\":\"cocktail2\",\"purchasePrice\":2.0,\"category\":\"Cocktail\"}]";

        assertEquals(expected, result.getResponse()
                .getContentAsString());
    }


    private List<ConsumableItemDTO> makeConsumableItemDTOList(int countOfConsumableItems) {
        List<ConsumableItemDTO> items = new ArrayList<>();
        for (int i = 1; i <= countOfConsumableItems; i++) {
            ConsumableItem consumableItem = ConsumableItem.builder().serialNumber(String.valueOf(i)).name("cocktail" + i).purchasePrice(i).category(ConsumableItemCategory.Cocktail).build();

            items.add(new ConsumableItemDTO(consumableItem));
        }
        return items;
    }

    private List<PopupBarDTO> makePopupBarsDTOList(int countOfBars) {
        List<PopupBarDTO> bars = new ArrayList<>();

        for (int i = 1; i <= countOfBars; i++) {

            PopupBar bar = PopupBar.builder().name("bar" + i).address("address" + i).build();

            bars.add(new PopupBarDTO(bar));
        }

        PopupBarDTO popupBarDTO1 = bars.get(0);
        PopupBarDTO popupBarDTO2 = bars.get(1);
        popupBarDTO1.setId(testUUID1);
        popupBarDTO2.setId(testUUID2);
        return bars;
    }
}
