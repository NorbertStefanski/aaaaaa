import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ConsumableItem } from '../models/consumable-item.model';
import { MenuItem } from '../models/menuItem.model';
import { MenuItemViewModel } from '../models/menuItemViewModel.model';
import { Popup } from '../models/popup.model';

@Injectable({
  providedIn: 'root'
})
export class PopupService {

  apiurl: string = 'http://localhost:8083/popupbar/api/popupbar/';

  constructor(private http: HttpClient) { }

  getPopups(): Observable<Popup[]> {
    return this.http.get<Popup[]>(this.apiurl).pipe(
      map(data => {
        console.log(data);
        return data;
      })
    );
  }

  getPopupById(id: string): Observable<Popup> {
    return this.http.get<Popup>(this.apiurl+id).pipe(
      map(data => {
        console.log(data);
        return data;
      })
    );
  }

  addPopup(popup: Popup): Observable<any>{
    return this.http.post(this.apiurl, popup);
  }

  updatePopup(popup: Popup): Observable<any>{
    return this.http.put(this.apiurl+popup.name, popup);
  }

  deletePopup(id: string) {
    return this.http.delete(this.apiurl+id);
  }

  deleteMenuItem(id: string) {
    return this.http.delete(this.apiurl+"delete-menu-item/"+id);
  }

  getConsumableItems(): Observable<ConsumableItem[]> {
    return this.http.get<ConsumableItem[]>(this.apiurl+"consumable-item").pipe(
      map(data => {
        console.log(data);
        return data;
      })
    );
  }

  getMenuItemsByBar(id: string): Observable<MenuItemViewModel[]> {
    return this.http.get<MenuItemViewModel[]>(this.apiurl+"menu-item/bar/"+id).pipe(
      map(data => {
        console.log(data);
        return data;
      })
    );
  }

  addMenuItem(menuItem: MenuItem): Observable<any>{
    return this.http.post(this.apiurl+"menu-item/", menuItem);
  }

  
}

