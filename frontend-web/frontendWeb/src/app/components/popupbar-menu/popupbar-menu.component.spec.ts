import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import {ActivatedRoute, convertToParamMap, Router} from '@angular/router';
import { of } from 'rxjs';

import { PopupbarMenuComponent } from './popupbar-menu.component';
import { Popup } from 'src/app/models/popup.model';
import { Cocktail } from 'src/app/models/cocktail.model';
import { MenuItemViewModel } from 'src/app/models/menuItemViewModel.model';
import { PopupService } from 'src/app/services/popup.service';
import {MenuItem} from "../../models/menuItem.model";

describe('PopupbarMenuComponent', () => {
  let component: PopupbarMenuComponent;
  let fixture: ComponentFixture<PopupbarMenuComponent>;
  let popupService: jasmine.SpyObj<PopupService>;
  let route: ActivatedRoute;
  let mockCocktail = [{ id: '1', name: 'Test Cocktail', serialNumber: '123', purchasePrice: 3.5, category: 'Alcohol' }];
  let mockMenuItem = [{ id: '1', name: 'Test Menu Item', serialNumber: '123', barId: '1', sellingPrice: 15 }];

  beforeEach(async(() => {
    const popupServiceSpy = jasmine.createSpyObj('PopupService', ['getPopupById', 'getConsumableItems']);

    TestBed.configureTestingModule({
      declarations: [ PopupbarMenuComponent ],
      providers: [
        { provide: PopupService, useValue: popupServiceSpy },
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: convertToParamMap({ id: '1' }) } } },
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PopupbarMenuComponent);
    component = fixture.componentInstance;
    popupService = TestBed.inject(PopupService) as jasmine.SpyObj<PopupService>;
    route = TestBed.inject(ActivatedRoute);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getPopupById and getConsumableItems on init', () => {
    expect(popupService.getPopupById).toHaveBeenCalledWith('1');
    expect(popupService.getConsumableItems).toHaveBeenCalled();
  });

  it('should set popupbarId and popup properties on init', () => {
    popupService.getPopupById.and.returnValue(of(new Popup('12', '12', '12', '12', new Array<Cocktail>())));
    fixture.detectChanges();

    expect(component.popupbarId).toEqual('1');
    expect(component.popup).toEqual(new Popup('12', '12', '12', '12', new Array<Cocktail>()));
  });

  it('should get the cocktail list', () => {
    spyOn(popupService, 'getConsumableItems').and.returnValue(of(mockCocktail));
    component.getCocktailList();
    expect(popupService.getConsumableItems).toHaveBeenCalled();
    //expect(component.cocktails$).toEqual(mockCocktail);
  });

  it('should add a menu item', () => {
    component.sellingPrice = 15;
    component.serialNumber = '123';
    component.popupbarId = '1';

    spyOn(popupService, 'addMenuItem').and.returnValue(of({}));
    component.onAddMenuItem();
    expect(popupService.addMenuItem).toHaveBeenCalledWith(new MenuItem("", '123', '1', 15));
    expect(popupService.getMenuItemsByBar).toHaveBeenCalledWith(component.popupbarId);
  });

  it('should get popup by id', () => {
    const popup = new Popup("1", "Popup 1", "Popup 1 location", "Bar 1", new Array<Cocktail>());
    popupService.getPopupById.and.returnValue(of(popup));
    component.ngOnInit();
    expect(component.popup).toEqual(popup);
  });

  describe('setShow function', () => {
    let x: HTMLElement;

    beforeEach(() => {
      x = document.createElement('div');
      x.className = '';
      document.body.appendChild(x);
    });

    afterEach(() => {
      x.remove();
    });

    it('should add the class "show" to the element', () => {
      //setShow(x);
      expect(x.className).toBe('show');
    });

    it('should remove the class "show" from the element after 3 seconds', (done) => {
      //setShow(x);
      setTimeout(() => {
        expect(x.className).toBe('');
        done();
      }, 3500);
    });
  });
});
