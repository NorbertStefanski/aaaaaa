import { TestBed, ComponentFixture } from '@angular/core/testing';
import { PopupbarViewComponent } from './popupbar-view.component';
import { PopupService } from 'src/app/services/popup.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('PopupbarViewComponent', () => {
  let component: PopupbarViewComponent;
  let fixture: ComponentFixture<PopupbarViewComponent>;
  let popupService: PopupService;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PopupbarViewComponent],
      providers: [
        { provide: PopupService, useValue: { getPopups: () => of([]) } },
        { provide: Router, useValue: { navigate: () => {} } }
      ]
    });
    fixture = TestBed.createComponent(PopupbarViewComponent);
    component = fixture.componentInstance;
    popupService = TestBed.get(PopupService);
    router = TestBed.get(Router);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getPopups on ngOnInit', () => {
    spyOn(popupService, 'getPopups').and.returnValue(of([]));
    fixture.detectChanges();
    expect(popupService.getPopups).toHaveBeenCalled();
  });

  it('should open the popup form when openPopupForm is called', () => {
    //spyOn(document, 'getElementsByClassName').and.returnValue([{ classList: { remove: () => {} } }]);
    component.openPopupForm();
    expect(document.getElementsByClassName).toHaveBeenCalledWith('addPopupFormBlock');
  });

  it('should close the popup form when closePopupForm is called', () => {
    //spyOn(document, 'getElementsByClassName').and.returnValue([{ classList: { add: () => {} } }]);
    component.closePopupForm();
    expect(document.getElementsByClassName).toHaveBeenCalledWith('addPopupFormBlock');
  });

  it('should navigate to the menu when toCocktail is called', () => {
    spyOn(router, 'navigate');
    component.toCocktail();
    expect(router.navigate).toHaveBeenCalledWith(['/menu']);
  });

  it('should navigate to the popup when toPopup is called', () => {
    spyOn(router, 'navigate');
    component.toPopup();
    expect(router.navigate).toHaveBeenCalledWith(['/popup']);
  });

  it('should call getPopupList on ngOnInit', () => {
    spyOn(component, 'getPopupList');
    fixture.detectChanges();
    expect(component.getPopupList).toHaveBeenCalled();
  });

  it('should set popups$ to the result of getPopups', () => {
    const mockPopups = [{ id: 1, name: 'Popup 1' }];
    //spyOn(popupService, 'getPopups').and.returnValue(of(mockPopups));
    component.getPopupList();
    expect(component.popups$).toEqual(of(mockPopups));
  });

  it('should open the popup form when openPopupForm is called', () => {
    //spyOn(document, 'getElementsByClassName').and.returnValue([{ classList: { remove: () => {} } }]);
    component.openPopupForm();
    expect(document.getElementsByClassName).toHaveBeenCalledWith('addPopupFormBlock');
  });

  it('should navigate to the menu when toCocktail is called', () => {
    spyOn(router, 'navigate');
    component.toCocktail();
    expect(router.navigate).toHaveBeenCalledWith(['/menu']);
  });

  it('should navigate to the popup when toPopup is called', () => {
    spyOn(router, 'navigate');
    component.toPopup();
    expect(router.navigate).toHaveBeenCalledWith(['/popup']);
  });

  it('should clear the form fields and close the form when closePopupForm is called', () => {
    //spyOn(document, 'getElementsByClassName').and.returnValue([{ classList: { add: () => {} } }]);
    component.popupName = "Test Popup";
    component.address = "123 Main St";
    component.closePopupForm();
    expect(component.popupName).toEqual("");
    expect(component.address).toEqual("");
    expect(document.getElementsByClassName).toHaveBeenCalledWith('addPopupFormBlock');
  });

  it('should call getPopupList on init', () => {
    spyOn(popupService, 'getPopups').and.returnValue(of([]));
    component.ngOnInit();
    expect(popupService.getPopups).toHaveBeenCalled();
  });

  it('should call processAdd when adding a new popup', () => {
    spyOn(popupService, 'addPopup').and.returnValue(of({}));
    component.onAddPopup();
    expect(popupService.addPopup).toHaveBeenCalled();
  });
});
