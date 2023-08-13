import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Cocktail } from 'src/app/models/cocktail.model';
import { Popup } from 'src/app/models/popup.model';
import { PopupService } from 'src/app/services/popup.service';

@Component({
  selector: 'app-popupbar-view',
  templateUrl: './popupbar-view.component.html',
  styleUrls: ['./popupbar-view.component.css']
})
export class PopupbarViewComponent implements OnInit {

  popups$!: Observable<Popup[]>;
  popupName: string = "";
  address: string = "";
  description: string = "";
  menuItems: Array<Cocktail> = [];

  constructor(private popupService: PopupService, private router: Router) {

  }

  ngOnInit(): void {
    this.getPopupList();
  }

  getPopupList(): void {
    this.popups$ = this.popupService.getPopups();
    
  }

  openPopupForm() {
    document.getElementsByClassName('addPopupFormBlock')[0].classList.remove("noPopupFormDisplay");
  }

  onAddPopup() {
    closePopupForm();
    const popup = new Popup("", this.popupName, this.address, this.description, this.menuItems);
    this.processAdd(popup);
    this.getPopupList();
  }

  processAdd(event: Popup): void{
    this.popupService.addPopup(event).subscribe(data => {
      console.log(data);
      this.getPopupList();
    });
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

  closePopupForm() {
    closePopupForm();
    this.popupName = "";
    this.address = "";
  }
}

function closePopupForm() {
  document.getElementsByClassName('addPopupFormBlock')[0].classList.add("noPopupFormDisplay");
}
