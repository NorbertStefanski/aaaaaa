package be.pxl.services.rest;

import be.pxl.services.controller.CocktailController;
import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.DrinkCategory;
import be.pxl.services.domain.dto.CocktailDTO;
import be.pxl.services.domain.dto.CocktailRequest;
import be.pxl.services.services.ICocktailService;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.test.web.servlet.RequestBuilder;
import org.springframework.test.web.servlet.ResultMatcher;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.client.match.MockRestRequestMatchers.content;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.delete;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;


//import static org.junit.jupiter.api.Assertions.*;
@WebMvcTest(controllers = CocktailController.class)
class CocktailControllerTest {

    @MockBean
    private ICocktailService cocktailService;

    @Autowired
    private MockMvc mockMvc;

    @Test
    void getCocktailsTest() throws Exception {
        List<CocktailDTO> cocktailDTOS = makeCocktailsDTOList(2);

        when(cocktailService.getAllCocktails()).thenReturn(cocktailDTOS);

        RequestBuilder requestBuilder = MockMvcRequestBuilders
                .get("/api/cocktail/")
                .accept(MediaType.APPLICATION_JSON);

        MvcResult result = mockMvc.perform(requestBuilder).andReturn();

        String expected = "[{\"serialNumber\":\"1\",\"name\":\"cocktail1\",\"purchasePrice\":1.0,\"category\":\"Cocktail\"}," +
                "{\"serialNumber\":\"2\",\"name\":\"cocktail2\",\"purchasePrice\":2.0,\"category\":\"Cocktail\"}]";

        assertEquals(expected, result.getResponse()
                .getContentAsString());
    }

    @Test
    void getCocktailBySerialNumberTest() throws Exception {

        CocktailDTO cocktailDTO = CocktailDTO.builder().serialNumber("1231231231232").name("cocktail" + 1).purchasePrice(1).category(DrinkCategory.Cocktail).build();
        when(cocktailService.getCocktailById("1231231231232")).thenReturn(cocktailDTO);

        RequestBuilder requestBuilder = MockMvcRequestBuilders
                .get("/api/cocktail/1231231231232")
                .accept(MediaType.APPLICATION_JSON);

        MvcResult result = mockMvc.perform(requestBuilder).andReturn();

        String expected = "{\"serialNumber\":\"1231231231232\",\"name\":\"cocktail1\",\"purchasePrice\":1.0,\"category\":\"Cocktail\"}";

        assertEquals(expected, result.getResponse()
                .getContentAsString());
    }

    @Test
    void addCocktailTest() throws Exception {
        CocktailRequest cocktailRequest = CocktailRequest.builder().serialNumber("1231231231232").name("cocktail" + 3).purchasePrice(3).category(DrinkCategory.Cocktail).build();
        CocktailDTO cocktailDTO = CocktailDTO.builder().serialNumber("1231231231232").name("cocktail" + 3).purchasePrice(3).category(DrinkCategory.Cocktail).build();

        when(cocktailService.addCocktail(cocktailRequest)).thenReturn(cocktailDTO);
        ObjectMapper mapper = new ObjectMapper();

        mockMvc.perform(post("/api/cocktail")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(mapper.writeValueAsString(cocktailRequest)))
                .andExpect(status().isCreated());

    }

    @Test
    void deleteCocktailTest() throws Exception {

        mockMvc.perform(delete("/api/cocktail/{id}", 1)
                        .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk());

    }
    private List<CocktailDTO> makeCocktailsDTOList(int countOfCocktails) {
        List<CocktailDTO> cocktails = new ArrayList<>();

        for (int i = 1; i <= countOfCocktails; i++) {

            CocktailDTO cocktail = CocktailDTO.builder().name("cocktail" + i).serialNumber(String.valueOf(i)).purchasePrice(i).category(DrinkCategory.Cocktail).build();
            cocktails.add(cocktail);
        }
        return cocktails;
    }

}
