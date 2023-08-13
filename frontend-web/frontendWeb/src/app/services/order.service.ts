import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Order } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  apiurl: string = 'http://localhost:8083/popupbar/api/popupbar/';

  constructor(private http: HttpClient) { }

  getPopups(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiurl + "orders").pipe(
      map(data => {
        console.log(data);
        return data;
      })
    );
  }
}
