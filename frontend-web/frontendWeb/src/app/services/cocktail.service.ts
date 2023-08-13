import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Cocktail } from '../models/cocktail.model';

@Injectable({
  providedIn: 'root'
})
export class CocktailService {

  apiurl: string = 'http://localhost:8083/cocktail/api/cocktail/';

  constructor(private http: HttpClient) { }

  getCocktails(): Observable<Cocktail[]> {
    return this.http.get<Cocktail[]>(this.apiurl).pipe(
      map(data => {
        console.log(data);
        return data;
      })
    );
  }

  addCocktail(cocktail: Cocktail): Observable<any>{
    return this.http.post(this.apiurl, cocktail);
  }

  updateCocktail(cocktail: Cocktail): Observable<any>{
    return this.http.put(this.apiurl+cocktail.serialNumber, cocktail);
  }

  deleteCocktail(serialNumber: string) {
    return this.http.delete(this.apiurl+serialNumber);
  }
}
