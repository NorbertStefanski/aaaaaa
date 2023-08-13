import {MenuComponent} from "./menu.component";
import {CocktailService} from "../../services/cocktail.service";
import {Router} from "@angular/router";
import {Cocktail} from "../../models/cocktail.model";
import {of} from "rxjs";

describe('MenuComponent', () => {
  let component: MenuComponent;
  let cocktailService: CocktailService;
  let router: Router;

  beforeEach(() => {
    cocktailService = jasmine.createSpyObj('CocktailService', ['getCocktails', 'addCocktail', 'updateCocktail', 'deleteCocktail']);
    router = jasmine.createSpyObj('Router', ['navigate']);
    component = new MenuComponent(cocktailService, router);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getCocktails method on the CocktailService', () => {
    component.ngOnInit();
    expect(cocktailService.getCocktails).toHaveBeenCalled();
  });

  it('should set the cocktailName, serialNumber, and purchasePrice properties correctly when the onAddCocktail method is called', () => {
    component.cocktailName = 'Mojito';
    component.serialNumber = '123';
    component.purchasePrice = 7.5;
    component.onAddCocktail();
    expect(cocktailService.addCocktail).toHaveBeenCalledWith(new Cocktail('123', 'Mojito', 7.5, 'Cocktail'));
  });

  it('should call the updateCocktail method on the CocktailService', () => {
    component.onUpdateCocktail();
    expect(cocktailService.updateCocktail).toHaveBeenCalled();
  });

  it('should call the openCocktailForm method', () => {
    spyOn(component, 'openCocktailForm');
    component.openCocktailForm();
    expect(component.openCocktailForm).toHaveBeenCalled();
  });

  it('should call the setPutVariablesAndOpenForm method', () => {
    spyOn(component, 'setPutVariablesAndOpenForm');
    component.setPutVariablesAndOpenForm('123', 'Mojito', 7.5);
    expect(component.setPutVariablesAndOpenForm).toHaveBeenCalledWith('123', 'Mojito', 7.5);
  });

  it('should call the closeCocktailForm method', () => {
    spyOn(component, 'closeCocktailForm');
    component.closeCocktailForm();
    expect(component.closeCocktailForm).toHaveBeenCalled();
  });

  it('should reset the cocktailName, serialNumber, and purchasePrice properties when the closeCocktailForm method is called', () => {
    component.cocktailName = 'Mojito';
    component.serialNumber = '123';
    component.purchasePrice = 7.5;
    component.closeCocktailForm();
    expect(component.cocktailName).toEqual('');
    expect(component.serialNumber).toEqual('');
    expect(component.purchasePrice).toEqual(0);
  });

  it('should call the closeUpdateCocktailForm method', () => {
    spyOn(component, 'closeUpdateCocktailForm');
    component.closeUpdateCocktailForm();
    expect(component.closeUpdateCocktailForm).toHaveBeenCalled();
  });

  it('should reset the cocktailName, serialNumber, and purchasePrice properties when the closeUpdateCocktailForm method is called', () => {
    component.cocktailName = 'Mojito';
    component.serialNumber = '123';
    component.purchasePrice = 7.5;
    component.closeUpdateCocktailForm();
    expect(component.cocktailName).toEqual('');
    expect(component.serialNumber).toEqual('');
    expect(component.purchasePrice).toEqual(0);
  });

  it('should filter the cocktail list', () => {
    component.getValue({target: {value: 'Mocktail 1'}});
    component.cocktailFilterArray.subscribe(res => {
      expect(res.length).toBe(1);
      expect(res[0].name).toBe('Mocktail 1');
    });
  });

  it('should call processAdd', () => {
    spyOn(cocktailService, 'addCocktail').and.returnValue(of({}));
    spyOn(component, 'getCocktailList');
    component.onAddCocktail();
    expect(cocktailService.addCocktail).toHaveBeenCalled();
    expect(component.getCocktailList).toHaveBeenCalled();
  });

  it('should call processUpdate', () => {
    spyOn(cocktailService, 'updateCocktail').and.returnValue(of({}));
    spyOn(component, 'getCocktailList');
    component.onUpdateCocktail();
    expect(cocktailService.updateCocktail).toHaveBeenCalled();
    expect(component.getCocktailList).toHaveBeenCalled();
  });
});
