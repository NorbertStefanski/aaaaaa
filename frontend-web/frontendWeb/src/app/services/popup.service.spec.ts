import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Popup } from 'src/app/models/popup.model';
import { PopupService } from 'src/app/services/popup.service';
import {Observable, of} from "rxjs";
import {ConsumableItem} from "../models/consumable-item.model";
import {MenuItem} from "../models/menuItem.model";
import {MenuItemViewModel} from "../models/menuItemViewModel.model";
import {Cocktail} from "../models/cocktail.model";

describe('PopupService', () => {
  let service: PopupService;
  let httpMock: HttpTestingController;
  let httpClientSpy: { delete: jasmine.Spy };
  let httpClientSpyGet: { get: jasmine.Spy };
  let httpClientSpyPost: { post: jasmine.Spy };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PopupService]
    });
    service = TestBed.inject(PopupService);
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['delete']);
    httpClientSpyGet = jasmine.createSpyObj('HttpClient', ['get']);
    httpClientSpyPost = jasmine.createSpyObj('HttpClient', ['post']);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all popups', () => {
    const mockPopups = [
      { id: '1', name: 'Popup 1', address: '123 Main St', description: 'A great popup!' },
      { id: '2', name: 'Popup 2', address: '456 Elm St', description: 'Another great popup!' }
    ];

    service.getPopups().subscribe(popups => {
      expect(popups.length).toBe(2);
      //expect(popups).toEqual(mockPopups);
    });

    const req = httpMock.expectOne(`${service.apiurl}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockPopups);
  });

  it('should get a popup by id', () => {
    const mockPopups: { address: string; name: string; menuItems: any[]; description: string } = {name: 'Popup 1', address: 'Address 1', description: 'Description 1', menuItems: []};

    service.getPopupById('1').subscribe(popup => {
      //expect(popup).toEqual(mockPopups);
    });

    const req = httpMock.expectOne(`${service.apiurl}1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockPopups);
  });

  it('should add a new popup', () => {
    const mockPopups: { address: string; name: string; menuItems: any[]; description: string } = {name: 'Popup 1', address: 'Address 1', description: 'Description 1', menuItems: []};
    //service.addPopup(mockPopups).subscribe();
    const req = httpMock.expectOne(`${service.apiurl}`);
    expect(req.request.method).toBe('POST');
    req.flush({});
  });

  it('should add a popup', () => {
    const popup: Popup = {
      id: '123',
      name: 'Test Popup',
      address: '123 Test St.',
      description: 'Test description',
      menuItems: []
    };

    service.addPopup(popup).subscribe(data => {
      expect(data).toEqual(popup);
    });

    const req = httpMock.expectOne(`${service.apiurl}`);
    expect(req.request.method).toBe('POST');
    req.flush(popup);
  });

  it('should update a popup', () => {
    const updatedPopup: Popup = {
      id: 'id',
      name: 'Test Popup',
      address: '456 Updated St.',
      description: 'Updated description',
      menuItems: []
    };

    service.updatePopup(updatedPopup).subscribe(data => {
      expect(data).toEqual(updatedPopup);
    });

    const req = httpMock.expectOne(`${service.apiurl}${updatedPopup.name}`);
    expect(req.request.method).toBe('PUT');
    req.flush(updatedPopup);
  });

  it('should delete the popup by id', () => {
    const id = '1';
    httpClientSpy.delete.and.returnValue(of(null));
    service.deletePopup(id).subscribe(() => {
      expect(httpClientSpy.delete).toHaveBeenCalledWith('http://localhost:8083/popupbar/api/popupbar/' + id);
    });
  });

  it('should delete a menu item', () => {
    service.deleteMenuItem('1').subscribe();
    const req = httpMock.expectOne(`${service.apiurl}delete-menu-item/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  it('should return an Observable<ConsumableItem[]>', () => {
    const mockConsumableItems: ConsumableItem[] = [
      { name: 'item1', category: "", purchasePrice: 2, serialNumber: "123" },
      { name: 'item2', category: "", purchasePrice: 2, serialNumber: "123"}
    ];
    httpClientSpyGet.get.and.returnValue(of(mockConsumableItems));

    service.getConsumableItems().subscribe(consumableItems => {
      expect(consumableItems).toEqual(mockConsumableItems);
    });
    expect(httpClientSpyGet.get.calls.count()).toBe(1);
    expect(httpClientSpyGet.get).toHaveBeenCalledWith(`${service.apiurl}consumable-item`);
  });

  it('should return an Observable of MenuItemViewModel[] when getMenuItemsByBar is called', () => {
    const expectedMenuItems: MenuItemViewModel[] = [{id: '12', itemId: '1', itemName: '123', barId: '123', sellingPrice: 12, purchasePrice: 12},
      {id: '12', itemId: '1', itemName: '123', barId: '123', sellingPrice: 12, purchasePrice: 12}];

    httpClientSpyGet.get.and.returnValue(of(expectedMenuItems));

    service.getMenuItemsByBar("barId").subscribe(menuItems => {
      expect(menuItems).toEqual(expectedMenuItems);
    });
  });

  it('should map the data returned by the HTTP call in getPopups', () => {
    const expectedPopups: Popup[] = [{id: '12', name: 'Popup 1', address: 'test address', description: 'test test', menuItems: new Array<Cocktail>()},
      {id: '12', name: 'Popup 1', address: 'test address', description: 'test test', menuItems: new Array<Cocktail>()}];

    httpClientSpyGet.get.and.returnValue(of(expectedPopups));

    service.getPopups().subscribe(popups => {
      expect(popups).toEqual(expectedPopups);
    });
  });

  it('should make a POST request with correct url and payload when addMenuItem is called', () => {
    const menuItem: MenuItem = {id: '12', itemId: '123', barId: '123', sellingPrice: 12 };
    httpClientSpyPost.post.and.returnValue(of(null));

    service.addMenuItem(menuItem).subscribe();
    expect(httpClientSpyPost.post).toHaveBeenCalledWith(`${service.apiurl}menu-item/`, menuItem);
  });

  it('should return an Observable when addMenuItem is called', () => {
    const menuItem: MenuItem = {id: '12', itemId: '123', barId: '123', sellingPrice: 12 };

    httpClientSpyPost.post.and.returnValue(of(null));

    const returnedValue = service.addMenuItem(menuItem);
    expect(returnedValue instanceof Observable).toBeTruthy();
  });
});
