import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Cocktail } from 'src/app/models/cocktail.model';
import { MenuItem } from 'src/app/models/menuItem.model';
import { MenuItemViewModel } from 'src/app/models/menuItemViewModel.model';
import { Popup } from 'src/app/models/popup.model';
import { of } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { PopupService } from 'src/app/services/popup.service';

@Component({
  selector: 'app-popupbar-menu',
  templateUrl: './popupbar-menu.component.html',
  styleUrls: ['./popupbar-menu.component.css']
})
export class PopupbarMenuComponent implements OnInit {

  public popupbarId: string = "";
  popup: Popup | undefined;
  cocktails$!: Observable<Cocktail[]>;
  menuItems$!: Observable<MenuItemViewModel[]>;
  sellingPrice: number = 0;
  cocktailName: string = "";
  serialNumber: string = "";
  purchasePrice: number = 0;

  constructor(private route: ActivatedRoute, 
    private popupService: PopupService, private router: Router) { }

  ngOnInit(): void {
    this.popupbarId = this.route.snapshot.params['id'] as string;
    this.getPopupById(this.popupbarId);
    this.getCocktailList();
    this.getMenuItemList();
  }

  getPopupById(id: string): void {
    this.popupService.getPopupById(id).subscribe(popup => { this.popup = popup})
  }

  getCocktailList(): void {
    this.cocktails$ = this.popupService.getConsumableItems();
  }

  getMenuItemList(): void {
    this.menuItems$ = this.popupService.getMenuItemsByBar(this.popupbarId);
  }

  onAddMenuItem() {
    closeMenuItemForm();

    const menuItem = new MenuItem("", this.serialNumber, this.popupbarId, this.sellingPrice);
    this.processAdd(menuItem);
  }

  processAdd(event: MenuItem) {
    this.popupService.addMenuItem(event).subscribe({next: (data) => {
      console.log(data);
      this.getMenuItemList();
      //showSnackbar();
    },
    error: (error: any) => {
      showSnackbarMenuItemError();
    }
  });
  }

  processDelete(event: string) {
    this.popupService.deleteMenuItem(event).subscribe(data => {
      console.log(data);
      this.getMenuItemList();
      //showSnackbarDelete()
    });
  }

  processDeleteBar(event: string) {
    this.popupService.deletePopup(event).subscribe(data => {
      console.log(data);
      //showSnackbarDelete()
    });
    this.router.navigate(['/popup']);
  }

  openMenuItemForm(name: string, serialNumber: string, purchasePrice: number) {
    this.cocktailName = name;
    this.serialNumber = serialNumber;
    this.purchasePrice = purchasePrice;

    document.getElementsByClassName('addMenuItemFormBlock')[0].classList.remove("noMenuItemFormDisplay");
  }

  closeMenuItemForm() {
    closeMenuItemForm();
    this.cocktailName = "";
    this.serialNumber = "";
    this.purchasePrice = 0;
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

function showSnackbarMenuItemError() {
  // Get the snackbar element
  var x = document.getElementById("snackbarMenuItemError");
  setShow(x);
}

function setShow(x: HTMLElement | null) {
  // Add the "show" class to the snackbar
  if (x != null) {
    x.className = "show";
    }
    // After 3 seconds, remove the show class from the snackbar
    
    setTimeout(function(){ if (x != null) {x.className = x.className.replace("show", "")}; }, 3000);
}

function closeMenuItemForm() {
  document.getElementsByClassName('addMenuItemFormBlock')[0].classList.add("noMenuItemFormDisplay");
}


