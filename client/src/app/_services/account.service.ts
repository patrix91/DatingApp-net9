import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

//SINGLETON
//Miejsce do przechowywania stanów i wykonywania żądań HTTP.

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = 'http://localhost:5203/api/'

  login(model: any){
    return this.http.post(this.baseUrl + 'account/login', model);
  }
}
