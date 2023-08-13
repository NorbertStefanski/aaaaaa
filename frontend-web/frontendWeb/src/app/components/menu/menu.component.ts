import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { Cocktail } from 'src/app/models/cocktail.model';
import { CocktailService } from 'src/app/services/cocktail.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-menu',
  template: `
    <input (input)="getValue($event)" type="text">
  `,
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent implements OnInit {
  title = "frontendWeb";

  cocktails$!: Observable<Cocktail[]>;
  cocktailFilterArray : Observable<Cocktail[]> = this.cocktails$;
  cocktailName: string = "";
  serialNumber: string = "";
  purchasePrice: number = 0;
  searchString: string = "";

  constructor(private cocktailService: CocktailService, private router: Router) {

  }

  ngOnInit(): void {
    this.getCocktailList();
  }

  getValue(event: any) {
    const value = event.target.value;
    //return this.cocktailFilterArray.filter((proj: any) => proj.name === name);

    //this.cocktailFilterArray.pipe(filter(num => num % 2 === 0));
    console.log(value);

    this.cocktailFilterArray = this.cocktails$.pipe(
      map(res => {
        return res.filter(item => item.name.toLowerCase().includes(value.toLowerCase()
        ));
      })
    )
  }

  getCocktailList(): void {
    this.cocktails$ = this.cocktailService.getCocktails();
    this.cocktailFilterArray = this.cocktails$;
  }

  onAddCocktail() {
    closeCocktailForm();
    const cocktail = new Cocktail(this.serialNumber, this.cocktailName, this.purchasePrice, "Cocktail");
    this.processAdd(cocktail);
  }

  onUpdateCocktail() {
    closeUpdateCocktailForm();
    const cocktail = new Cocktail(this.serialNumber, this.cocktailName, this.purchasePrice, "Cocktail");
    this.processUpdate(cocktail);
  }

  openCocktailForm() {
    document.getElementsByClassName('addCocktailFormBlock')[0].classList.remove("noFormDisplay");
  }

  
  setPutVariablesAndOpenForm(serialNumber: string, name: string, purchasePrice: number) {
    this.cocktailName = name;
    this.serialNumber = serialNumber;
    this.purchasePrice = purchasePrice;
    document.getElementsByClassName('updateCocktailFormBlock')[0].classList.remove("noUpdateFormDisplay");
  }

  closeCocktailForm() {
    closeCocktailForm();
    this.cocktailName = "";
    this.serialNumber = "";
    this.purchasePrice = 0;
  }

  closeUpdateCocktailForm() {
    closeUpdateCocktailForm();
    this.cocktailName = "";
    this.serialNumber = "";
    this.purchasePrice = 0;
  }

  processAdd(event: Cocktail): void {
    this.cocktailService.addCocktail(event).subscribe({next: (data) => {
      console.log(data);
      this.getCocktailList();
      showSnackbar();
    },
    error: (error: any) => {
      showCocktailValidationError();
    }
  });
  }

  processUpdate(event: Cocktail): void{
    this.cocktailService.updateCocktail(event).subscribe(data => {
      console.log(data);
      this.getCocktailList();
      showSnackbarUpdate()
    });
  }

  processDelete(event: string) {
    this.cocktailService.deleteCocktail(event).subscribe(data => {
      console.log(data);
      this.getCocktailList();
      showSnackbarDelete()
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

}

function closeCocktailForm() {
  document.getElementsByClassName('addCocktailFormBlock')[0].classList.add("noFormDisplay");
}

function closeUpdateCocktailForm() {
  document.getElementsByClassName('updateCocktailFormBlock')[0].classList.add("noUpdateFormDisplay");
  
}

function showSnackbar() {
  // Get the snackbar element
  var x = document.getElementById("snackbar");
  setShow(x);
}

function showSnackbarUpdate() {
  // Get the snackbar element
  var x = document.getElementById("snackbarUpdate");
  setShow(x);
  
}

function showSnackbarDelete() {
  // Get the snackbar element
  var x = document.getElementById("snackbarDelete");
  setShow(x);
}

function showCocktailValidationError() {
  var x = document.getElementById("snackbarCocktailValidation");
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



