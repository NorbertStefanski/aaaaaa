import {CocktailService} from "./cocktail.service";
import {Cocktail} from "../models/cocktail.model";
import {of} from "rxjs";

describe('CocktailService', () => {
  let service: CocktailService;
  let httpClientSpy: { get: jasmine.Spy, post: jasmine.Spy, put: jasmine.Spy, delete: jasmine.Spy };

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post', 'put', 'delete']);
    service = new CocktailService(<any>httpClientSpy);
  });

  it('should get all cocktails', () => {
    const mockCocktails: Cocktail[] = [
      { serialNumber: '1', name: 'Mojito', purchasePrice: 7, category: 'Cocktail' },
      { serialNumber: '2', name: 'Margarita', purchasePrice: 8, category: 'Cocktail' },
    ];
    httpClientSpy.get.and.returnValue(of(mockCocktails));
    service.getCocktails().subscribe(cocktails => {
      expect(cocktails).toEqual(mockCocktails);
    });
    expect(httpClientSpy.get).toHaveBeenCalledWith('http://localhost:8083/cocktail/api/cocktail/');
  });

  it('should add a cocktail', () => {
    const mockCocktail: Cocktail = { serialNumber: '1', name: 'Mojito', purchasePrice: 7, category: 'Cocktail' };
    httpClientSpy.post.and.returnValue(of(mockCocktail));
    service.addCocktail(mockCocktail).subscribe(cocktail => {
      expect(cocktail).toEqual(mockCocktail);
    });
    expect(httpClientSpy.post).toHaveBeenCalledWith('http://localhost:8083/cocktail/api/cocktail/', mockCocktail);
  });

  it('#updateCocktail should update a cocktail', () => {
    const cocktail: Cocktail = { serialNumber: "1234", name: "Mojito", purchasePrice: 5, category: "Cocktail" }
    httpClientSpy.put.and.returnValue(of(cocktail));
    service.updateCocktail(cocktail).subscribe(res => {
      expect(res).toEqual(cocktail, 'expected the updated cocktail');
      expect(httpClientSpy.put.calls.count()).toBe(1, 'one call');
    });
  });

  it('#deleteCocktail should delete a cocktail', () => {
    const serialNumber = '1234';
    httpClientSpy.delete.and.returnValue(of(null));
    service.deleteCocktail(serialNumber).subscribe(res => {
      expect(res).toBeNull('expected the deleted cocktail to be null');
      expect(httpClientSpy.delete.calls.count()).toBe(1, 'one call');
    });
  });
});
