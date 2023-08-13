import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import {OrderService} from "../../services/order.service";
import {OrderPageComponent} from "./order-page.component";

describe('OrderPageComponent', () => {
  let component: OrderPageComponent;
  let fixture: ComponentFixture<OrderPageComponent>;
  let orderService: OrderService;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ RouterTestingModule ],
      declarations: [ OrderPageComponent ],
      providers: [ OrderService ]
    });
    fixture = TestBed.createComponent(OrderPageComponent);
    component = fixture.componentInstance;
    orderService = TestBed.get(OrderService);
    router = TestBed.get(Router);
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to /menu when toCocktail is called', () => {
    spyOn(router, 'navigate');
    component.toCocktail();
    expect(router.navigate).toHaveBeenCalledWith(['/menu']);
  });

  it('should navigate to /popup when toPopup is called', () => {
    spyOn(router, 'navigate');
    component.toPopup();
    expect(router.navigate).toHaveBeenCalledWith(['/popup']);
  });

  it('should navigate to /order when toOrder is called', () => {
    spyOn(router, 'navigate');
    component.toOrder();
    expect(router.navigate).toHaveBeenCalledWith(['/order']);
  });
});
