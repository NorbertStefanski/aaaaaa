import {OrderService} from "./order.service";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {TestBed} from "@angular/core/testing";
import {Order} from "../models/order.model";

describe('OrderService', () => {
  let service: OrderService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [OrderService]
    });
    service = TestBed.inject(OrderService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should return an Observable<Order[]>', () => {
    const mockResponse: Order[] = [{
      id: "123",
      barId: "123",
      userId: 123,
      orderedItemIds: "123",
      orderTotal: 123,
      tableId: 12
    }, {
      id: "123",
      barId: "123",
      userId: 123,
      orderedItemIds: "123",
      orderTotal: 123,
      tableId: 12
    }];
    service.getPopups().subscribe(popups => {
      expect(popups.length).toBe(2);
      expect(popups).toEqual(mockResponse);
    });
    const request = httpMock.expectOne('http://localhost:8083/popupbar/api/popupbar/orders');
    request.flush(mockResponse);
    httpMock.verify();
  });
});
