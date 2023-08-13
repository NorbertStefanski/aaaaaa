import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css']
})
export class OrderPageComponent implements OnInit {

  constructor(private orderService: OrderService, private router: Router) {

  }

  ngOnInit(): void {
  }

  toCocktail() {
    this.router.navigate(['/menu']);
  }

  toPopup() {
    this.router.navigate(['/popup']);
  }

  toOrder() {
    this.router.navigate(['/order']);
  }

}
